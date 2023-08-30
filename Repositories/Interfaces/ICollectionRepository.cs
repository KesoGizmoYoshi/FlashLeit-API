using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Interfaces;

public interface ICollectionRepository : IRepository<CollectionModel>
{
    Task<CollectionModel> GetCollectionWithCardsAsync(string storedProcedure, int collectionId, int userId);

    Task<List<CollectionModel>> GetCollectionsByUserIdAsync(string storedProcedure, int id);
}
