using Microsoft.EntityFrameworkCore;
using System;
using WeTravel.DataAccessInterface;

namespace WeTravel.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        
        public UnitOfWork(
            DbContext context,
            ICategoryRepository categoryRepository,
            ILodgingRepository lodgingRepository,
            IRegionRepository regionRepository,
            IReserveRepository reserveRepository,
            ITouristLocationRepository touristLocationRepository,
            IUserRepository userRepository,
            ISessionRepository sessionRepository,
            IReviewRepository reviewRepository
            ) 
        {
            _context = context;
            CategoryRepository = categoryRepository;
            LodgingRepository = lodgingRepository;
            RegionRepository = regionRepository;
            ReserveRepository = reserveRepository;
            TouristLocationRepository = touristLocationRepository;
            UserRepository = userRepository;
            SessionRepository = sessionRepository;
            ReviewRepository = reviewRepository;
        }

        public ICategoryRepository CategoryRepository { get; }
        public ILodgingRepository LodgingRepository { get; }
        public IRegionRepository RegionRepository { get; }
        public IReserveRepository ReserveRepository { get; }
        public ITouristLocationRepository TouristLocationRepository { get; }
        public IUserRepository UserRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public IReviewRepository ReviewRepository { get; }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}



