﻿using BattleCalculator.Model.Entities;
using System.Threading.Tasks;

namespace BattleCalculator.Data.Abstract
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        Task<bool> IsUsernameUniqueAsync(string username);
        Task<bool> isEmailUniqueAsync(string email);
    }
}