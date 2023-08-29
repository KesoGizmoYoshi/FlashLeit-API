using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.Identity.Client.Extensibility;

namespace FlashLeit_API.Repositories.Implementations;

public class CardRepository : Repository<CardModel>, ICardRepository
{

    // Implementation of application specific operations.

    private readonly ISqlDataAccess _sql;

    public CardRepository(ISqlDataAccess sql) : base(sql)
    {
        _sql = sql;
    }
}
