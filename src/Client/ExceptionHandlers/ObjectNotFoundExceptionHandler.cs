using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Client.ExceptionHandlers;

public class ObjectNotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ObjectNotFoundException objectNotFoundException) return false;

        await Results.Problem(objectNotFoundException.Message, statusCode: 400).ExecuteAsync(httpContext);
        return true;
    }
}
