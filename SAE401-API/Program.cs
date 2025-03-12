using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Pour ajouter la chaine de connexion, ajoutez un fichier .env avec comme contenu : CONNECTION_STRING="..."
builder.Services.AddDbContext<_DBMilibooContext>(options =>
  options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

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
