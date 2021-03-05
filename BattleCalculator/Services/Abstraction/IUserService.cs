using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleCalculator.Models.User;

namespace BattleCalculator.Services.Abstraction
{
	public interface IUserService
	{
		Task UpdateAsync(UpdateUserRequest model);
	}
}
