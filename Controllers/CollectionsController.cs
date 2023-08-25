using FlashLeit_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CollectionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CollectionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/<CollectionsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Collections.GetAllAsync("dbo.spCollections_GetAll", new { }));
    }

    // GET api/<CollectionsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var collection = await _unitOfWork.Collections.GetCollectionWithCardsAsync("dbo.spCollections_GetById", id);

        return (collection != null) ? Ok(collection) : NotFound();
    }

    // POST api/<CollectionsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<CollectionsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CollectionsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
