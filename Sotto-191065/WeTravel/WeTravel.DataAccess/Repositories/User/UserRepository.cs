using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            if (_context.Set<User>().Contains(user))
            {
                throw new InvalidOperationExceptionBeautifier("user exists");
            }
            else
            {
                _context.Set<User>().Add(user);
            }
        }

        public void Delete(string email)
        {
            var user = new User() { Email = email };
            if (_context.Set<User>().Contains(user))
            {
                var findUser = _context.Set<User>().First(u => u.Equals(user));
                _context.Set<User>().Remove(findUser);
            }
            else
            {
                throw new InvalidOperationExceptionBeautifier("user does exists");
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Set<User>().ToList();
        }

        public User Get(User user)
        {
            if (_context.Set<User>().Contains(user))
            {
                return _context.Set<User>().Where(u => u.Email == user.Email).FirstOrDefault();
            }
            else
            {
                throw new InvalidOperationExceptionBeautifier("user does exists");
            }
        }

        public void UpdateUser(User user)
        {
            if (_context.Set<User>().Contains(user))
            {
                _context.Set<User>().Update(user);
            }
            else
            {
                throw new InvalidOperationExceptionBeautifier("user does exists");
            }
        }
    }
}


