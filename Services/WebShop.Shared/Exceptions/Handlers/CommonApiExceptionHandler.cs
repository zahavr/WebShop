using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebShop.Shared.Exceptions.Handlers;

public class CommonApiExceptionHandler(
    ILogger<CommonApiExceptionHandler> logger)
    : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError("Error message: {exceptionMessage}. Time of ocurrence {time}.", exception.Message,
                        DateTime.UtcNow);

        (string Detail, string Title, int StatusCode) exceptionInfo = exception switch
        {
            InternalServerException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            ),
            ValidationException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            BadRequestException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            NotFoundException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status404NotFound
            ),
            _ =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };

        var problemDetails = new ProblemDetails
        {
            Title = exceptionInfo.Title,
            Status = exceptionInfo.StatusCode,
            Detail = exceptionInfo.Detail,
            Instance = context.Request.Path,
            Extensions = 
            {
                {"TraceId", context.TraceIdentifier}
            }
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }
        
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}