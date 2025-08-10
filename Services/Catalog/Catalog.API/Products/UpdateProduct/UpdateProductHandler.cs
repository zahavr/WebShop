using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    string ImageFile,
    List<string> Categories,
    decimal Price) : ICommand<Unit>;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(Constants.ProductValidation.ErrorMessages.ProductIdEmpty);
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(Constants.ProductValidation.ErrorMessages.NameEmpty)
            .MaximumLength(Constants.ProductValidation.Rules.NameMaxLength)
            .WithMessage(Constants.ProductValidation.ErrorMessages.NameLongerThanMaxLength);

        RuleFor(c => c.ImageFile)
            .NotEmpty()
            .WithMessage(Constants.ProductValidation.ErrorMessages.ImageFileEmpty);
        
        RuleFor(c => c.Categories)
            .NotEmpty()
            .WithMessage(Constants.ProductValidation.ErrorMessages.CategoryEmpty);

        RuleFor(x => x.Price)
            .GreaterThan(Constants.ProductValidation.Rules.MinimalPrice)
            .WithMessage(Constants.ProductValidation.ErrorMessages.PriceMustBeGreaterThanZero);
    }
}

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