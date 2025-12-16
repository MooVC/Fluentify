namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;
using static Fluentify.Semantics.AutoInitiationResolver;

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
    public static bool TryGetInitialization(this ITypeSymbol type, out Initialization initialization)
    {
        initialization = default;

        if (type.HasAttribute(SkipAutoInitializationAttributeGenerator.Name))
        {
            return false;
        }

        AttributeData? attribute = type.GetAttribute(AutoInitiateWithAttributeGenerator.Name);

        if (attribute is null || !attribute.TryGetMember(out string member))
        {
            return false;
        }

        return TryResolve(type, member, out initialization);
    }
}
