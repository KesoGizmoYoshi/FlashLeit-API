﻿using Azure.Identity;
using FlashLeit_API.Models.B2CRelatedModels;
using FlashLeit_API.Repositories.Interfaces;
using flashleit_class_library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.IdentityModel.Tokens.Jwt;

namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Users.GetAllAsync("dbo.spUsers_GetAll", new { }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _unitOfWork.Users.GetByIdAsync("dbo.spUsers_GetById", new {Id = id}));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistrationClaimsModel claims)
    {
        IEnumerable<UserModel> dbUsers = await _unitOfWork.Users.GetAllAsync("dbo.spUsers_GetAll", new { });

        UserModel? dbUser = dbUsers.FirstOrDefault(u => u.Email == claims.Email);

        if (dbUser is not null)
        {
            return new OkObjectResult(new { version = "1.0.0", action = "Continue" });
        }

        dbUser = dbUsers.FirstOrDefault(u => u.AccountName == claims.DisplayName);

        if (dbUser is null)
        {
            var newUser = await _unitOfWork.Users.AddAsync("dbo.spUsers_Insert", new
            { 
                Email = claims.Email,
                AccountName = claims.DisplayName,
                Username = claims.DisplayName,
                SelectedAvatarUrl = $"/img/user_avatars/avatar_{new Random().Next(1, 5)}.png"
            });

            int userId = newUser.FirstOrDefault()!.Id;

            return new OkObjectResult(new { version = "1.0.0", action = "Continue", extension_UserID = userId });
        }

        return new BadRequestObjectResult(new { version = "1.0.0", status = 400, action = "ValidationError", userMessage = "Username is already in use!" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UserModel user)
    {
        if (user != null)
        {
            int affectedRows = await _unitOfWork.Users.Update("dbo.spUsers_Update", new
            {
                Id = id,
                NewUserName = user.UserName,
                NewSelectedAvatarUrl = user.SelectedAvatarUrl
            });

            return affectedRows > 0 ? Ok() : NotFound();
        }

        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromHeader] string authorization)
    {
        // This endpoint utilize claims in the ID-Token from the "authorization" request header.
        // And looks like this: "authorization": "Bearer {The signed in users ID-Token}"
        // We need the following claims from the ID-Token:
        // 1. Object ID (claim "sub" in the token), this is used for deleting a user from Azure AD B2C.
        // 2. User ID (claim "extension_UserId" in the token), this is used for deleting the user from out database.
        // We need to use the Microsoft Graph Api for deleting users from B2C

        // Decodes the ID-Token from "authorization" in the header to a JSON Web Token (JWT).
        // This need to be done so we can access the claims we need.
        // Starts at index 7 since "authorization" also includes "Bearer ".
        JwtSecurityToken token = new(authorization.Substring(7));

        // Extracts the value for the claims we are interested in from the JWT.
        string objectId = token.Claims.First(c => c.Type == "sub").Value;
        string userId = token.Claims.First(c => c.Type == "extension_UserId").Value;

        // Default endpoint for the Microsoft Graph Api
        string[] scopes = { "https://graph.microsoft.com/.default" };

        // Values taken from out application registered in Azure AD B2C
        string clientId = "24fdaf11-b4f2-467d-af76-2a8596c9c0ab";
        string tenantId = "bf84b024-37c0-4f78-adf1-d182afd9c876";
        string clientSecret = "FOm8Q~4WFcpv2xkwO_hMI0mwAmgRIb7o_kk29cFU";
        
        // This is used to authenticate against Azure AD with our application values
        ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        // This is used to make api calls to the Graph Api
        GraphServiceClient graphClient = new GraphServiceClient(clientSecretCredential, scopes);

        // This deletes the user from the Azure AD B2C
        await graphClient.Users[objectId].DeleteAsync();

        int affectedRows = await _unitOfWork.Users.Delete("dbo.spUsers_DeleteById", new { Id = userId });

        return affectedRows > 0 ? Ok() : NotFound("User doesn't exist in the database");
    }
}
