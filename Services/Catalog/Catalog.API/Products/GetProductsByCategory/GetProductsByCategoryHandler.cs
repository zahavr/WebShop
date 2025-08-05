namespace Catalog.API.Products.GetProductsByCategory;

internal record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

internal record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler(
    ILogger<GetProductsByCategoryQueryHandler> logger,
    IQuerySession session) 
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult> 
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);
        
        IReadOnlyList<Product> products = await session
            .Query<Product>()
            .Where(p => p.Categories.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult(products);
    }
}