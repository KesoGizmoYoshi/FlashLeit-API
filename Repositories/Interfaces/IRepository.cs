using System.Linq.Expressions;

namespace FlashLeit_API.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{

    // In the IRepository interface we have all generic
    // functions that can be applied to basically any
    // application.
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity?>> GetAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(TEntity entity);

    // Add list of objects:
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities);
}