using System;
using System.Threading.Tasks;
using BattleCalculator.Models.Game;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Model.Entities;

namespace BattleCalculator.Services
{
	public class GameService : IGameService
	{
		private readonly IGameRepository _gameRepository;

		public GameService(IGameRepository gameRepository)
		{
			_gameRepository = gameRepository;
		}
		public async Task<Game> CreateAsync(CreateGameRequest model)
		{
			Game game = new Game
			{
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
