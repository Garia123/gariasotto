using System;
using System.Linq.Expressions;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Model;

namespace WeTravel.DataAccess
{
    public class LodgingFilter : IFilterRepository<Domain.Lodging, LodgingModelFilter>
    {
        public LodgingModelFilter CheckFilterEmpty(LodgingModelFilter filter)
        {
            return filter ?? new LodgingModelFilter();
        }

        public Expression<Func<Domain.Lodging, bool>> GetExpressionFromFilter(LodgingModelFilter filter)
        {
            Expression<Func<Domain.Lodging, bool>> applyFilters = null;

            if (filter.TouristLocationId != Guid.Empty)
            {
                applyFilters = l => l.TouristLocation.Id == filter.TouristLocationId;
            }

            return applyFilters;
        }
    }
}
