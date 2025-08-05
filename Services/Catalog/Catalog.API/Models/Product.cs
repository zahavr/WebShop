namespace Catalog.API.Models;

public record Product
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string ImageFile { get; init; } = null!;

    public decimal Price { get; init; }

    public List<string> Categories { get; init; } = new();
    
}