using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Implementations;

public class AchievementRepository: Repository<AchievementModel>, IAchievementRepository
{
    private readonly ISqlDataAccess _sql;

    public AchievementRepository(ISqlDataAccess sql) : base(sql)
    {
        _sql = sql;
    }
}
