using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.DataAccess
{
    public class ReserveRepository : IReserveRepository
    {
        private DbContext _context;

        public ReserveRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(Domain.Reserve reserve)
        {
            _context.Set<Domain.Reserve>().Add(reserve);
        }

        public Domain.Reserve GetById(Guid id)
        {
            if (ReserveExistsById(id))
            {
                return _context.Set<Domain.Reserve>().Where(r => r.Id == id).Include(r => r.ReserveDescription).Include(r => r.Lodging).FirstOrDefault();
            }
            else
            {
                throw new InvalidOperationExceptionBeautifier("reserve exists");
            }
        }

        public IEnumerable<Domain.Reserve> GetReserves(TouristLocationReportFilter touristLocationReportFilter)
        {
            var filter = new ReserveFilter();

            return _context.Set<Domain.Reserve>().Include(r => r.Lodging).Where(filter.GetExpressionFromFilter(touristLocationReportFilter)).ToList();
        }

        public void UpdateState(ReserveDescription reserveDescription)
        {
            Domain.Reserve reserve = GetById(reserveDescription.ReserveId);
            reserve.ReserveDescription = reserveDescription;
            _context.Set<Domain.Reserve>().Update(reserve);
        }

        private bool ReserveExistsById(Guid id)
        {
            Domain.Reserve reserve = new Domain.Reserve() { Id = id };
            return _context.Set<Domain.Reserve>().Contains(reserve);
        }
    }
}


