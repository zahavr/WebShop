using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct;

internal record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    string ImageFile,
    List<string> Categories,
    decimal Price) : ICommand<Unit>;

internal class UpdateProductCommandHandler(
    IDocumentSession session,
    ILogger<UpdateProductCommandHandler> logger) 
    : ICommandHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);
        
        Product? product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null) throw new ProductNotFoundException(command.Id.ToString());

        product = command.Adapt<Product>();
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}