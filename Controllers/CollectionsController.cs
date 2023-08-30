using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

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

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCollectionsByUserId(int id)
    {
        var collections = await _unitOfWork.Collections.GetCollectionsByUserIdAsync("dbo.spUserCollection_GetUserCollections", id);

        return Ok(collections);
    }


    // POST api/<CollectionsController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CollectionModel collection)
    {
        if(collection != null)
        {
            var newCollection = await _unitOfWork.Collections.AddAsync("dbo.spCollections_Insert", new
            {
                UserId = collection.UserId,
                Title = collection.Title,
                IsPublic = collection.IsPublic
            });

            var collectionId = newCollection.FirstOrDefault().Id;

            return Ok(collectionId);
        }

        return BadRequest();
    }

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> CloneCollection(int id, [FromBody] CollectionModel model)
    {
        var results = await _unitOfWork.Collections.AddAsync("dbo.spCollections_Clone", new 
        { 
            PublicKey = model.PublicKey,
            AuthorId = id,
            UserId = model.UserId,
            Title = model.Title
        });


        return results != null ? Ok() : NotFound();
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
    public async Task<IActionResult> Delete(int id, [FromBody] int userId)
    {
        var affectedRows = await _unitOfWork.Collections.Delete("dbo.spCollections_DeleteById", new { CollectionId = id, UserId = userId});


        return affectedRows > 0 ? Ok() : NotFound();
    }
}
