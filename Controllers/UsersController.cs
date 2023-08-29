using FlashLeit_API.Models.B2CRelatedModels;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
    public async Task<IActionResult> Post([FromBody] RegistrationClaimsModel claims)
    {
        SuccessfulValidationModel successfulValidation = new();

        JsonResult response = new(new { successfulValidation });

        IEnumerable<UserModel> dbUsers = await _unitOfWork.Users.GetAllAsync("dbo.spUsers_GetAll", new { });

        UserModel? dbUser = dbUsers.FirstOrDefault(u => u.Email == claims.Email);

        if (dbUser is not null)
        {
            return Ok(response);
        }

        dbUser = dbUsers.FirstOrDefault(u => u.AccountName == claims.DisplayName);

        if (dbUser is null)
        {
            var newUser = await _unitOfWork.Users.AddAsync("dbo.spUsers_Insert", new
            {
                Email = claims.Email,
                AccountName = claims.DisplayName,
                Username = claims.DisplayName,
                AvatarUrl = "DefaultAvatarUrl.png"
            });

            var userModelId = newUser.FirstOrDefault().Id;

            successfulValidation.Extension_UserId = userModelId.ToString();

            response = new(new { successfulValidation });

            return Ok(response);
        }

        FailedValidationModel failedValidation = new();

        response = new(new { failedValidation });

        return BadRequest(response);
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
