namespace Catalog.API.Exceptions;

public class ProductNotFoundException(string id) : Exception($"Product with {id} not found");