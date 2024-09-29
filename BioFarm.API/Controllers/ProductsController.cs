using BioFarm.Core.Entities;
using BioFarm.Core.Interfaces;
using BioFarm.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BioFarm.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    #region Methods 

    // GET ALL
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        return Ok(await repo.GetProductsAsync());
    }

    // GET BY ID
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        Product? product = await repo.GetProductByIdAsync(id);
        if (product is null)
            return NotFound();
        return product;
    }
    // CREATE
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);

        if (await repo.SaveChangesAsync())
            return CreatedAtAction("GetProductById", new { id = product.Id }, product);

        return BadRequest("Problem creating the product");
    }

    // UPDATE
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Product>> UpdateProduct(Guid id, Product product)
    {

        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");

        repo.UpdateProduct(product);

        if (await repo.SaveChangesAsync())
            return NoContent();

        return BadRequest("Problem updating the product");
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {

        var product = await repo.GetProductByIdAsync(id);

        if (product is null)
            return NotFound();

        repo.DeleteProduct(product);

        if (await repo.SaveChangesAsync())
            return NoContent();

        return BadRequest("Problem deleting the product");
    }
  

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repo.GetBrandsAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repo.GetTypesAsync());
    }

    private bool ProductExists(Guid id)
    {
        return repo.ProductExists(id);
    }
    #endregion

}
