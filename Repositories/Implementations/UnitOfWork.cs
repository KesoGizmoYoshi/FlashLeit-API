using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;

namespace FlashLeit_API.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISqlDataAccess _sql;

    public ICardRepository Cards { get; set; }
    public IUserRepository Users { get; set; }
    public ICollectionRepository Collections { get; set; }
    public IAchievementRepository Achievements { get; set; }
    public IAvatarRepository Avatars { get; set }

    public UnitOfWork(ISqlDataAccess sql)
    {
        _sql = sql;
        Cards = new CardRepository(_sql);
        Users = new UserRepository(_sql);
        Collections = new CollectionRepository(_sql);
        Achievements = new AchievementRepository(_sql);
        Avatars = new AvatarRepository(_sql);
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
