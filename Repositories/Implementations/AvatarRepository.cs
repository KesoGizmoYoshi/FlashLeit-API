using FlashLeit_API.DataAccess;
using FlashLeit_API.Models;
using FlashLeit_API.Repositories.Interfaces;

namespace FlashLeit_API.Repositories.Implementations;

public class AvatarRepository : Repository<AvatarModel>, IAvatarRepository
{

    private readonly ISqlDataAccess _sql;


    public AvatarRepository(ISqlDataAccess sql) : base(sql)
    {
        _sql = sql;
    }
}
