
using System;
using WeTravel.Domain;

namespace WeTravel.DataAccessInterface
{
    public interface ISessionRepository
    {
        void Create(Session session);
        bool ContainsToken(Guid token);
    }
}
