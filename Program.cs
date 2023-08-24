using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FlashLeit_API.Data.Database;
using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Implementations;
using FlashLeit_API.Repositories.Interfaces;
using FlashLeit_API.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dapper -- Stored Procedures
builder.Services.AddSingleton<IConnectionStringService, ConnectionStringService>();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();


// --- TEST ---
builder.Services.AddSingleton<IUserData, UserData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
