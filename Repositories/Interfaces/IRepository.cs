using System.Linq.Expressions;

namespace FlashLeit_API.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities);
}