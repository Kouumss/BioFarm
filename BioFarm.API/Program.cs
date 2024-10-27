using BioFarm.API.Middleware;
using BioFarm.Core.Interfaces;
using BioFarm.Infrastructure.Data;
using BioFarm.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BioFarm"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

//Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.MapControllers();


var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{ 
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context, services.GetRequiredService<ILogger<StoreContextSeed>>(), services.GetRequiredService<IConfiguration>());
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during migration and seeding.");
    throw; // Rethrow the exception after logging
}

app.Run();
