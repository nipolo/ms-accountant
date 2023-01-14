using System;
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MS.Accountant.Api.Exceptions
{
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpResponseExceptionFilter> _logger;

        public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is UnauthorizedAccessException nonAuthorizedException)
                {
                    context.Result = new ObjectResult(nonAuthorizedException.Message)
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized
                    };
                }
                else
                {
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }

                context.ExceptionHandled = true;
            }


            if (context.Exception != null)
            {
                _logger.LogError(context.Exception, context.Exception.Message);
            }
        }
    }
}
