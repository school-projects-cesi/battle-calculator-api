using AutoWrapper;
using BattleCalculator.Api.Services;
using BattleCalculator.Api.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Data.Contexts;
using BattleCalculator.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCalculator.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		public void ConfigureServices(IServiceCollection services)
		{
			// database
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"))
			);

			// settings
			IConfigurationSection jwtSettingsSection = Configuration.GetSection("JwtSettings");
			services.Configure<JwtSettings>(jwtSettingsSection);

			// authentications
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,

						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(jwtSettingsSection.Get<JwtSettings>().SecretKey)
						)
					};
				});

			// services and repositories
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddTransient<IGameRepository, GameRepository>();

			services.AddTransient<IAuthService, AuthService>();

			services.AddTransient<IGameService, GameService>();



			// controllers
			services.AddControllers()
				.AddNewtonsoftJson();
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { UseApiProblemDetailsException = true });

			if (env.IsDevelopment())
			{
				// app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
