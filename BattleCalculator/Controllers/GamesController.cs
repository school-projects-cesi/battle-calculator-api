using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using BattleCalculator.Common.Data.Levels;
using BattleCalculator.Controllers.Abstract;
using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;
using BattleCalculator.Models.Game;
using BattleCalculator.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleCalculator.Controllers
{
	[Authorize]
	public class GamesController : BaseApiController
	{
		private readonly IGameService _gameService;
		private readonly IMapper _mapper;

		public GamesController(IGameService gameService, IMapper mapper)
		{
			_gameService = gameService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<CreateGameResponse> Post([FromBody] CreateGameRequest model)
		{
			// vérifie que les données de l'utilisateur sont correct
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);

			Game game = await _gameService.CreateAsync(model);
			return _mapper.Map<CreateGameResponse>(game);
		}


		[HttpGet("[action]/{level:int}")]
		public async Task<IEnumerable<BestUserResponse>> Best(int? level)
		{
			//if (Level.CheckType(level, out LevelType levelType))
			//	throw new ApiProblemDetailsException($"Level with type {level} not exist.", StatusCodes.Status404NotFound);
			IEnumerable<(int, Game)> result = await _gameService.GetBestUsersByLevelAsync((LevelType)level);
			return result.Select(g => new BestUserResponse
			{
				Position = g.Item1,
				IdUser = g.Item2.UserId,
				UserName = g.Item2.User.Username,
				Score = g.Item2.TotalScore,
				Date = g.Item2.EndedAt.Value
			});
		}
	}
}
