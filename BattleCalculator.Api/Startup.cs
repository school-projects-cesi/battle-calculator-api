using AutoWrapper;
using BattleCalculator.Api.Services;
using BattleCalculator.Api.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Data.Contexts;
using BattleCalculator.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VueCliMiddleware;

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

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");

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

			services.AddTransient<IAuthService, AuthService>();

			// controllers
			services.AddControllers()
				.AddNewtonsoftJson();
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
			{
				UseApiProblemDetailsException = true,
				IsApiOnly = false,
				WrapWhenApiPathStartsWith = "/api"
			});

			if (env.IsDevelopment())
			{
				// app.UseDeveloperExceptionPage();
			}

			// fichier statics
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			// routing
			app.UseRouting();

			// authentification
			app.UseAuthentication();
			app.UseAuthorization();

			// endpoints controller
			app.UseEndpoints(endpoints => endpoints.MapControllers());

			// version dev pour le dev
			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "client-app";

				if (env.IsDevelopment())
				{
					spa.UseVueCli(npmScript: "serve");
				}
			});
		}
	}
}
