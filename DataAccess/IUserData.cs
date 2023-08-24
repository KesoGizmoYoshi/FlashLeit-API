using flashleit_class_library.Models;

namespace FlashLeit_API.DataAccess;
public interface IUserData
{
    Task<List<UserModel>> GetById(int userId);
}