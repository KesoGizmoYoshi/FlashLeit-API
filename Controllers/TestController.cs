using FlashLeit_API.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
}
