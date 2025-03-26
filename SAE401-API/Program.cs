using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using DotNetEnv;
using SAE401_API.Models.Repository;
using SAE401_API.Models.DataManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SAE401_API.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProduitRepository<Produit>, ProduitManager>();
builder.Services.AddScoped<ICategorieRepository<Categorieproduit>, CategorieManager>();
builder.Services.AddScoped<IRegroupementRepository<Regroupementproduit>, RegroupementManager>();
builder.Services.AddScoped<IAttributRepository<Attributproduit>, AttributManager>();
builder.Services.AddScoped<ITransporteurRepository<Transporteur>, TransporteurManager>();
builder.Services.AddScoped<ITypePaiementRepository<Typepaiement>, TypePaiementManager>();
builder.Services.AddScoped<ICodePromoRepository<Codepromo>, CodePromoManager>();
builder.Services.AddScoped<IDetailPanierRepository<Detailpanier>, DetailPanierManager<Detailpanier>>();
builder.Services.AddScoped<ICommandeRepository<Commande>, CommandeManager<Commande>>();
builder.Services.AddScoped<IHistoriqueconsultationRepository<Historiqueconsultation>, HistoriqueconsultationManager<Historiqueconsultation>>();
builder.Services.AddScoped<IClientRepository<Client>, ClientManager>();
builder.Services.AddScoped<IAdresseRepository<Adresse>, AdresseManager<Adresse>>();
builder.Services.AddScoped<IProduitsimilaireRepository<Produitsimilaire>, ProduitsimilaireManager>(); // Lier l'interface au manager


// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Pour ajouter la chaine de connexion, ajoutez un fichier .env avec comme contenu : CONNECTION_STRING="..."
builder.Services.AddDbContext<_DBMilibooContext>(options =>
  options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
         ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))),
         ClockSkew = TimeSpan.Zero
     };
 });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("Login", Policies.LoginPolicy());
});

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
