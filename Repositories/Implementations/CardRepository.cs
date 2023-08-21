using FlashLeit_API.Data.Database;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.Identity.Client.Extensibility;

namespace FlashLeit_API.Repositories.Implementations;

public class CardRepository : Repository<CardModel>, ICardRepository
{

    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
