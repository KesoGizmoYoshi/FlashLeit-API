using FlashLeit_API.Data.Database;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Implementations;

public class UserStatsRepository: Repository<UserStatsModel>, IUserStatsRepository
{
    private readonly AppDbContext _context;


    public UserStatsRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
