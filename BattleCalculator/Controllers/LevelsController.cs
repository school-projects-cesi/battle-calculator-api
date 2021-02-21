using System.Collections.Generic;
using AutoMapper;
using BattleCalculator.Common;
using BattleCalculator.Common.Data.Levels;
using BattleCalculator.Controllers.Abstract;
using BattleCalculator.Models.Level;
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
		public IEnumerable<GetLevelListResponse> GetAll()
			=> _mapper.Map<IEnumerable<GetLevelListResponse>>(Constants.LEVELS.Values);
	}
}
