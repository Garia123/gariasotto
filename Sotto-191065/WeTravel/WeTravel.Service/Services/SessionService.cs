using System;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class SessionService : ServiceBase,ISessionService
    {
        public SessionService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public Guid Login(LoginModelIn login)
        {
            if (login == null)
            {
                throw new ArgumentExceptionBeautifier("Model");
            }
            Guid sessionToken = Guid.NewGuid();
            User user = getUser(login);

            checkIfPasswordsMatch(login, user);

            createSession(sessionToken, user);

            return sessionToken;
        }

        private void createSession(Guid sessionToken, User user)
        {
            Session session = new Session() { Token = sessionToken, User = user };
            UnitOfWork.SessionRepository.Create(session);
            UnitOfWork.Save();
        }

        private static void checkIfPasswordsMatch(LoginModelIn login, User user)
        {
            if (user.Password != login.Password)
            {
                throw new InvalidOperationExceptionBeautifier("PasswordInvalid");
            }
        }

        private User getUser(LoginModelIn login)
        {
            User fakeUser = new User() { Email = login.Email };
            User user = UnitOfWork.UserRepository.Get(fakeUser);
            return user;
        }

        public bool ValidateToken(Guid token)
        {
            return UnitOfWork.SessionRepository.ContainsToken(token);
        }
    }
}
