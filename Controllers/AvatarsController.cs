using FlashLeit_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AvatarsController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;

    public AvatarsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/<AvatarsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var avatars = await _unitOfWork.Avatars.GetAllAsync("dbo.spAvatars_GetAll", new {});

        return avatars != null ? Ok(avatars) : NotFound("No avatars found");
    }

}
