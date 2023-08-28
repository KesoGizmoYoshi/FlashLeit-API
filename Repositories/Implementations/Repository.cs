using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace FlashLeit_API.Repositories.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{

    protected readonly ISqlDataAccess _sql;

    public Repository(ISqlDataAccess sql)
    {
        _sql = sql;
    }
    public async Task<IEnumerable<TEntity>> GetByIdAsync(string storedProcedure, dynamic parameters)
    {
        var results = await _sql.LoadData<TEntity, dynamic>(storedProcedure, parameters);

        return results; 
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
    public async Task<IEnumerable<TEntity>> AddAsync(string storedProcedure, dynamic parameters)
    {
        var results = await _sql.LoadData<TEntity, dynamic>(storedProcedure, parameters);

        return results;
    }
    //public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    //{

    //    await _context.Set<TEntity>().AddRangeAsync(entities);
    //    await _context.SaveChangesAsync();
    //}
    public Task<int> Update(string storedProcedure, dynamic parameters)
    {
        var affectedRows = _sql.SaveData<dynamic>(storedProcedure, parameters);

        return affectedRows;
    }
    public async Task<int> Delete(string storedProcedure, dynamic parameters)
    {
        var affectedRows = await _sql.SaveData<dynamic>(storedProcedure, parameters);

        return affectedRows;
    }
    //public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    //{
    //    _context.Set<TEntity>().RemoveRange(entities);
    //    await _context.SaveChangesAsync();
    //}
}