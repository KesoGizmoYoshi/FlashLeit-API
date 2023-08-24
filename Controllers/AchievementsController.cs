﻿using FlashLeit_API.Repositories.Interfaces;
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

    // GET: api/<AchievementsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Achievements.GetAllAsync("dbo.spAchievments_GetAll"));
    }

    // GET api/<AchievementsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        AchievementModel? achievement = await _unitOfWork.Achievements.GetByIdAsync("dbo.spAchievments_GetById", id);
        
        return (achievement is null) ? NotFound() : Ok(achievement);
    }

    // POST api/<AchievementsController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AchievementModel achievement)
    {
        if (achievement is null)
        {
            return BadRequest();
        }

        await _unitOfWork.Achievements.AddAsync("dbo.spAchievments_Create", achievement);

        return Ok(achievement);
    }

    // PUT api/<AchievementsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AchievementModel achievement)
    {
        if (achievement is null || !id.Equals(achievement.AchievementId)) return BadRequest();

        bool isUpdated = await _unitOfWork.Achievements.UpdateAsync("dbo.spAchievments_UpdateAchievement", achievement);

        return isUpdated ? Ok(achievement) : NotFound();
    }

    // DELETE api/<AchievementsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        AchievementModel? achievement = await _unitOfWork.Achievements.GetByIdAsync("dbo.spAchievments_GetById", id);

        if( achievement is null)
        {
            return NotFound();
        }
        
        await _unitOfWork.Achievements.RemoveAsync("dbo.spAchievments_DeleteById", id);

        return Ok(achievement);
    }
}
