using System.Text;

namespace WebShop.Shared.Extensions;

public static class StringExtensions
{
    public static string Interpolate(this string message, params object[] args)
    {
        var sb = new StringBuilder(message);
        
        var argumentNumber = 0;
        foreach (var arg in args)
        {
            var forReplacement = $"{{{argumentNumber}}}";
            sb.Replace(forReplacement, arg.ToString());
            argumentNumber++;
        }
        
        return sb.ToString();
    }
}