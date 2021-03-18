using System;
using System.Linq.Expressions;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Model;

namespace WeTravel.DataAccess
{
    public class ReserveFilter : IFilterRepository<Reserve, TouristLocationReportFilter>
    {
        public TouristLocationReportFilter CheckFilterEmpty(TouristLocationReportFilter filter)
        {
            return filter ?? new TouristLocationReportFilter();
        }

        public Expression<Func<Reserve, bool>> GetExpressionFromFilter(TouristLocationReportFilter filter)
        {
            Expression<Func<Reserve, bool>> filterExpression;

            filterExpression = r => r.Lodging.Name.Equals(filter.TouristLocationName) &&
                                    ((r.CheckIn <= filter.StartDate && r.CheckOut <= filter.StartDate) || (r.CheckIn <= filter.EndDate && r.CheckOut <= filter.EndDate));

            return filterExpression;
        }
    }
}
