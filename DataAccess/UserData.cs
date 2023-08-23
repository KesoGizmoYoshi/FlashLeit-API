using flashleit_class_library.Models;

namespace FlashLeit_API.DataAccess;

public class UserData
{
    private readonly ISqlDataAccess _sql;

    public UserData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    //public List<UserModel> GetAll()
    //{
    //    _sql.LoadData<UserModel, dynamic>("dbo.spUsers_GetAll");
    //} 

    public Task<List<UserModel>> GetById(int userId)
    {
        return _sql.LoadData<UserModel, dynamic>(
            "dbo.spUsers_GetById",
            new { UserId = userId });
    }
}
