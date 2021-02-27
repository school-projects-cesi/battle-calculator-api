using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleCalculator.Models.Game
{
	public class BestUsersResponse
	{
		public int Position { get; set;  }
		public int IdUser { get; set; }
		public string UserName { get; set; }
		public int Score { get; set; }
		public DateTime Date { get; set;  }
	}
}
