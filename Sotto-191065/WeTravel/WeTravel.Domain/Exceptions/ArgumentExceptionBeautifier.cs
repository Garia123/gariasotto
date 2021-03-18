using System;

namespace WeTravel.Domain.Exceptions
{
    public class ArgumentExceptionBeautifier : Exception
    {
        public override string Message { get; }

        public ArgumentExceptionBeautifier(string nameOfArugment)
        {
            Message = $"The argument provided {nameOfArugment} is invalid";
        }
    }
}
