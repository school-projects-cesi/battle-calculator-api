using AutoWrapper;
using BattleCalculator.Services;
using BattleCalculator.Services.Abstraction;
using BattleCalculator.Data.Abstract;
using BattleCalculator.Data.Contexts;
using BattleCalculator.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using BattleCalculator.Settings;
using Microsoft.Extensions.FileProviders;
using System.IO;
using VueCliMiddleware;

namespace BattleCalculator
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			Env = env;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Env { get; }

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		public void ConfigureServices(IServiceCollection services)
		{
			// database
			services.AddDbContext<ApplicationDbContext>(options => options.UseCosmos(Configuration.GetConnectionString("DefaultConnection"), "database"));

			// In production, the React files will be served from this directory
			if (Env.IsProduction())
			{
				services.AddSpaStaticFiles(options => options.RootPath = "clients/dist");
			}
			else
			{
				services.AddSpaStaticFiles(options => options.RootPath = "clients/client-app/dist");
				services.AddSpaStaticFiles(options => options.RootPath = "clients/client-presentation/dist");
			}

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

			// mapper
			services.AddAutoMapper(typeof(Startup));

			// services and repositories
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddTransient<IGameRepository, GameRepository>();

			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IGameService, GameService>();

			// controllers
			services.AddControllers()
				.AddNewtonsoftJson();
			services.AddHttpContextAccessor();
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		public void Configure(IApplicationBuilder app)
		{
			app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
			{
				UseApiProblemDetailsException = true,
				IsApiOnly = false,
				WrapWhenApiPathStartsWith = "/api"
			});

			if (Env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
			}

			// fichier statics
			app.UseSpaStaticFiles();

			// routing
			app.UseRouting();

			// authentification
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				// forcer la redirection vers l'app vue
				endpoints.MapGet("/", context =>
				{
					context.Response.Redirect("/app/");
					return Task.FromResult(0);
				});
				endpoints.MapControllers();
			});


			// version dev pour le dev
			app.Map("/app", client =>
			{
				client.UseSpa(spa =>
				{
					if (Env.IsProduction())
					{
						spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
						{
							FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "clients", "dist", "app")),
						};
					}
					else
					{
						spa.Options.SourcePath = "clients/client-app";
						spa.UseVueCli(runner: ScriptRunnerType.Yarn, port: 3060);
					}
				});
			});

			app.Map("/presentation", presentation =>
			{
				presentation.UseSpa(spa =>
				{
					if (Env.IsProduction())
					{
						spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
						{
							FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "clients", "dist", "presentation")),
						};
					}
					else
					{
						spa.Options.SourcePath = "clients/client-presentation";
						spa.UseVueCli(runner: ScriptRunnerType.Yarn, port: 3061);
					}
				});
			});
		}
	}
}
