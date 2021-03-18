using System;
using System.Linq.Expressions;

namespace WeTravel.DataAccessInterface
{
    public interface IFilterRepository<Entity, ModelFilter>
    {
        ModelFilter CheckFilterEmpty(ModelFilter filter);
        Expression<Func<Entity, bool>> GetExpressionFromFilter(ModelFilter filter);
    }
}
