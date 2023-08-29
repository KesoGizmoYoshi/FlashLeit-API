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

    public async Task<CollectionModel> GetCollectionWithCardsAsync(string storedProcedure, int collectionId)
    {
        var results = await _sql.GetCollectionWithCardsAsync(storedProcedure, collectionId);

        return results;
    }

    public async Task<List<CollectionModel>> GetCollectionsByUserIdAsync(string storedProcedure, int userId)
    {
        var results = await _sql.GetCollectionsByUserId(storedProcedure, userId);

        return results;
    }
}
