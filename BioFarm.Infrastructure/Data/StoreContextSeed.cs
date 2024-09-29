using System.Text.Json;
using BioFarm.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BioFarm.Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, ILogger<StoreContextSeed> logger, IConfiguration configuration)
    {
        try
        {
            if (!context.Products.Any())
            {
                var relativePath = configuration.GetValue<string>("SeedData:ProductsFilePath");

                if (string.IsNullOrEmpty(relativePath))
                {
                    logger.LogError("Relative path is null or empty.");
                    return;
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

                await using var stream = File.OpenRead(filePath);
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(stream);

                if (products?.Any() == true)
                {
                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Products added successfully.");
                }
                else
                {
                    logger.LogWarning("No products found in the seed data.");
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            logger.LogError(ex, "File not found: {FilePath}", configuration.GetValue<string>("SeedData:ProductsFilePath"));
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "JSON deserialization error for file: {FilePath}", configuration.GetValue<string>("SeedData:ProductsFilePath"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while seeding the database.");
        }
    }
}


// public static async Task SeedAsync(StoreContext context)
// {
//     if (!context.Products.Any())
//     {
//         // Get the base directory 
//         var basePath = AppContext.BaseDirectory;

//         // Dynamically construct the path
//         var filePath = Path.Combine(basePath, "Data", "SeedData", "products.json");

//         var productsData = await File.ReadAllTextAsync(filePath);
//         var products = JsonSerializer.Deserialize<List<Product>>(productsData);

//         if (products is not null)
//         {
//             context.Products.AddRange(products);
//             await context.SaveChangesAsync();
//         }
//     }
// }

