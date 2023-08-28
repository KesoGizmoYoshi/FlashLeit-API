using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/<UsersController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Users.GetAllAsync("dbo.spUsers_GetAll", new { }));
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _unitOfWork.Users.GetByIdAsync("dbo.spUsers_GetById", new {Id = id}));
    }

    // POST api/<UsersController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UserModel user)
    {
        if (user != null)
        {
            int affectedRows = await _unitOfWork.Users.Update("dbo.spUsers_Update", new
            {
                Id = id,
                NewUserName = user.UserName,
                NewAvatarUrl = user.AvatarUrl
            });

            return affectedRows > 0 ? Ok() : NotFound();
        }

        return BadRequest();
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int affectedRows = await _unitOfWork.Users.Delete("dbo.spUsers_DeleteById", new { Id = id });

        return affectedRows > 0 ? Ok() : NotFound("User doesn't exist in the database");
    }
}
