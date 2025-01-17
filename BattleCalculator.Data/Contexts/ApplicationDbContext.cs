﻿using BattleCalculator.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BattleCalculator.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Score> Scores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureModelBuilderForUser(modelBuilder);
            ConfigureModelBuilderForGame(modelBuilder);
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
                .Property(game => game.UserId)
                .IsRequired();

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
                .Property(game => game.Ended)
                .IsRequired();
        }

        public void ConfigureModelBuilderForScore(ModelBuilder modelBuilder)
        {
            // validations
            modelBuilder.Entity<Score>()
                .Property(game => game.GameId)
                .IsRequired();

            modelBuilder.Entity<Score>()
                .Property(game => game.Operation)
                .IsRequired();

            modelBuilder.Entity<Score>()
                .Property(game => game.Result)
                .IsRequired();

            modelBuilder.Entity<Score>()
                .Property(game => game.CreatedAt)
                .IsRequired();
        }
        #endregion
    }
}