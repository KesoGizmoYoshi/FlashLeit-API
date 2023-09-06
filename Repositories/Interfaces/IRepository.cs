using System.Linq.Expressions;

namespace FlashLeit_API.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity?>> GetByIdAsync(string storedProcedure, dynamic parameters);
    Task<IEnumerable<TEntity>> GetAllAsync(string storedProcedure, dynamic parameters);
    Task<IEnumerable<TEntity>> AddAsync(string storedProcedure, dynamic parameters);
    Task<int> Update(string storedProcedure, dynamic parameters);
    Task<int> Delete(string storedProcedure, dynamic parameters);

}