using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BattleCalculator.Models.Game
{
	public class CreateGameRequest
	{
		[Required, Range(1, 3)]
		public int Level { get; set; }
	}
}
