using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace FlashLeit_API.Repositories.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{

    protected readonly SqlDataAccess _sql;

    public Repository(SqlDataAccess sql)
    {
        _sql = sql;
    }
    public async Task<TEntity> GetByIdAsync(string storedProcedure, int id)
    {
        var results = await _sql.LoadData<TEntity, int>(storedProcedure, id);

        return results.FirstOrDefault(); 
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(string storedProcedure, dynamic parameters)
    {
        return await _sql.LoadData<TEntity, dynamic>(storedProcedure, parameters);
    }
    
    //public async Task<IEnumerable<TEntity>> GetAsync(string storedProcedure, Expression<Func<TEntity, bool>> predicate)
    //{

    //    return await _sql.LoadData

    //    return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    //}
    public async Task<TEntity> AddAsync(string storedProcedure, dynamic parameters)
    {
        var results = await _sql.LoadData<TEntity, dynamic>(storedProcedure, parameters);

        return results.FirstOrDefault();
    }
    //public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    //{

    //    await _context.Set<TEntity>().AddRangeAsync(entities);
    //    await _context.SaveChangesAsync();
    //}
    public void Update(string storedProcedure, dynamic parameters)
    {
        _sql.SaveData<TEntity>(storedProcedure, parameters);
    }
    public void Delete(string storedProcedure, dynamic parameters)
    {
        _sql.SaveData<TEntity>(storedProcedure, parameters);
    }
    //public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    //{
    //    _context.Set<TEntity>().RemoveRange(entities);
    //    await _context.SaveChangesAsync();
    //}
}