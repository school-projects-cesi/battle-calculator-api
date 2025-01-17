﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BattleCalculator.Models.User
{
	public class AuthenticateRequest
	{
		[Required]
		[JsonProperty("Username")]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
