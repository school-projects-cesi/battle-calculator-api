using System;
using System.Threading.Tasks;
using BattleCalculator.Models.Game;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Model.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BattleCalculator.Services
{
	public class GameService : IGameService
	{
		private readonly IGameRepository _gameRepository;
		private readonly IAuthService _authService;

		public GameService(IGameRepository gameRepository, IAuthService authService)
		{
			_gameRepository = gameRepository;
			_authService = authService;
		}
		public async Task<Game> CreateAsync(CreateGameRequest model)
		{
			string user = _authService.GetUserId();
			Game game = new Game
			{
				UserId = new Guid(user),
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
