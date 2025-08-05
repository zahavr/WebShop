namespace Catalog.API.Products.GetProductsByCategory;

// internal record GetProductsByCategoryRequest()

internal record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category:minlength(1):maxlength(128)}", async (string category, ISender sender) =>
            {
                GetProductsByCategoryQuery query = new GetProductsByCategoryQuery(category);
                GetProductsByCategoryResult result = await sender.Send(query);

                GetProductsByCategoryResponse response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get products by category")
            .WithDescription("Get products by category");
    }
}