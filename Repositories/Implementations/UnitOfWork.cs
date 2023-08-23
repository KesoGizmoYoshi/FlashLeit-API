using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;

namespace FlashLeit_API.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly SqlDataAccess _sql;

    public ICardRepository Cards { get; set; }
    public IUserRepository Users { get; set; }
    public ICollectionRepository Collections { get; set; }
    public ICounterRepository Counters { get; set; }
    public IUserStatsRepository UserStats { get; set; }
    public IAchievementRepository Achievements { get; set; }

    public UnitOfWork(SqlDataAccess sql)
    {
        _sql = sql;
        Cards = new CardRepository(sql);
        Users = new UserRepository(sql);
        Collections = new CollectionRepository(sql);
        Counters = new CounterRepository(sql);
        UserStats = new UserStatsRepository(sql);
        Achievements = new AchievementRepository(sql);
    }

    //public async Task CompleteAsync()
    //{
    //    await _context.SaveChangesAsync();
    //}

    public void Dispose()
    {
        // Gives warning if not here, don't know what to implement..
    }


}
