using BattleCalculator.Models.User;
using BattleCalculator.Services.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BattleCalculator.Settings;
using Microsoft.AspNetCore.Http;
using BattleCalculator.Model.Entities;
using BattleCalculator.Data.Abstract;
using System.Threading.Tasks;

namespace BattleCalculator.Services
{
	public class AuthService : IAuthService
	{
		private readonly JwtSettings _jwtSettings;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IUserRepository _userRepository;

		public AuthService(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContext, IUserRepository userRepository)
		{
			_jwtSettings = jwtSettings.Value;
			_httpContext = httpContext;
			_userRepository = userRepository;
		}

		public AuthenticateResponse Authenticate(int id)
		{
			DateTime expirationTime = DateTime.UtcNow.AddMinutes(_jwtSettings.Lifespan);

			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, id.ToString())
				}),
				Expires = expirationTime,
				// new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
					SecurityAlgorithms.HmacSha256Signature
				)
			};
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			string token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

			return new AuthenticateResponse
			{
				Token = token,
				TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
				Id = id
			};
		}

		public int GetUserId()
			=> Convert.ToInt32(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

		public async Task<User> GetUserAsync()
			=> await _userRepository.FindAsync(GetUserId());

		public string HashPassword(string password)
			=> BC.HashPassword(password);

		public bool VerifyPassword(string password, string hashedPassword)
			=> BC.Verify(password, hashedPassword);
	}
}
