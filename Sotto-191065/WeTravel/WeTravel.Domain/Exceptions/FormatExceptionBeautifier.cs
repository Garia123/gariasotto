using System;

namespace WeTravel.Domain.Exceptions
{
    public class FormatExceptionBeautifier : Exception
    {
        public override string Message { get; }

        public FormatExceptionBeautifier(string nameOfArugment)
        {
            Message = $"The sub argument {nameOfArugment} provided is invalid";
        }
    }
}
