using System;
using System.Collections.Generic;
using AutoMapper;
using AutoWrapper.Wrappers;
using BattleCalculator.Common;
using BattleCalculator.Common.Data.Levels;
using BattleCalculator.Common.Extensions;
using BattleCalculator.Controllers.Abstract;
using BattleCalculator.Model.Enums;
using BattleCalculator.Models.Level;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleCalculator.Controllers
{
	public class LevelsController : BaseApiController
	{
		private readonly IMapper _mapper;

		public LevelsController(IMapper mapper)
		{
			_mapper = mapper;
		}


		[HttpGet]
		public IEnumerable<GetTinyLevelResponse> GetAll()
			=> _mapper.Map<IEnumerable<GetTinyLevelResponse>>(Constants.LEVELS.Values);

		[HttpGet("{type:int}")]
		public GetLevelResponse Get(int type)
		{
			if (Level.CheckType(type, out LevelType levelType))
				throw new ApiProblemDetailsException($"Level with type {type} not exist.", StatusCodes.Status404NotFound);

			return _mapper.Map<GetLevelResponse>(Constants.LEVELS[levelType]);
		}
	}
}
