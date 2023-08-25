using System.Linq.Expressions;

namespace FlashLeit_API.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{

    // In the IRepository interface we have all generic
    // functions that can be applied to basically any
    // application.
    Task<IEnumerable<TEntity?>> GetByIdAsync(string storedProcedure, dynamic parameters);
    Task<IEnumerable<TEntity>> GetAllAsync(string storedProcedure, dynamic parameters);
    
    //Task<IEnumerable<TEntity?>> GetAsync(Expression<Func<T, bool>> predicate); -- Delegates?
    Task<TEntity> AddAsync(string storedProcedure, dynamic parameters);

    // Add list of objects:
    //Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(string storedProcedure, dynamic parameters);
    Task<int> Delete(string storedProcedure, dynamic parameters);
    //Task RemoveRangeAsync(IEnumerable<TEntity> entities);
}