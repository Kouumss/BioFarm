using BioFarm.Core.Entities;
using BioFarm.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BioFarm.Infrastructure.Data.Repositories;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public bool ProductExists(Guid id)
    {
        return context.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(x => x.Brand)
        .Distinct()
        .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(x => x.Type)
        .Distinct()
        .ToListAsync();
    }
}
