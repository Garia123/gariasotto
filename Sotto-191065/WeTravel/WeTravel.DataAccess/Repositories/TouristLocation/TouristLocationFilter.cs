using System;
using System.Linq;
using System.Linq.Expressions;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Model;

namespace WeTravel.DataAccess
{
    public class TouristLocationFilter : IFilterRepository<Domain.TouristLocation, TouristLocationModelFilter>
    {
        public TouristLocationModelFilter CheckFilterEmpty(TouristLocationModelFilter filter)
        {
            return filter ?? new TouristLocationModelFilter();
        }

        public Expression<Func<Domain.TouristLocation, bool>> GetExpressionFromFilter(TouristLocationModelFilter filter)
        {
            Expression<Func<Domain.TouristLocation, bool>> filterExpression;

            if (filter.RegionId != Guid.Empty && filter.CategoryId != Guid.Empty)
            {
                filterExpression = l => l.Region.Id == filter.RegionId && l.TouristLocationCategories.Where(c => c.CategoryId == filter.CategoryId).Any();
            }
            else if (filter.RegionId != Guid.Empty)
            {
                filterExpression = l => l.Region.Id == filter.RegionId;
            }
            else if (filter.CategoryId != Guid.Empty)
            {
                filterExpression = l => l.TouristLocationCategories.Where(c => c.CategoryId == filter.CategoryId).Any();
            }
            else
            {
                filterExpression = null;
            }

            return filterExpression;
        }
    }
}
