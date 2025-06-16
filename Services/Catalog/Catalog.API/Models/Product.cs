namespace Catalog.API.Models;

internal class Product
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public string ImageFile { get; init; } = default!;

    public decimal Price { get; init; }

    public List<string> Categories { get; init; } = new();
}