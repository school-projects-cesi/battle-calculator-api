using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using BattleCalculator.Common;
using BattleCalculator.Common.Data.Levels;
using BattleCalculator.Controllers.Abstract;
using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;
using BattleCalculator.Models.Game;
using BattleCalculator.Models.Level;
using BattleCalculator.Models.Score;
using BattleCalculator.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleCalculator.Controllers
{
	[Authorize]
	public class GamesController : BaseApiController
	{
		private readonly IGameService _service;
		private readonly IScoreService _scoreService;
		private readonly IMapper _mapper;

		public GamesController(IGameService gameService, IScoreService scoreService, IMapper mapper)
		{
			_service = gameService;
			_scoreService = scoreService;
			_mapper = mapper;
		}


		[HttpGet("[action]/{level:int}")]
		public async Task<IEnumerable<BestUserResponse>> Best(int? level)
		{
			if (Level.CheckType(level, out LevelType levelType))
				throw new ApiProblemDetailsException($"Level with type {level} not exist.", StatusCodes.Status404NotFound);

			IEnumerable<(int, Game)> result = await _service.GetBestUsersByLevelAsync((LevelType)level);
			return result.Select(g => new BestUserResponse
			{
				Position = g.Item1,
				IdUser = g.Item2.UserId,
				UserName = g.Item2.User.Username,
				Score = g.Item2.TotalScore,
				Date = g.Item2.EndedAt.Value
			});
		}

		[HttpPost]
		public async Task<CreateGameResponse> Post([FromBody] CreateGameRequest model)
		{
			// vérifie que les données de l'utilisateur sont correct
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);

			Game game = await _service.CreateAsync(_mapper.Map<Game>(model));
			CreateGameResponse response = _mapper.Map<CreateGameResponse>(game);
			response.Score = _mapper.Map<CreateScoreResponse>(game.Scores.FirstOrDefault());
			return response;
		}

		[HttpGet("{id:int}")]
		public async Task<GetGameResponse> Get(int? id)
		{
			if (id == null || id < 1)
				throw new ApiProblemDetailsException($"Game with id {id} not exist.", StatusCodes.Status404NotFound);

			// récupère le game
			Game game = await _service.FindByUserAsync(id.Value);

			// vérifie la date
			if (!_service.ValidGameDate(game) || game.Ended)
				throw new ApiProblemDetailsException($"Game cant be returned.", StatusCodes.Status403Forbidden);

			// rendu
			GetGameResponse response = _mapper.Map<GetGameResponse>(game);
			response.Score = _mapper.Map<GetScoreResponse>(game.Scores.LastOrDefault());
			response.Started = game.Scores.Any(s => s.AnsweredAt != null);
			return response;
		}

		[HttpPatch("{id:int}/[action]")]
		public async Task<PatchGameResponse> End(int? id)
		{
			if (id == null || id < 1)
				throw new ApiProblemDetailsException($"Game with id {id} not exist.", StatusCodes.Status404NotFound);

			// récupère le game
			Game game = await _service.EndAsync(id.Value);
			PatchGameResponse response = _mapper.Map<PatchGameResponse>(game);
			response.LevelData = _mapper.Map<GetTinyLevelResponse>(Constants.LEVELS[(LevelType)game.Level]);
			// rendu
			return response;
		}


		#region scores
		[HttpPatch("{idGame:int}/[action]/{id:int}")]
		public async Task<PatchScoreResponse> Scores(int? idGame, int? id, CreateScoreRequest model)
		{
			if (idGame == null || idGame < 1)
				throw new ApiProblemDetailsException($"Game with id {idGame} not exist.", StatusCodes.Status404NotFound);

			if (id == null || id < 1)
				throw new ApiProblemDetailsException($"Score with id {idGame} not exist.", StatusCodes.Status404NotFound);

			// récupère le score
			Score score = await _scoreService.FindByUserAsync(idGame.Value, id.Value);

			// vérifie la date
			if (!_service.ValidGameDate(score.Game))
				throw new ApiProblemDetailsException($"Game cant be updated.", StatusCodes.Status403Forbidden);

			// vérifie la date
			if (score.AnsweredAt != null)
				throw new ApiProblemDetailsException($"Score cant be updated.", StatusCodes.Status403Forbidden);

			// vérifie que les données de l'utilisateur sont correct
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);

			// mise à jour de l'objet score
			score.UserResult = model.Result;
			score.AnsweredAt = DateTime.Now;
			await _scoreService.UpdateAsync(score);
			Score newScore = _scoreService.GenerateScore(score.Game.GetLevelType());
			await _scoreService.CreateAsync(score.Game.Id, newScore);
			// rendu
			return new PatchScoreResponse
			{
				Updated = _mapper.Map<CreateScoreResponse>(score),
				Next = _mapper.Map<CreateScoreResponse>(newScore)
			};
		}
		#endregion
	}
}
