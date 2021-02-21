using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using BattleCalculator.Controllers.Abstract;
using BattleCalculator.Model.Entities;
using BattleCalculator.Models.Game;
using BattleCalculator.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
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
	}
}
