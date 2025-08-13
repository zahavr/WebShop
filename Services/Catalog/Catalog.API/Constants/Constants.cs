namespace Catalog.API;

internal static class Constants
{
    public static class ProductValidation
    {
        public static class ErrorMessages
        {
            public static readonly string ProductIdEmpty = "ProducId cannot be empty";
            public static readonly string NameEmpty = "Product name cannot be empty";
            public static readonly string CategoryEmpty = "Product category cannot be empty";
            public static readonly string ImageFileEmpty =  "Product image file cannot be empty";
            public static readonly string PriceMustBeGreaterThanZero = "Product price must be greater than zero";
            
            public static readonly string NameLongerThanMaxLength = $"Product name cannot be longer than {Rules.NameMaxLength}";
        }

        public static class Rules
        {
            public const int NameMaxLength = 128;
            public const decimal MinimalPrice = 0.00M;
        }
    }

    public static class Environments
    {
        public const string Local = "Local";
    }
}