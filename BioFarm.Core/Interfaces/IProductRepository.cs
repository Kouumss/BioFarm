using BioFarm.Core.Entities;

namespace BioFarm.Core.Interfaces;

public interface IProductRepository
{   
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(Guid id);
    Task<bool>SaveChangesAsync();
}
