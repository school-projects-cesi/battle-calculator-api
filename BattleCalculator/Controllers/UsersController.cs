using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BattleCalculator.Models.User;

namespace BattleCalculator.Controllers
{
	[Authorize]
	public class UsersController : BaseApiController
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		#region UPDATE

		[HttpPatch]
		public async Task<bool> Update([FromBody] UpdateUserRequest model)
		{
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);
			await _userService.UpdateAsync(model);
			return true;
		}
		#endregion
	}
}
