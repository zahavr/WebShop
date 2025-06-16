using MediatR;

namespace WebShop.Shared.Abstractions.CQRS;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, Unit>
    where TQuery : IQuery<Unit>;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull;