using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleCalculator.Api.Models.Game
{
	public class CreateGameResponse
	{
		public int Id { get; set; }

		public int Level { get; set; }

		public string Chrono { get; set; }

	}
}
