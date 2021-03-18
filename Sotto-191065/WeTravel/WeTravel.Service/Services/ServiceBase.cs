using WeTravel.DataAccessInterface;

namespace WeTravel.Service
{
    public class ServiceBase
    {
        protected readonly IUnitOfWork UnitOfWork;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
} 
