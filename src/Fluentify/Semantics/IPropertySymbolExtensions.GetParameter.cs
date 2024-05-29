namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Gets the parameter associated with the <paramref name="property"/> when declared within a record.
    /// </summary>
    /// <param name="property">The property to be checked for the presence of an attribute.</param>
    /// <returns>
    /// The parameter associated with the <paramref name="property"/> when declared within a record, otherwise <see langword="null"/>.
    /// </returns>
    public static IParameterSymbol? GetParameter(this IPropertySymbol property)
    {
        INamedTypeSymbol containingType = property.ContainingType;

        if (containingType.IsRecord)
        {
            return containingType
                .InstanceConstructors
                .SelectMany(constructor => constructor.Parameters)
                .FirstOrDefault(parameter => parameter.Name == property.Name);
        }

        return default;
    }
}