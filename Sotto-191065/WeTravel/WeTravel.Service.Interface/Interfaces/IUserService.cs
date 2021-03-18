using System;
using System.Collections.Generic;
using System.Text;
using WeTravel.Model;

namespace WeTravel.ServiceInterface
{
    public interface IUserService
    {
        void Create(UserModelIn userModelIn);
        void Delete(string email);
        void UpdateUser(UserModelIn userModelIn);
        IEnumerable<UserModelOut> Get();
    }
}

