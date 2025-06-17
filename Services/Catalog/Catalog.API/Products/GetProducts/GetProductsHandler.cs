namespace Catalog.API.Products.GetProducts;

internal record GetProductQuery : IQuery<GetProductResult>;

internal record GetProductResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(
    IDocumentSession session,
    ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

        IReadOnlyList<Product> products = await session
            .Query<Product>()
            .ToListAsync(cancellationToken);

        return new GetProductResult(products);
    }
}