using FlashLeit_API.Data.Database;
using FlashLeit_API.Repositories.Interfaces;

namespace FlashLeit_API.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ICardRepository Cards { get; set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Cards = new CardRepository(context);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }


}
