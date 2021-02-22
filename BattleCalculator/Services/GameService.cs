using System;
using System.Threading.Tasks;
using BattleCalculator.Models.Game;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Model.Entities;
using Microsoft.AspNetCore.Http;
using AutoWrapper.Wrappers;

namespace BattleCalculator.Services
{
	public class GameService : IGameService
	{
		private readonly IGameRepository _gameRepository;
		private readonly IAuthService _authService;

		public GameService(IAuthService authService, IGameRepository gameRepository)
		{
			_authService = authService;
			_gameRepository = gameRepository;
		}
		
		
		public async Task<Game> CreateAsync(CreateGameRequest model)
		{
			User user = await _authService.GetUserAsync();
			if (user == null)
				throw new ApiProblemDetailsException("User not exist", StatusCodes.Status403Forbidden);

			Game game = new Game
			{
				User = user,
				Level = model.Level,
				Chrono = 60,
				TotalScore = 0,
				CreatedAt = DateTime.Now,
				Ended = false
			};

			await _gameRepository.AddAsync(game);
			await _gameRepository.CommitAsync();

			return game;
		}
	}
}
