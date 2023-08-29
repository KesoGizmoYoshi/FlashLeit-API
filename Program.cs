using FlashLeit_API.DataAccess;
using FlashLeit_API.Repositories.Implementations;
using FlashLeit_API.Repositories.Interfaces;
using FlashLeit_API.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dapper -- Stored Procedures
builder.Services.AddSingleton<IConnectionStringService, ConnectionStringService>();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options => { options.AddPolicy("CorsPolicy", policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
