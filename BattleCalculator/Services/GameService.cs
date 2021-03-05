using System;
using System.Threading.Tasks;
using BattleCalculator.Model.Entities;
using Microsoft.AspNetCore.Http;
using AutoWrapper.Wrappers;
using System.Collections.Generic;
using System.Linq;
using BattleCalculator.Model.Enums;
using BattleCalculator.Data.Repositories.Abstract;
using BattleCalculator.Settings;
using Microsoft.Extensions.Options;
using BattleCalculator.Services.Abstraction;

namespace BattleCalculator.Services
{
	public class GameService : IGameService
	{
		private readonly IGameRepository _repository;
		private readonly IAuthService _authService;
		private readonly IScoreService _scoreService;
		private readonly AppSettings _appSettings;

		public GameService(IAuthService authService, IGameRepository gameRepository, IOptions<AppSettings> appSettings, IScoreService scoreService)
		{
			_authService = authService;
			_repository = gameRepository;
			_appSettings = appSettings.Value;
			_scoreService = scoreService;
		}

		public async Task<Game> FindByUserAsync(int id)
		{
			int user = _authService.GetUserId();

			Game game = await _repository.FindAsync(id, g => g.Scores);
			if (game == null || game.UserId != user)
				throw new ApiProblemDetailsException("Game not exist", StatusCodes.Status404NotFound);

			return game;
		}

		public async Task<Game> CreateAsync(Game model)
		{
			User user = await _authService.GetUserAsync();
			if (user == null)
				throw new ApiProblemDetailsException("User not exist", StatusCodes.Status403Forbidden);

			// création game
			Game game = new Game
			{
				User = user,
				Level = model.Level,
				Chrono = _appSettings.Chrono,
				TotalScore = 0,
				CreatedAt = DateTime.Now,
				Ended = false
			};

			// ajout d'un score dans la game
			game.Scores.Add(_scoreService.GenerateScore(game.GetLevelType()));

			await _repository.AddAsync(game);
			await _repository.CommitAsync();

			return game;
		}

		public async Task<Game> EndAsync(int id)
		{
			int user = _authService.GetUserId();

			Game game = await _repository.FindAsync(id, g => g.Scores);
			if (game == null || game.UserId != user)
				throw new ApiProblemDetailsException("Game not exist", StatusCodes.Status404NotFound);

			// vérifie la date
			if (!ValidGameDate(game, 60) || game.Ended)
				throw new ApiProblemDetailsException($"Game cant be updated.", StatusCodes.Status403Forbidden);

			// update
			game.Ended = true;
			game.EndedAt = DateTime.Now;
			game.TotalScore = game.Scores.Where(s => s.AnsweredAt != null && s.Result == s.UserResult).Count();

			// database
			_repository.Update(game);
			await _repository.CommitAsync();

			return game;
		}

		public async Task<IEnumerable<(int, Game)>> GetBestUsersByLevelAsync(LevelType level)
		{
			int lastPosition = 1;

			IEnumerable<Game> games = await _repository.GetBestUsersByLevelAsync(level);
			// TODO: ajouter la gestion des égaltiés
			return games
				.OrderByDescending(g => g.TotalScore)
				.ThenByDescending(g => g.EndedAt)
				.Select(g => (lastPosition++, g));
		}

		public bool ValidGameDate(Game game, int plus = 3)
			=> game.CreatedAt.AddSeconds(_appSettings.Chrono + plus) > DateTime.Now;
	}
}
