using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Implementations;

public class UserRepository: Repository<UserModel>, IUserRepository
{

    private readonly SqlDataAccess _sql;

    public UserRepository(SqlDataAccess sql)
    {
        _sql = sql;
    }
}
