using FlashLeit_API.Data.Database;
using FlashLeit_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashLeit_API.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class TestController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public TestController(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("woop")]
    public async Task<IActionResult> Get()
    {
        //return await _context.TestTable.ToListAsync();

        

        return Ok(await _unitOfWork.Users.GetAllAsync("dbo.spUsers_GetAll", 1));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        //return await _context.TestTable.ToListAsync();



        return Ok(await _unitOfWork.Users.GetByIdAsync("dbo.spUsers_GetById", 1));
    }
}
