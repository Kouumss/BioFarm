using BioFarm.Core.Entities;
using BioFarm.Core.Interfaces;
using BioFarm.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace BioFarm.API.Controllers;

public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
{
    #region Methods 

    // GET ALL
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams specParams) // With [APICONTROLLER] request are by Default FromBody   
    {
        var spec = new ProductSpecification(specParams);
        return await CreatePageResult(repo, spec, specParams.PageIndex, specParams.PageSize);
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
        var spec = new BrandListSpecification();

        return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();

        return Ok(await repo.ListAsync(spec));
    }

    private async Task<bool> ProductExists(Guid id)
    {
        return await repo.ExistsAsync(id);
    }
    #endregion

}
