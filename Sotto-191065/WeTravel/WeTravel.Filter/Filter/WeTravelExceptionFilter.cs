using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Filter
{
    [ExcludeFromCodeCoverage]
    public class WeTravelExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentExceptionBeautifier ||
                context.Exception is InvalidOperationExceptionBeautifier ||
                context.Exception is FormatExceptionBeautifier)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = context.Exception.Message
                };
            }
            else
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 500,
                    Content = "UNEXEPCTED SERVER ERROR"
                };
            }
        }
    }
}
