using WebShop.Shared.Exceptions;
using WebShop.Shared.Extensions;

namespace Catalog.API.Exceptions;

public class ProductNotFoundException(string id) : NotFoundException(ErrorMessages.ProductNotFound.Interpolate(id));