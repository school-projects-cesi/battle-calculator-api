using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleCalculator.Models.User
{
	public class CheckEmailRequest
	{
		[Required, EmailAddress]
		public string Email { get; set; }
	}
}
