using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AtsCommerce.Core.Errors.Handler
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                logger.LogError(error.ToString());
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ConflictException:
                        // Conflict error
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case ForbiddenException:
                        // Forbidden error
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case BadRequestException:
                        // Bad request error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException:
                        // Not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnAuthorizeException:
                        // Unauthorize error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        // Unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                throw new ApiException(error.Message, response.StatusCode);
            }
        }
    }
}
