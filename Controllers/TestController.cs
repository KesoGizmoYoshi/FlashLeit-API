using FlashLeit_API.Data.Database;
using FlashLeit_API.Models.B2CRelatedModels;
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

    public TestController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("woop")]
    public async Task<IEnumerable<TestModel>> Get()
    {
        return await _context.TestTable.ToListAsync();
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
