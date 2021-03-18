using WeTravel.Domain;
using System;
using System.Collections.Generic;
using WeTravel.DataAccessInterface;
using System.Linq;
using System.Linq.Expressions;
using WeTravel.Model;
using Microsoft.EntityFrameworkCore;
using WeTravel.Domain.Exceptions;

namespace WeTravel.DataAccess
{
    public class TouristLocationRepository : ITouristLocationRepository
    {
        private readonly DbContext _context;
        private readonly IFilterRepository<TouristLocation, TouristLocationModelFilter> _filter;

        public TouristLocationRepository(DbContext context, IFilterRepository<TouristLocation, TouristLocationModelFilter> filter)
        {
            _context = context;
            _filter = filter;
        }

        public void Create(TouristLocation touristLocation)
        {
            CheckRegionExists(touristLocation.Region);
            CheckCategoriesExists(touristLocation.TouristLocationCategories.Select(c => new Category() { Id = c.CategoryId }).ToList());
            _context.Attach(touristLocation.Region);
            _context.Set<TouristLocation>().Add(touristLocation);
        }

        private void CheckRegionExists(Region region)
        {
            if (!_context.Set<Region>().Contains(region))
            {
                throw new InvalidOperationExceptionBeautifier("region does exists");
            }
        }

        private void CheckCategoriesExists(IEnumerable<Category> categories)
        {
            var categoriesList = _context.Set<Category>().ToList();
            var categoriesFiltered = categories.Except(categoriesList);
            if (categoriesFiltered.Any())
            {
                throw new InvalidOperationExceptionBeautifier("category does exists");
            }
        }

        public IEnumerable<TouristLocation> Get(TouristLocationModelFilter filter)
        {
            filter = _filter.CheckFilterEmpty(filter);
            var filterQuery = _filter.GetExpressionFromFilter(filter);

            return filterQuery != null ?
                GetFilteredList(filterQuery) :
                GetList();
        }

        public TouristLocation GetById(Guid id)
        {
            var touristLocation = new TouristLocation() { Id = id };
            if (_context.Set<TouristLocation>().Contains(touristLocation))
            {
                return _context.Set<TouristLocation>().Where(l => l.Id == id).Include(t => t.Region).Include(t => t.TouristLocationCategories).FirstOrDefault();
            }
            else
            {
                throw new InvalidOperationExceptionBeautifier("tourist location does exists");
            }
        }
        
        private List<TouristLocation> GetList()
        {
            return _context.Set<TouristLocation>().Include(t => t.Region).Include(t => t.TouristLocationCategories).ToList();
        }

        private List<TouristLocation> GetFilteredList(Expression<Func<TouristLocation, bool>> filterQuery)
        {
            return _context.Set<TouristLocation>().Where(filterQuery).Include(t => t.Region).Include(t => t.TouristLocationCategories).ToList();
        }
    }
}


