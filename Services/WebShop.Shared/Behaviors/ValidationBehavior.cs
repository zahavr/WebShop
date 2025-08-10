using FluentValidation;
using MediatR;
using WebShop.Shared.Abstractions.CQRS;

namespace WebShop.Shared.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICommand<TResponse> 
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(f => f.Errors.Count != 0)
            .SelectMany(v => v.Errors)
            .ToList();

        if (failures.Count != 0) throw new ValidationException(failures);
        
        return await next(cancellationToken);
    }
}