using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WeTravel.ServiceInterface;

namespace WeTravel.Filter
{
    public class WeTravelAuthFilter : Attribute, IAuthorizationFilter
    {
        private const string INVALID_TOKEN_MESSAGE = "Invalid token.";
        private const string OK_TOKEN_MESSAGE = "Token provided is OK.";
        private ISessionService _sessionService;

        public WeTravelAuthFilter(ISessionService session)
        {
            _sessionService = session;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Guid token;
            try
            {
                var headerValue = context.HttpContext.Request.Headers["auth"].ToString();
                token = Guid.Parse(headerValue);

                if (!_sessionService.ValidateToken(token))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = INVALID_TOKEN_MESSAGE
                    };
                    return;
                }
            }
            catch (Exception)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = INVALID_TOKEN_MESSAGE
                };
                return;
            }

            //context.Result = new ContentResult()
            //{
            //    StatusCode = 200,
            //    Content = OK_TOKEN_MESSAGE
            //};
        }
    }
}