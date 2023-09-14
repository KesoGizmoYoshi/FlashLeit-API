namespace FlashLeit_API.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{

    // Add all Interfaces for Repositories
    public ICardRepository Cards { get; set; }
    public IUserRepository Users { get; set; }
    public ICollectionRepository Collections { get; set; }
    public IAchievementRepository Achievements { get; set; }
    public IAvatarRepository Avatars { get; set; }

    public void Dispose();
}
