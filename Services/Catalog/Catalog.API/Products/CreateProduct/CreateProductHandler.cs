namespace Catalog.API.Products.CreateProduct;

internal record CreateProductCommand(
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories) : ICommand<CreateProductResult>;

internal record CreateProductResult(Guid Id);

internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        Product product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
            Categories = command.Categories
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}