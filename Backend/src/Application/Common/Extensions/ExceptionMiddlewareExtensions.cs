using Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace Application.Common.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}