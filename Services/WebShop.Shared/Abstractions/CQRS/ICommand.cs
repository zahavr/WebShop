using MediatR;

namespace WebShop.Shared.Abstractions.CQRS;

public interface ICommand : ICommand<Unit>;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : notnull;