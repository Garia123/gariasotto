using WeTravel.Domain;
using System.Collections.Generic;
using WeTravel.DataAccessInterface;
using WeTravel.Model;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeTravel.Domain.Exceptions;

namespace WeTravel.DataAccess
{
    public class LodgingRepository : ILodgingRepository
    {
        private const string PRECONDITION_LODGING_MSG = "lodging exists";
        private const string PRECONDITION_TL_MSG = "tourist location exists";

        private DbContext _context;
        private IFilterRepository<Domain.Lodging, LodgingModelFilter> _filter;

        public LodgingRepository(DbContext context, IFilterRepository<Domain.Lodging, LodgingModelFilter> filter)
        {
            _context = context;
            _filter = filter;
        }

        public void Create(Domain.Lodging lodging)
        {
            _logingExists(lodging, false);
            _checkIfTouristLocationExists(lodging);
            _context.Attach(lodging.TouristLocation);
            _context.Set<Domain.Lodging>().Add(lodging);
        }

        private void _checkIfTouristLocationExists(Domain.Lodging lodging)
        {
            if (!_context.Set<Domain.TouristLocation>().Contains(lodging.TouristLocation))
            {
                throw new InvalidOperationExceptionBeautifier(PRECONDITION_TL_MSG);
            }
        }

        public IEnumerable<Domain.Lodging> Get(LodgingModelFilter filter)
        {
            filter = _filter.CheckFilterEmpty(filter);
            var applyFilters = _filter.GetExpressionFromFilter(filter);

            return applyFilters == null ?
                _context.Set<Domain.Lodging>().Include(l => l.TouristLocation).ToList() :
                _context.Set<Domain.Lodging>().Where(applyFilters).Include(l => l.TouristLocation).ToList();
        }

        public void Delete(Guid id)
        {
            Domain.Lodging lodging = new Domain.Lodging() { Id = id };
            _logingExists(lodging);
            lodging = _context.Set<Domain.Lodging>().Where(l => l.Id == lodging.Id).Include(l => l.TouristLocation).FirstOrDefault();
            lodging.Available = false;
            _context.Set<Domain.Lodging>().Update(lodging);
        }

        public void ChangeStatus(Guid id)
        {
            Domain.Lodging lodging = new Domain.Lodging() { Id = id };
            _logingExists(lodging);
            lodging = _context.Set<Domain.Lodging>().Where(l => l.Id == lodging.Id).Include(l => l.TouristLocation).FirstOrDefault();
            lodging.Available = true;
            _context.Set<Domain.Lodging>().Update(lodging);
        }

        public Domain.Lodging GetById(Guid id)
        {
            Domain.Lodging lodging = new Domain.Lodging() { Id = id };
            _logingExists(lodging);
            return _context.Set<Domain.Lodging>().Where(l => l.Id == id).Include(l => l.TouristLocation).FirstOrDefault();
        }
        
        private void _logingExists(Domain.Lodging lodging, bool wantToExist = true)
        {
            if (wantToExist != _context.Set<Domain.Lodging>().Contains(lodging))
            {
                throw new InvalidOperationExceptionBeautifier(PRECONDITION_LODGING_MSG);
            }
        }
    }
}


