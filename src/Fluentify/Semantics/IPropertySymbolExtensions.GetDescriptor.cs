namespace Fluentify.Semantics;

using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using static Fluentify.DescriptorAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    private static readonly Regex pattern = new("^[A-Z][a-zA-Z0-9]*$", RegexOptions.Compiled);

    /// <summary>
    /// Gets the descriptor associated with the <paramref name="property"/> when declared within a record.
    /// </summary>
    /// <param name="property">The property to be checked for the presence of an attribute.</param>
    /// <returns>
    /// The descriptor associated with the <paramref name="property"/> when declared within a record, otherwise <see langword="null"/>.
    /// </returns>
    public static string? GetDescriptor(this IPropertySymbol property)
    {
        AttributeData? attribute = property.GetAttribute(Name);

        if (attribute is null)
        {
            IParameterSymbol? parameter = property.GetParameter();

            if (parameter is null)
            {
                return default;
            }

            attribute = parameter.GetAttribute(Name);

            if (attribute is null)
            {
                return default;
            }
        }

        TypedConstant argument = attribute.ConstructorArguments.FirstOrDefault();

        if (argument.Value is string value && pattern.IsMatch(value))
        {
            return value;
        }

        return default;
    }
}