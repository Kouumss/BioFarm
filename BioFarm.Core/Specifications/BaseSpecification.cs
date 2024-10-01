using System.Linq.Expressions;
using BioFarm.Core.Interfaces;

namespace BioFarm.Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria => criteria;
}
