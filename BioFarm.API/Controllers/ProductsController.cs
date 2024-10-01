using BioFarm.Core.Entities;
using BioFarm.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BioFarm.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{
    #region Methods 

    // GET ALL
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        return Ok(await repo.ListAllAsync());
    }

    // GET BY ID
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        Product? product = await repo.GetByIdAsync(id);
        if (product is null)
            return NotFound();
        return product;
    }
    // CREATE
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await repo.AddAsync(product);

        if (await repo.SaveAllAsync())
            return CreatedAtAction("GetProductById", new { id = product.Id }, product);

        return BadRequest("Problem creating the product");
    }

    // UPDATE
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Product>> UpdateProduct(Guid id, Product product)
    {

        if (product.Id != id || !await ProductExists(id))
            return BadRequest("Cannot update this product");

        await repo.UpdateAsync(product);

        if (await repo.SaveAllAsync())
            return NoContent();

        return BadRequest("Problem updating the product");
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {

        var product = await repo.GetByIdAsync(id);

        if (product is null)
            return NotFound();

        await repo.RemoveAsync(product);

        if (await repo.SaveAllAsync())
            return NoContent();

        return BadRequest("Problem deleting the product");
    }
  

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {   
        //IMPLEMENT 
        return Ok();
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {   
        //IMPLEMENT 
        return Ok();
    }

    private async Task<bool> ProductExists(Guid id)
    {
        return await repo.ExistsAsync(id);
    }
    #endregion

}
