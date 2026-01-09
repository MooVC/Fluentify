namespace Fluentify.Semantics;

using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using static Fluentify.AutoInitializeWithAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Attempts to retrieve the initialization value associated with the specified property symbol.
    /// </summary>
    /// <remarks>This method checks for an initialization attribute on the property, its parameter, or its
    /// type. If the attribute is present and can be resolved, the corresponding initialization value is returned via
    /// the out parameter.</remarks>
    /// <param name="property">The property symbol for which to obtain the initialization value.</param>
    /// <param name="initialization">When this method returns, contains the resolved initialization value if available; otherwise, an empty string.</param>
    /// <returns>true if an initialization value was successfully resolved; otherwise, false.</returns>
    public static bool TryGetInitialization(this IPropertySymbol property, out string initialization)
    {
        initialization = string.Empty;

        if (property.HasSkipAutoInitialization())
        {
            return false;
        }

        AttributeData? attribute = property.GetAttribute(Name)
            ?? property.GetParameter()?.GetAttribute(Name)
            ?? property.Type.GetAttribute(Name);

        if (attribute is null || !attribute.TryGetMember(out string member))
        {
            return false;
        }

        return property.ContainingType.TryResolve(property.Type, ref member, out initialization)
            || property.Type.TryResolve(property.Type, ref member, out initialization);
    }
}