using System;
using WeTravel.Model;

namespace WeTravel.ServiceInterface
{
    public interface ISessionService
    {
        Guid Login(LoginModelIn login);
        bool ValidateToken(Guid token);
    }
}
