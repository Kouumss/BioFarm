using System.Linq.Expressions;

namespace BioFarm.Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}
