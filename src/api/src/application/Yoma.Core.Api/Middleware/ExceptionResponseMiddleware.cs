using System.Net;
using Yoma.Core.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Yoma.Core.Api.Middleware
{
    public class ExceptionResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //default

            switch (ex.GetType().Name)
            {
                case nameof(BusinessException):
                case nameof(ValidationException):
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case nameof(SecurityException):
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case nameof(DataCollisionException):
                case nameof(DataInconsistencyException):
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var errorResponse = new Dictionary<string, string>()
            { { ex.GetType().Name, ex.Message} };

            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
