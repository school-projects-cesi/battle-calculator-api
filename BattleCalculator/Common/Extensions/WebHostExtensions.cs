
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

				// migrations
#if !DEBUG
				await applicationDbContext.Database.EnsureCreatedAsync();
#else
				await applicationDbContext.Database.MigrateAsync();
#endif
			}

			return host;
		}
	}
}
