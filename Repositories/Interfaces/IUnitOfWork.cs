namespace FlashLeit_API.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{

    public ICardRepository Cards { get; set; }



    Task CompleteAsync();

    void Dispose();
}
