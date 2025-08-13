using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

internal record GetProductQuery(int? PageNumber = 1, int? PageSize = 10 ) : IQuery<GetProductResult>;

internal record GetProductResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(
    ILogger<GetProductsQueryHandler> logger,
    IDocumentSession session)
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

        var products = await session
            .Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10,cancellationToken);

        return new GetProductResult(products);
    }
}