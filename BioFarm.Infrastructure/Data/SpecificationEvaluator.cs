using BioFarm.Core.Entities;
using BioFarm.Core.Interfaces;

namespace BioFarm.Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria); // x => x.Brand == brand
        }

        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        return query;
    }
}
