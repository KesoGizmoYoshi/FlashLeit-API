using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
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


    public async Task<IEnumerable<TEntity>> AddAsync<TEntity>(string storedProcedure, dynamic parameters)
    {
        var results = await _sql.LoadData<TEntity, dynamic>(storedProcedure, parameters);

        return results;
    }

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
}