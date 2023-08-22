using FlashLeit_API.Data.Database;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Implementations;

public class AchievementRepository: Repository<AchievementModel>, IAchievementRepository
{
    private readonly AppDbContext _context;

    public AchievementRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
