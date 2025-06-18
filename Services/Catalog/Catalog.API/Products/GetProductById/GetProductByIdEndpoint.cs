namespace Catalog.API.Products.GetProductById;

// internal record GetProductByIdRequest(Guid Id);

internal record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{productId:guid}", async (Guid productId, ISender sender) =>
            {
                GetProductByIdQuery query = new GetProductByIdQuery(productId);
                GetProductByIdResult result = await sender.Send(query);

                GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get product by id")
            .WithDescription("Get product by id");
    }
}