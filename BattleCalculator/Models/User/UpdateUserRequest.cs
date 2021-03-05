using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BattleCalculator.Models.User
{
	public class UpdateUserRequest
	{

		[MinLength(6)]
		public string Username { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[MinLength(6)]
		public string Password { get; set; }
	}
}
