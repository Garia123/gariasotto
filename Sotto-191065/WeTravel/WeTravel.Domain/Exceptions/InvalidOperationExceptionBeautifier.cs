using System;

namespace WeTravel.Domain.Exceptions
{ 
    public class InvalidOperationExceptionBeautifier : Exception
    {
        public override string Message { get; }

        public InvalidOperationExceptionBeautifier(string preCondition)
        {
            Message = $"The precondition: {preCondition} was not met";
        }
    }
}
