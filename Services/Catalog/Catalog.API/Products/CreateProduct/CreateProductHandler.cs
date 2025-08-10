namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
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

internal class CreateProductCommandHandler(
    ILogger<CreateProductCommandHandler> logger,
    IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

        Product product = command.Adapt<Product>();

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}