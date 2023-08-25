using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Interfaces;

public interface ICollectionRepository : IRepository<CollectionModel>
{
    public Task<CollectionModel> GetCollectionWithCardsAsync(string storedProcedure, int id);
}
