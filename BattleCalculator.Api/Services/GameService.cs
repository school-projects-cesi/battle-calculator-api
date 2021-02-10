using System;
using System.Threading.Tasks;
using BattleCalculator.Api.Models.Game;
using BattleCalculator.Api.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Model.Entities;

namespace BattleCalculator.Api.Services
{
	public class GameService : IGameService
	{
		private readonly IGameRepository _gameRepository;

		public GameService(IGameRepository gameRepository)
		{
			_gameRepository = gameRepository;
		}
		public async Task<Game> Create(CreateGameRequest model)
		{
			Game game = new Game
			{
				Level = model.Level,
				Chrono = "",
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
