using System.Threading.Tasks;
using BattleCalculator.Data.Repositories.Abstract;
using BattleCalculator.Model.Entities;
using BattleCalculator.Models.User;
using BattleCalculator.Services.Abstraction;

namespace BattleCalculator.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IAuthService _authService;

		public UserService(IUserRepository userRepository, IAuthService authService)
		{
			_userRepository = userRepository;
			_authService = authService;
		}


		public async Task UpdateAsync(UpdateUserRequest model)
		{
			User user = await _authService.GetUserAsync();

			if (!string.IsNullOrWhiteSpace(model.Email))
				user.Email = model.Email;
			if (!string.IsNullOrWhiteSpace(model.Username))
				user.Username = model.Username;
			if (!string.IsNullOrWhiteSpace(model.Password))
				user.PasswordHashed = _authService.HashPassword(model.Password);

			_userRepository.Update(user);
			await _userRepository.CommitAsync();
		}

		public async Task<User> GetInfoAsync()
			=> await _authService.GetUserAsync();
	}
}
