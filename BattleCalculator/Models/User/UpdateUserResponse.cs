using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BattleCalculator.Api.Models.User
{
	public class UpdateUserResponse
	{
		[Required]
		[JsonProperty("Username")]
		public string Username { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string PasswordHashed { get; set; }
	}
}
