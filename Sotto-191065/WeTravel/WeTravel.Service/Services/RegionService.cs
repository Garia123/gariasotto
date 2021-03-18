using WeTravel.Model;
using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class RegionService : ServiceBase, IRegionService
    {
        public RegionService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }

        public IEnumerable<RegionModelOut> Get()
        {
            return UnitOfWork.RegionRepository.Get().Select(r => _categoryModelOut(r));
        }

        private RegionModelOut _categoryModelOut(Region r)
        {
            return new RegionModelOut()
            {
                Id = r.Id,
                Name = r.Name
            };
        }
    }
}
