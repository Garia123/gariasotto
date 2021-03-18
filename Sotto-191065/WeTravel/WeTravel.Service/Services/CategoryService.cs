using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Model;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }

        public IEnumerable<CategoryModelOut> Get()
        {
            return UnitOfWork.CategoryRepository.Get().Select(c => _categoryModelOut(c));
        }

        private CategoryModelOut _categoryModelOut(Category c)
        {
            return new CategoryModelOut()
            {
                Id = c.Id,
                Name = c.Name
            };
        }
    }
}
