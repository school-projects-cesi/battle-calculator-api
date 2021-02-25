﻿using BattleCalculator.Model.Entities;
using BattleCalculator.Model.Enums;
using Bogus;
using Bogus.Extensions;
using System;
using System.Collections.Generic;
using BC = BCrypt.Net.BCrypt;

namespace DefaultSeedConsoleApp
{
    public static class FakeData
    {
        public static List<User> Users = new List<User>();
        public static List<Game> Games = new List<Game>();

        public static void Init(int count)
        {
            string password = "123Pa$$word!";
            string passwordHashed = BC.HashPassword(password);

            var gameFaker = new Faker<Game>("fr")
               .RuleFor(g => g.Level, f => (int)f.PickRandom<LevelType>())
               .RuleFor(g => g.Chrono, _ => 60)
               .RuleFor(g => g.TotalScore, f => f.Random.Int(0, 1000))
               .RuleFor(g => g.CreatedAt, f => f.Date.Past().Date.AddHours(f.Random.Int(9, 22)).AddMinutes(f.Random.Int(0, 59)).AddSeconds(f.Random.Int(0, 59)))
               .RuleFor(g => g.Ended, f => f.Random.Bool(.75f))
               .RuleFor(g => g.EndedAt, (f, g) => g.CreatedAt.AddMinutes(1));

            var userFaker = new Faker<User>("fr")
               .RuleFor(u => u.Username, f => f.Internet.UserName())
               .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Username))
               .RuleFor(u => u.PasswordHashed, _ => passwordHashed)
               .RuleFor(b => b.Games, (f, g) => gameFaker.GenerateBetween(1, 15));

            // populate
            Users.AddRange(userFaker.Generate(count));
        }
    }
}
