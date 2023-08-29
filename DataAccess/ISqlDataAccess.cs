using flashleit_class_library.Models;

namespace FlashLeit_API.DataAccess;

public interface ISqlDataAccess
{
    Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters);
    Task<int> SaveData<T>(string storedProcedure, T parameters);

    Task<CollectionModel> GetCollectionWithCardsAsync(string storedProcedure, int collectionId);

    Task<List<CollectionModel>> GetCollectionsByUserId(string storedProcedure, int userId);
}