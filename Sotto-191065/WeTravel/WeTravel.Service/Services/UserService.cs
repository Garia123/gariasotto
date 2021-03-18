using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(UserModelIn userModelIn)
        {
            if(userModelIn == null)
            {
                throw new ArgumentExceptionBeautifier("USER");
            }
            else
            {
                var user = GetEntityFromModel(userModelIn);
                UnitOfWork.UserRepository.Create(user);
                UnitOfWork.Save();
            }
        }

        public void Delete(string email)
        {
            UnitOfWork.UserRepository.Delete(email);
            UnitOfWork.Save();
        }

        public IEnumerable<UserModelOut> Get()
        {
            return UnitOfWork.UserRepository.GetAll().Select(GetModelFromEntity);
        }

        public void UpdateUser(UserModelIn userModelIn)
        {
            if (userModelIn == null)
            {
                throw new ArgumentExceptionBeautifier("USER");
            }
            else
            {
                var user = GetEntityFromModel(userModelIn);
                UnitOfWork.UserRepository.UpdateUser(user);
                UnitOfWork.Save();
            }
        }

        private UserModelOut GetModelFromEntity(User user)
        {
            return new UserModelOut()
            {
                Email = user.Email,
                FullName = user.FullName
            };
        }

        private User GetEntityFromModel(UserModelIn user)
        {
            return new User()
            {
                Email = user.Email,
                FullName = user.FullName,
                Password = user.Password
            };
        }
    }
}
