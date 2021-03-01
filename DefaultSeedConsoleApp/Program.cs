using BattleCalculator.Data.Contexts;
using CommandLine;
using DefaultSeedConsoleApp.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DefaultSeedConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ParserResult<CommandLineOptions> result = Parser.Default.ParseArguments<CommandLineOptions>(args);
            result.WithParsedAsync(MainAsync);
        }


        static async Task MainAsync(CommandLineOptions options)
        {
            try
            {
                Console.WriteLine("==== Début de la génération ====");

                // connection
                string connectionString = "Server=localhost;port=3306;Database=battle-calculator;User=root;Password=;";
                //string connectionString = "Data Source=./data/database/sqlite.db;Foreign Keys=False";

                // options
                DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseMySQL(connectionString);
                //optionsBuilder.UseSqlite(connectionString);
                // context
                using ApplicationDbContext dbContext = new ApplicationDbContext(optionsBuilder.Options);

                // remove all data
                if (options.Verbose)
                    Console.WriteLine("Suppression des anciennes données");
                await dbContext.Database.ExecuteSqlRawAsync("SET FOREIGN_KEY_CHECKS=0;");
                dbContext.Users.RemoveRange(dbContext.Users);
                dbContext.Games.RemoveRange(dbContext.Games);
                await dbContext.SaveChangesAsync();
                await dbContext.Database.ExecuteSqlRawAsync("SET FOREIGN_KEY_CHECKS=1;");

                // generate
                if (options.Verbose)
                    Console.WriteLine("Génération des données");
                FakeData.Init(options.Size);

                // add new data
                if (options.Verbose)
                    Console.WriteLine("Insertion des données");
                await dbContext.Users.AddRangeAsync(FakeData.Users);
                await dbContext.SaveChangesAsync(); 
                if (options.Verbose)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Données ajoutées");
                    Console.ResetColor();
                }

                Console.WriteLine("==== Génération terminée ====");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                LogError(e);
                Console.ResetColor();
            }
        }


        #region privates
        private static void LogError(Exception e)
        {
            Console.WriteLine("ERROR: " + e.Message);
            if (e.InnerException != null)
                LogError(e.InnerException);
        }
        #endregion
    }
}
