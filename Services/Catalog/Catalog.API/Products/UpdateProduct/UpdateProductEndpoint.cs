namespace Catalog.API.Products.UpdateProduct;

internal record UpdateProductRequest(
    string Name,
    string Description,
    string ImageFile,
    List<string> Categories,
    decimal Price);

// internal record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{productId:guid}", async (
                Guid productId,
                UpdateProductRequest request,
                ISender sender) =>
            {
                UpdateProductCommand command = request.Adapt<UpdateProductCommand>();
                command = command with { Id = productId };

                await sender.Send(command);
                
                return Results.NoContent();
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update product")
            .WithDescription("Update product");
        ;
    }
};