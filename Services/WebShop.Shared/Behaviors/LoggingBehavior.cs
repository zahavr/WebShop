using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WebShop.Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull

{
    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "[START] Handling request: '{@Request}'. Response: '{@Response}'. RequestData: '{@RequestData}'.",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            request);

        var timer = new Stopwatch();
        timer.Start();
        var response = await next(cancellationToken);
        timer.Stop();

        var elapsed = timer.Elapsed;
        if (elapsed.TotalMilliseconds > 300)
        {
            logger.LogWarning("[PERFORMANCE] The request '{@Request}' took '{@Elapsed}' milliseconds to complete.",
                              typeof(TRequest).Name, elapsed.TotalMilliseconds);
        }

        logger.LogInformation("[END] Completing request: '{@Request}'. Response: '{@Response}'.", typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}