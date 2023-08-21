using FlashLeit_API.Data.Database;
using FlashLeit_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlashLeit_API.Repositories.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }
    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }
    public async Task AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
    public void Update(TEntity entity)
    {
        _context.Update<TEntity>(entity);
    }
    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}