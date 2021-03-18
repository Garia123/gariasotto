using System.Text.RegularExpressions;

namespace WeTravel.Domain
{
    public static class DataValidator
    {
        public const string ShortTextRegex = @"^[a-zA-Z0-9._ '-]{1,50}$";
        public const string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public const string PasswordRegex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
        public const string LongTextRegex = "^[a-zA-Z._ '-]{1,2000}$";
        public const string TelephoneRegex = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
    }
}
