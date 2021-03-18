using System;
using System.Collections.Generic;
using System.Text;
using WeTravel.Domain;
using WeTravel.Model;

namespace WeTravel.DataAccessInterface
{
    public interface IUserRepository
    {
        void Create(User user);
        IEnumerable<User> GetAll();
        void Delete(string email);
        void UpdateUser(User userModelIn);
        User Get(User user);
    }
}
