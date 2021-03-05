﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BattleCalculator.Models.User
{
	public class UpdateUserRequest
	{

		[JsonProperty("Username")]
		public string Username { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
