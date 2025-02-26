using System.Threading.RateLimiting;
using DataAccessLayer.Repositories.BlockedAttemps;
using DataAccessLayer.Repositories.BlockedCountries;
using DataAccessLayer.Repositories.TemporalBlockCountries;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using Services.AttempsServices;
using Services.Conteries;
using Services.GeoLocationService;
using Services.LocationService;
using Services.TemporalBlockedCounteryServices;

var builder = WebApplication.CreateBuilder(args);

#region DI Container

builder.Services.AddScoped<IBlockedCountriesRepository, BlockedCountriesRepository>();
builder.Services.AddScoped<IBlockedCountryService, CountryService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IBlockdAttempsRepo, BlockedAttemptsRepo>();
builder.Services.AddScoped<IBlockedAttepmsService, BlockedAttemptsService>(); 
builder.Services.AddScoped<ITemporalBlockCountryRepo, TemporalBlockCountryRepo>();
builder.Services.AddScoped<ITemporalBlockService, TemporalBlockService>();
// Add services to the container.
# endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Register Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Blocked Countries API",
        Version = "v1",
        Description = "API for managing blocked countries and validating IP addresses.",
        Contact = new OpenApiContact
        {
            Name = "Ahmed ELshiekh",
            Email = "ahmedelshiekh190@gmail.com"
        }
    });
});


builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    { 
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(12);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));


builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
