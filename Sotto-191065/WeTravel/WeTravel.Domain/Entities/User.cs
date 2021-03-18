using System.ComponentModel.DataAnnotations;
using WeTravel.Domain.Exceptions;
using WeTravel.Domain.Interface;

namespace WeTravel.Domain
{
    public class User
    {
        [RegularExpression(DataValidator.ShortTextRegex)]
        public string FullName { get; set; }
        [RegularExpression(DataValidator.EmailRegex)]
        public string Email { get; set; }
        [RegularExpression(DataValidator.PasswordRegex)]
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is User user)
            {
                result = Email.Equals(user.Email);
            }

            return result;
        }

        public override int GetHashCode()
        {
            int hash = 19;
            hash = hash * 23 + ((Email == null) ? 0 : Email.GetHashCode());
            return hash;
        }
    }
}
