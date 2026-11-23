namespace Fluentify.Syntax;

using MooVC.Syntax;
using MooVC.Syntax.CSharp;

internal static partial class StringExtensions
{
    public static Symbol ToTypeSymbol(this string spelling)
    {
        // Keep compound Roslyn type spellings intact when passing them to the builder.
        if (spelling.EndsWith("Attribute", StringComparison.Ordinal))
        {
            // Qualification otherwise treats the suffix as an attribute usage.
            spelling += "/**/";
        }

        return (Qualification)(Moniker)spelling;
    }
}