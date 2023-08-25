using FlashLeit_API.Repositories.Implementations;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public CardsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // GET: api/<CardsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Cards.GetAllAsync("dbo.spAchievments_GetAll", null));
    }

    // GET api/<CardsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var card = await _unitOfWork.Cards.GetByIdAsync("dbo.spCards_GetById", id);

        return (card != null) ? Ok(card) : NotFound();

    }

    // POST api/<CardsController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CardModel card)
    {
        if (card != null)
        {
            await _unitOfWork.Cards.AddAsync("dbo.spCards_Insert", card);
            return Ok(card);
        }

        else return BadRequest();
    }

    // PUT api/<CardsController>/5
    //[HttpPut("{id}")]
    //public async Task<IActionResult> Put(int id, [FromBody] CardModel card)
    //{
    //    if(card is null || id != card.CardId)
    //    {
           
    //        return BadRequest();
    //    }

    //    int affectedRows = await _unitOfWork.Cards.Update("dbo.spCard_UpdateCard", card);

    //    return (affectedRows == 1) ? Ok(card) : NotFound();
    //}

    // DELETE api/<CardsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int affectedRows = await _unitOfWork.Cards.Delete("dbo.spCards_DeleteById", id);

        return affectedRows > 0 ? Ok("Delete successful") : NotFound("Card not found in the database");
    }

    // NOT IN USE!If affactedrows isn't working then Consider using for deleting in scenarios where cards are allready loaded
    //public async void Delete([FromBody] CardModel card)
    //{
    //    if (card != null)
    //    {
    //        _unitOfWork.Cards.Delete("dbo.spCard_DeleteById", card);
    //    }
    //}
}
