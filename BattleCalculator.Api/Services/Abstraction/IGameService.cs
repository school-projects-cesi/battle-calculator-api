using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleCalculator.Api.Models.Game;
using BattleCalculator.Model.Entities;

namespace BattleCalculator.Api.Services.Abstraction
{
	public interface IGameService
	{
		Task<Game> Create(CreateGameRequest model);
	}
}
