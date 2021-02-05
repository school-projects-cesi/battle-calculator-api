using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using BattleCalculator.Api.Models.Game;
using BattleCalculator.Api.Models.User;
using BattleCalculator.Api.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleCalculator.Api.Controllers
{
	public class GameController : BaseApiController
	{
		private readonly IGameService _gameService;
		private readonly IGameRepository _gameRepository;

		[HttpPost("[action]")]
		public async Task<CreateGameResponse> CreateGame([FromBody] CreateGameRequest model)
		{
			// vérifie que les données de l'utilisateur sont correct
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);

			Game game = await _gameService.NewGame(model);

			return new CreateGameResponse
			{
				Id=game.Id,
				Level=game.Level,
				Chrono=game.Chrono
			};
		}

	}
}
