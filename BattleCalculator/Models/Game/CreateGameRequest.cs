using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BattleCalculator.Api.Models.Game
{
	public class CreateGameRequest
	{
		[Required]
		public int Level { get; set; }
	}
}
