using Catalog.API.Exceptions;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<Unit>;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty()
            .WithMessage(Constants.ProductValidation.ErrorMessages.ProductIdEmpty); 
    }
}

internal class DeleteProductHandler(
    ILogger<DeleteProductHandler> logger,
    IDocumentSession session) 
    : ICommandHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", command);

        Product? entity = await session.LoadAsync<Product>(command.ProductId, cancellationToken);
        if(entity is null) throw new ProductNotFoundException(command.ProductId.ToString());
        
        session.Delete(entity);
        await session.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}