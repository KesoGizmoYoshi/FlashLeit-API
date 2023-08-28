using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace FlashLeit_API.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class TestController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserData _data;


    public TestController(AppDbContext context, IUnitOfWork unitOfWork, IUserData data)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _data = data;
    } 

    [HttpGet("woop")]
    public async Task<IActionResult> Get()
    {
        //return await _context.TestTable.ToListAsync();

        

        return Ok(await _unitOfWork.Users.GetAllAsync("dbo.spUsers_GetAll", new { }));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        //return await _context.TestTable.ToListAsync();

        //dynamic userId = new { UserId = id };

        return Ok(await _unitOfWork.Users.GetByIdAsync("dbo.spUsers_GetById", new { Id = id}));

        //return Ok(await _data.GetById(id));


    }

    [HttpPost("validation")]
    public async Task<IActionResult> Post([FromBody] RegistrationClaimsModel claims)
    {
        SuccessfulValidationResponseModel successfulValidationResponse = new()
        {
            Version = "1.0.0",
            Action = "Continue",
        };

        FailedValidationResponseModel failedValidationResponse = new()
        {
            Version = "1.0.0",
            Status = "400",
            Action = "ValidationError",
            UserMessage = "Username is already in use!"
        };


        JsonResult responseJson = new(new { successfulValidationResponse });

        SummaryModel? dbUser = await _context.Summaries.FirstOrDefaultAsync(u => u.Email == claims.Email);

        if(dbUser is not null)
        {
            return Ok(responseJson);
        }

        dbUser = await _context.Summaries.FirstOrDefaultAsync(u => u.Name == claims.DisplayName);

        if (dbUser is null)
        {
            await _context.Summaries.AddAsync(new SummaryModel
            {
                Email = claims.Email!,
                Name = claims.DisplayName!
            });

            await _context.SaveChangesAsync();

            return Ok(responseJson);
        }

        responseJson = new(new { failedValidationResponse });

        return BadRequest(responseJson);
    }
}
