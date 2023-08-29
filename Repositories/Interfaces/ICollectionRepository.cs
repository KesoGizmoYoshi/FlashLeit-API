﻿using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Interfaces;

public interface ICollectionRepository : IRepository<CollectionModel>
{
    Task<CollectionModel> GetCollectionWithCardsAsync(string storedProcedure, int id);

    Task<List<CollectionModel>> GetCollectionsByUserIdAsync(string storedProcedure, int id);
}
