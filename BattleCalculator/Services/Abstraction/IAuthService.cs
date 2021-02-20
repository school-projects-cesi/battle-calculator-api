using BattleCalculator.Models.User;
using BattleCalculator.Model.Entities;
using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace BattleCalculator.Services.Abstraction
{
	public interface IAuthService
	{
		string HashPassword(string password);
		bool VerifyPassword(string password, string hashedPassword);
		AuthenticateResponse Authenticate(Guid id);
		string GetUserId();
	}
}
