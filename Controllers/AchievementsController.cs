using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FlashLeit_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AchievementsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AchievementsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Achievements.GetAllAsync("dbo.spAchievements_GetAll", new { }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        IEnumerable<AchievementModel>? achievements = await _unitOfWork.Achievements.GetByIdAsync("dbo.spAchievements_GetAchievementsByUserId", new { Id = id });

        return achievements != null ? Ok(achievements) : NotFound();

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Post(int id, [FromQuery] int achievementId)
    {
        var result = await _unitOfWork.Achievements.AddAsync("dbo.spUserAchievements_InsertUserAchievementRelation", new { UserId = id, AchievementId = achievementId });

        return result != null ? Ok() : BadRequest();
    }
}
