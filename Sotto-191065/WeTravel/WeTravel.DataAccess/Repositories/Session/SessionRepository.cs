using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;

namespace WeTravel.DataAccess
{
    public class SessionRepository : ISessionRepository
    {
        private DbContext _context;

        public SessionRepository(DbContext context)
        {
            _context = context;
        }

        public bool ContainsToken(Guid token)
        {
            return _context.Set<Session>().Where(s => s.Token == token).Any();
        }

        public void Create(Session session)
        {
            _context.Set<Session>().Add(session);
        }
    }
}


