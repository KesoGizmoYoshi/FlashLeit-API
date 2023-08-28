using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
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
    public async Task<IActionResult> Post([FromBody] CollectionModel collectionModel)
    {
        if(collectionModel != null)
        {
            var dbCollectionModel = await _unitOfWork.Collections.AddAsync("dbo.spCollections_Insert", new
            {
                UserId = collectionModel.UserId,
                Title = collectionModel.Title
            });

            return Ok(dbCollectionModel);
        }

        return BadRequest();
    }

    // PUT api/<CollectionsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] string newTitle)
    {
        var affectedRows = await _unitOfWork.Collections.Update("dbo.spCollections_Update", new 
        { 
            Id = id,
            Title = newTitle
        });

        return affectedRows > 0 ? Ok() : NotFound();
    }

    // DELETE api/<CollectionsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
