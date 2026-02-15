namespace Fluentify.Semantics;

using Fluentify.Syntax;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    /// <summary>
    /// Attempts to resolve the initialization expressions for the specified <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The type for which the initialization is to be resolved.</param>
    /// <param name="initialization">The resolved initialization expressions, when available.</param>
    /// <returns>True if initialization expressions can be resolved, otherwise False.</returns>
    public static bool TryGetInitialization(this ITypeSymbol type, out string initialization)
    {
        initialization = string.Empty;

        if (type.HasAttribute(SkipAutoInitializationAttributeGenerator.Name))
        {
            return false;
        }

        AttributeData? attribute = type.GetAttribute(AutoInitializeWithAttributeGenerator.Name);

        if (attribute is null || !attribute.TryGetMember(out string member))
        {
            return false;
        }

        return type.TryResolve(ref member, out initialization);
    }
}