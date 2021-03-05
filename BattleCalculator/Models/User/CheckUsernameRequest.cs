using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleCalculator.Models.User
{
	public class CheckUsernameRequest
	{
		[Required]
		public string Username { get; set; }
	}
}
