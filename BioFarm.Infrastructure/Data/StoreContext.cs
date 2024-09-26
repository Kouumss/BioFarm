using BioFarm.Core.Entities;
using BioFarm.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace BioFarm.Infrastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{   
    public DbSet<Product> Products{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
