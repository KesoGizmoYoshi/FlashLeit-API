namespace FlashLeit_API.DataAccess;

public interface ISqlDataAccess
{
    Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters);
    Task SaveData<T>(string storedProcedure, T parameters);
}