using BattleCalculator.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleCalculator.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }


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
        #endregion
    }
}