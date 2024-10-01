using BioFarm.Core.Entities;

namespace BioFarm.Core.Specifications;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification() {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}
