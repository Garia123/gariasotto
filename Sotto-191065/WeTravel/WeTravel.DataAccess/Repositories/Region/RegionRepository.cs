using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;

namespace WeTravel.DataAccess
{
    public class RegionRepository : IRegionRepository
    {
        private DbContext _context;

        public RegionRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Region> Get()
        {
            return _context.Set<Region>().ToList();
        }
    }
}



