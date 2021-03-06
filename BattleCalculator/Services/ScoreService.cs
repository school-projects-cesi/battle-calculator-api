using System.Threading.Tasks;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Model.Entities;
using Microsoft.AspNetCore.Http;
using AutoWrapper.Wrappers;
using BattleCalculator.Data.Repositories.Abstract;
using BattleCalculator.Model.Enums;
using BattleCalculator.Common;
using BattleCalculator.Common.Data.Levels;
using System;

namespace BattleCalculator.Services
{
	public class ScoreService : IScoreService
	{
		private readonly IScoreRepository _repository;
		private readonly IAuthService _authService;

		public ScoreService(IAuthService authService, IScoreRepository gameRepository)
		{
			_authService = authService;
			_repository = gameRepository;
		}


		public async Task<Score> FindByUserAsync(int idGame, int id)
		{
			int user = _authService.GetUserId();

			Score score = await _repository.FindAsync(id, s => s.Game);
			if (score == null || score.GameId != idGame || score.Game.UserId != user)
				throw new ApiProblemDetailsException("Score not exist", StatusCodes.Status404NotFound);

			return score;
		}

		public async Task<Score> CreateAsync(int gameId, Score score)
		{
			score.GameId = gameId;
			score.CreatedAt = DateTime.UtcNow;

			await _repository.AddAsync(score);
			await _repository.CommitAsync();

			return score;
		}

		public async Task UpdateAsync(Score score)
		{
			_repository.Update(score);
			await _repository.CommitAsync();
		}

		public Score GenerateScore(LevelType levelType)
		{
			Level level = Constants.LEVELS[levelType];
			LevelOperatorCalcul calcul = level.PickRandomOperator().GenerateCalcul();
			Score score = calcul.TransformToScore();
			score.CreatedAt = DateTime.UtcNow;
			return score;
		}
	}
}
