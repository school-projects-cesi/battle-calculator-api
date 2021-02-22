using BattleCalculator.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace DefaultSeedConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==== Début de la génération ====");

            // connection
            string connectionstring = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

            // options
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseCosmos(connectionstring, "database");
            // context
            using ApplicationDbContext dbContext = new ApplicationDbContext(optionsBuilder.Options);

            // remove all data
            dbContext.Users.RemoveRange(dbContext.Users);
            dbContext.Games.RemoveRange(dbContext.Games);
            dbContext.SaveChanges();
            Console.WriteLine("Suppression des anciennes données");

            // add new data
            FakeData.Init(45);
            dbContext.Users.AddRange(FakeData.Users);
            dbContext.Games.AddRange(FakeData.Games);
            dbContext.SaveChanges();
            Console.WriteLine("Données ajoutées");

            Console.WriteLine("==== Génération terminée ====");
        }
    }
}
