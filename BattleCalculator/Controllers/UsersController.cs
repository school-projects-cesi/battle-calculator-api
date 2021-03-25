using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BattleCalculator.Models.User;
using BattleCalculator.Model.Entities;
using AutoMapper;

namespace BattleCalculator.Controllers
{
	[Authorize]
	public class UsersController : BaseApiController
	{
		private readonly IUserService _service;
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public UsersController(IUserService userService, IAuthService authService, IMapper mapper)
		{
			_service = userService;
			_authService = authService;
			_mapper = mapper;
		}


		[HttpGet("[action]")]
		public async Task<GetUserResponse> Me()
		{
			User user = await _authService.GetUserAsync();
			return _mapper.Map<GetUserResponse>(user);
		}

		[HttpPatch]
		public async Task<bool> Update([FromBody] UpdateUserRequest model)
		{
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);
			await _service.UpdateAsync(model);
			return true;
		}
	}
}
