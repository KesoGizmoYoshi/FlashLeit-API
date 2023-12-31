﻿using FlashLeit_API.Repositories.Implementations;
using FlashLeit_API.Repositories.Interfaces;
using FlashLeit_API.Services;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Cards.GetAllAsync("dbo.spCards_GetAll", null));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var card = await _unitOfWork.Cards.GetByIdAsync("dbo.spCards_GetById", new {Id = id});

        return (card != null) ? Ok(card) : NotFound();

    }

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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CardModel card)
    {
        if (card != null)
        {
            int affectedRows = await _unitOfWork.Cards.Update("dbo.spCards_Update", new
            {
                PublicKey = _keyService.ConstructPublicKey(id, card.Id),
                Question = card.Question,
                Answer = card.Answer
            });

            return (affectedRows == 1) ? Ok() : NotFound();

        }

        return BadRequest();
    }

    [HttpPut("leitner/{id}")]
    public async Task<IActionResult> PutLeitnerIndex(int id, [FromBody] CardModel card)
    {
        if (card != null)
        {
            int affectedRows = await _unitOfWork.Cards.Update("dbo.spCards_UpdateLeitnerIndex", new
            {
                Id = id,
                LeitnerIndex = card.LeitnerIndex
            });

            return (affectedRows == 1) ? Ok() : NotFound();
        }

        return BadRequest();
    }

    [HttpPut("set-date/{id}")]
    public async Task<IActionResult> SetDate(int id, [FromBody] CardModel card)
    {
        if(card != null)
        {
            int affectedRows = await _unitOfWork.Cards.Update("dbo.spCards_UpdateLastReviewedDate", new
            {
                Id = id,
                NewDate = card.LastReviewedDate
            });

            return (affectedRows == 1) ? Ok() : NotFound();
        }

        return BadRequest();
    } 

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] CardModel cardModel)
    {
        int affectedRows = await _unitOfWork.Cards.Delete("dbo.spCards_DeleteCardByPublicKey",new 
        { 
            PublicKey = _keyService.ConstructPublicKey(cardModel.UserId, cardModel.Id) 
        });

        return affectedRows > 0 ? Ok("Delete successful") : NotFound("Card not found in the database");
    }

}
