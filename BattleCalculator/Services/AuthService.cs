using BattleCalculator.Models.User;
using BattleCalculator.Services.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BattleCalculator.Model.Entities;
using System.Threading.Tasks;

namespace BattleCalculator.Services
{
	public class AuthService : IAuthService
	{
		private readonly JwtSettings _jwtSettings;

		public AuthService(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}


		public AuthenticateResponse Authenticate(int id)
		{
			DateTime expirationTime = DateTime.UtcNow.AddMinutes(_jwtSettings.Lifespan);

			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, id.ToString())
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

		public string HashPassword(string password)
			=> BC.HashPassword(password);

		public bool VerifyPassword(string password, string hashedPassword)
			=> BC.Verify(password, hashedPassword);
	}
}
