﻿using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Implementations;

public class CollectionRepository : Repository<CollectionModel>, ICollectionRepository
{

    private readonly ISqlDataAccess _sql;

    public CollectionRepository(ISqlDataAccess sql) : base (sql)
    {
        _sql = sql;
    }
}