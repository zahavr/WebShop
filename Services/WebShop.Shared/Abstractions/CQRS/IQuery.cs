using MediatR;

namespace WebShop.Shared.Abstractions.CQRS;

public interface IQuery : IRequest;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull;