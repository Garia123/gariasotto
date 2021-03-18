using System;
using System.Collections.Generic;
using System.Text;

namespace WeTravel.DataAccessInterface
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        ILodgingRepository LodgingRepository { get; }
        IRegionRepository RegionRepository { get; }
        IReserveRepository ReserveRepository { get; }
        ITouristLocationRepository TouristLocationRepository { get; }
        IUserRepository UserRepository { get; }
        ISessionRepository SessionRepository { get; }
        IReviewRepository ReviewRepository { get; }
        int Save();
    }
}
