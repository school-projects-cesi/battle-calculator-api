using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using BattleCalculator.Models.User;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BattleCalculator.Controllers.Abstract;
using BattleCalculator.Data.Repositories.Abstract;

namespace BattleCalculator.Controllers
{
	public class AuthController : BaseApiController
	{
		private readonly IAuthService _authService;
		private readonly IUserRepository _userRepository;

		public AuthController(IAuthService authService, IUserRepository userRepository)
		{
			_authService = authService;
			_userRepository = userRepository;
		}

		[HttpPost("[action]")]
		public async Task<AuthenticateResponse> Login([FromBody] AuthenticateRequest model)
		{
			// vérifie que les données de l'utilisateur sont correct
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);

			// recherche le user en fonction de l'email
			User user = await _userRepository.FindAsync(u => u.Email == model.Email);

			// vérifie qu'un user existe avec cette email
			if (user == null)
			{
				ModelState.AddModelError(nameof(model.Email), "Nom d'utilisateur ou mot de passe incorrect.");
				throw new ApiProblemDetailsException(ModelState);
			}

			bool passwordValid = _authService.VerifyPassword(model.Password, user.PasswordHashed);
			// vérifie que le password est correct
			if (!passwordValid)
			{
				ModelState.AddModelError(nameof(model.Email), "Nom d'utilisateur ou mot de passe incorrect.");
				throw new ApiProblemDetailsException(ModelState);
			}

			// renvoie les données pour l'authenification
			return _authService.Authenticate(user.Id);
		}

		[HttpPost("[action]")]
		public async Task<AuthenticateResponse> Register([FromBody] RegisterRequest model)
		{
			// vérifie que les données de l'utilisateur sont correct
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);

			bool emailUnique = await _userRepository.isEmailUniqueAsync(model.Email);
			// vérifie que l'email n'est pas déjà utilisé
			if (!emailUnique)
			{
				ModelState.AddModelError(nameof(model.Email), "This email already exists.");
				throw new ApiProblemDetailsException(ModelState);
			}

			bool usernameUniq = await _userRepository.IsUsernameUniqueAsync(model.Username);
			// vérifie que le pseudo n'est pas déjà utilisé
			if (!usernameUniq)
			{
				ModelState.AddModelError(nameof(model.Username), "This username already exists.");
				throw new ApiProblemDetailsException(ModelState);
			}

			// créer le nouveau user
			User user = new User
			{
				Username = model.Username,
				Email = model.Email,
				PasswordHashed = _authService.HashPassword(model.Password)
			};
			await _userRepository.AddAsync(user);
			await _userRepository.CommitAsync();

			// renvoie les données pour l'authenification
			return _authService.Authenticate(user.Id);
		}

	}
}
