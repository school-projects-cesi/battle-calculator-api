using System;
using System.Threading.Tasks;
using BattleCalculator.Models.Game;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Model.Entities;
using Microsoft.AspNetCore.Http;
<<<<<<< HEAD
using AutoWrapper.Wrappers;
=======
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
>>>>>>> 35de407 (Récup 10 meilleur joueur par level (sans tri exequo))

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
<<<<<<< HEAD
		
		
=======

>>>>>>> 35de407 (Récup 10 meilleur joueur par level (sans tri exequo))
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

		public async Task<List<GetBestUsersResponse>> GetBestUsersByLevelAsync(int level)
		{
			int cpt = 1;

			IEnumerable<Game> games = await _gameRepository.GetAllAsync();
			List<GetBestUsersResponse> result = new List<GetBestUsersResponse>();


			// Données test
			//List<Game> test = new List<Game>();
			//Random aleatoire = new Random();
			//int _level = 1;
			//for (int i = 1; i<37; i++)
			//{
			//	if (i==21 || i==31)
			//	{
			//		_level += 1;
			//	}

			//	test.Add(new Game
			//	{
			//		Id = new Guid(),
			//		Chrono = 60,
			//		Level = _level,
			//		Ended = true,
			//		UserId=new Guid(),
			//		TotalScore=aleatoire.Next(1, 41),
			//		CreatedAt=DateTime.Now,
			//		EndedAt=DateTime.Now
			//	});
			//}

			




			foreach(var game in games.Where(x=>x.Level==level).Take(10).OrderBy(x=>x.TotalScore))
			{
				result.Add(new GetBestUsersResponse
				{
					Position = cpt,
					IdUser = game.UserId,
					UserName = "User",
					Score = game.TotalScore,
					Date = (DateTime)game.EndedAt
				});

				cpt += 1;
			}


			return result;
		}
	}
}
