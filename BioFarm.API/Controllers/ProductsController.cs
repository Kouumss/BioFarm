using BioFarm.Core.Entities;
using BioFarm.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BioFarm.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{   
    #region Properties
    private readonly StoreContext context;

    #endregion

    #region Constructor
    public ProductsController(StoreContext context)
    {
        this.context = context;
    }

    #endregion


    #region Crud 


    // GET ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await context.Products.ToListAsync();
    }
    // GET BY ID
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
            return NotFound();
        return product;
    }
    // CREATE
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        return product;
    }
    // UPDATE
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Product>> UpdateProduct(Guid id, Product product)
    {

        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");

        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {

        var product = await context.Products.FindAsync(id);
        if (product is null)
            return NotFound();
        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return NoContent();
    }
    #endregion

    #region Private Method

    // CHECK IF PRODUCT EXIST
    private bool ProductExists(Guid id)
    {
        return context.Products.Any(p => p.Id == id);
    }
    #endregion

}
