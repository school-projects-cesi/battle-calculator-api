
using System;
using System.Threading.Tasks;
using BattleCalculator.Models.User;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Data.Contexts;
using BattleCalculator.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BattleCalculator.Common.Extensions
{
	public static class WebHostExtensions
	{
		public async static Task<IHost> SeedDataAsync(this IHost host)
		{
			using (IServiceScope scope = host.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;

				// context
				ApplicationDbContext applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
				// services
				IAuthService authService = services.GetRequiredService<IAuthService>();

				// seeds				
				#region user
				try
				{
#if DEBUG
					// vérifie si il y a déjà des utilisateur
					if (await applicationDbContext.Users.CountAsync() == 0)
					{
						await SeedAdminUserAsync(authService, applicationDbContext);
						await SeedUserAsync(authService, applicationDbContext);
					}
#endif
				}
				catch { }
				#endregion

				// enregistrement en base de données
				await applicationDbContext.SaveChangesAsync();
			}

			return host;
		}


		#region privates
		private static async Task SeedAdminUserAsync(IAuthService service, ApplicationDbContext context)
		{
			//Seed Default User
			string password = "password";
			User user = new User
			{
				Username = "Admin",
				Email = "admin@gmail.com",
				PasswordHashed = service.HashPassword(password)
			};
			await context.Users.AddAsync(user);
		}
		private static async Task SeedUserAsync(IAuthService service, ApplicationDbContext context)
		{
			//Seed Default User
			string password = "123Pa$$word!";
			User user = new User
			{
				Username = "Jean-Claude",
				Email = "fan@battle-calculator.com",
				PasswordHashed = service.HashPassword(password)
			};
			await context.Users.AddAsync(user);
		}
		#endregion
	}
}
