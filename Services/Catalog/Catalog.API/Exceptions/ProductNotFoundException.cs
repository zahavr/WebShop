namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string id) : base($"Product with {id} not found")
    {
        
    }
}