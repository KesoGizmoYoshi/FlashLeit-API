using FlashLeit_API.Repositories.Implementations;
using FlashLeit_API.Repositories.Interfaces;
using FlashLeit_API.Services;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicKeyService _keyService;
    public CardsController(IUnitOfWork unitOfWork, IPublicKeyService keyService)
    {
        _unitOfWork = unitOfWork;
        _keyService = keyService;
    }
    // GET: api/<CardsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Cards.GetAllAsync("dbo.spCards_GetAll", null));
    }

    // GET api/<CardsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var card = await _unitOfWork.Cards.GetByIdAsync("dbo.spCards_GetById", new {Id = id});

        return (card != null) ? Ok(card) : NotFound();

    }

    // POST api/<CardsController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CardModel card)
    {
        if (card != null)
        {
            await _unitOfWork.Cards.AddAsync("dbo.spCards_Insert", new
            {
                CollectionId = card.CollectionId,
                UserId = card.UserId,
                CollectionPublicKey = _keyService.ConstructPublicKey(card.UserId, card.CollectionId),
                Question = card.Question,
                Answer = card.Answer,
                
            });

            return Ok();
        }

        else return BadRequest();
    }

    // PUT api/<CardsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CardModel card)
    {
        if (card != null)
        {
            int affectedRows = await _unitOfWork.Cards.Update("dbo.spCards_Update", new
            {
                Id = id,
                CollectionId = card.CollectionId,
                Question = card.Question,
                Answer = card.Answer
            });

            return (affectedRows == 1) ? Ok(card) : NotFound();

        }

        return BadRequest();
    }

    // DELETE api/<CardsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int affectedRows = await _unitOfWork.Cards.Delete("dbo.spCards_DeleteById", new { Id = id });

        return affectedRows > 0 ? Ok("Delete successful") : NotFound("Card not found in the database");
    }

}
