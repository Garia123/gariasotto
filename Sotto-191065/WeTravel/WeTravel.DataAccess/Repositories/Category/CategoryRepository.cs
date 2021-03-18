using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;

namespace WeTravel.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        private DbContext _context;

        public CategoryRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Get()
        {
            return _context.Set<Category>().ToList();
        }
    }
}



