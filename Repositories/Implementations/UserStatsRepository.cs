﻿using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Implementations;

public class UserStatsRepository: Repository<UserStatsModel>, IUserStatsRepository
{
    private readonly SqlDataAccess _sql;

    public UserStatsRepository(SqlDataAccess sql) : base(sql)
    {
        _sql = sql;
    }
}
