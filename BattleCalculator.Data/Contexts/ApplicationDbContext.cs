using BattleCalculator.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleCalculator.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureModelBuilderForUser(modelBuilder);
        }

        #region privates
        public void ConfigureModelBuilderForUser(ModelBuilder modelBuilder)
        {
            // validations
            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .HasMaxLength(100)
                .IsRequired();

            // unique
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
        }

        public void ConfigureModelBuilderForGame(ModelBuilder modelBuilder)
        {
            // validations
            modelBuilder.Entity<Game>()
                .Property(game => game.Level)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(game => game.Chrono)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(game => game.TotalScore);

            modelBuilder.Entity<Game>()
                .Property(game => game.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(game => game.EndedAt);

            modelBuilder.Entity<Game>()
                .Property(game => game.Ended)
                .IsRequired();
        }
        #endregion
    }
}