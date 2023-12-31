﻿using FlashLeit_API.Repositories.Interfaces;
using FlashLeit_API.Services;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Mvc;


namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CollectionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicKeyService _keyService;

    public CollectionsController(IUnitOfWork unitOfWork, IPublicKeyService keyService)
    {
        _unitOfWork = unitOfWork;
        _keyService = keyService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Collections.GetAllAsync("dbo.spCollections_GetAll", new { }));
    }

    [HttpGet("{collectionId}/user/{userId}")]
    public async Task<IActionResult> Get(int collectionId, int userId)
    {
        var collection = await _unitOfWork.Collections.GetCollectionWithCardsAsync("dbo.spCollections_GetById", collectionId, userId);

        return (collection != null) ? Ok(collection) : NotFound();
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetCollectionsByUserId(int id)
    {
        var collections = await _unitOfWork.Collections.GetByIdAsync("dbo.spCollections_GetUserCollections", new {Id = id});

        return collections != null ? Ok(collections) : NotFound("No collections found");
    }

    [HttpGet("author/{id}")]
    public async Task<IActionResult> GetCollectionsByAuthorId(int id)
    {
        var collections = await _unitOfWork.Collections.GetByIdAsync("dbo.spCollections_GetAuthoredCollections", new {UserId = id});

        return collections != null ? Ok(collections) : NotFound("No collections found");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CollectionModel collection)
    {
        if(collection != null)
        {
            var results = await _unitOfWork.Collections.AddAsync("dbo.spCollections_Insert", new
            {
                UserId = collection.UserId,
                Title = collection.Title,
                Description = collection.Description,
                IsPublic = collection.IsPublic
            });

            var id = results.FirstOrDefault()?.Id;

            if(id.HasValue)
            {
                return Ok(new { Id = id.Value });
            }
        }

        return BadRequest();
    }

    [HttpPost("clone/{id}")]
    public async Task<IActionResult> CloneCollection(int id, [FromBody] CollectionModel collection)
    {

        var results = await _unitOfWork.Collections.AddAsync("dbo.spCollections_Clone", new
        {
            AuthorId = collection.UserId,
            PublicKey = _keyService.ConstructPublicKey(collection.UserId, collection.Id),
            UserId = id,
            Title = collection.Title,
            Description = collection.Description
        });

        return results != null ? Ok() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CollectionModel collection)
    {
        var affectedRows = await _unitOfWork.Collections.Update("dbo.spCollections_Update", new
        {
            PublicKey = _keyService.ConstructPublicKey(id, collection.Id),
            Title = collection.Title
        }) ;

        return affectedRows > 0 ? Ok() : NotFound();
    }

    [HttpPut("update-counter/{id}/{category}")]
    public async Task<IActionResult> UpdateCounter(int id, string category)
    {
        var affectedRows = await _unitOfWork.Collections.Update($"dbo.spCollections_{category}", new { Id = id});

        return affectedRows > 0 ? Ok() : NotFound();
    }

    [HttpDelete("/delete/{id}")]
    public async Task<IActionResult> DeletePrivateCollection(int id)
    {
        var affectedRows = await _unitOfWork.Collections.Delete("dbo.spCollections_DeletePrivateCollectionById", new { Id = id });

        return affectedRows > 0 ? Ok(affectedRows) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
    {
        var affectedRows = await _unitOfWork.Collections.Delete("dbo.spCollections_DeleteByPublicKey", new { PublicKey = _keyService.ConstructPublicKey(userId, id) });


        return affectedRows > 0 ? Ok() : NotFound();
    }
}
