using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Implementations;

public class AchievementRepository: Repository<AchievementModel>, IAchievementRepository
{
    private readonly SqlDataAccess _sql;

    public AchievementRepository(SqlDataAccess sql) : base(sql)
    {
        _sql = sql;
    }
}
