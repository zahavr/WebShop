using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById;

internal record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

internal record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(
    ILogger<GetProductByIdQueryHandler> logger,
    IQuerySession session) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);

        Product? product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id.ToString());
        }

        return new GetProductByIdResult(product);
    }
}