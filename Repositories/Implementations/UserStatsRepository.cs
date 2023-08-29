using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Implementations;

public class UserStatsRepository: Repository<UserStatsModel>, IUserStatsRepository
{
    private readonly ISqlDataAccess _sql;

    public UserStatsRepository(ISqlDataAccess sql) : base(sql)
    {
        _sql = sql;
    }
}
