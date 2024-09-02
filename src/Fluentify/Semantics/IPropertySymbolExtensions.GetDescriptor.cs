namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Fluentify.DescriptorAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Gets the descriptor associated with the <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The property to be checked for the presence of an attribute.</param>
    /// <returns>
    /// The descriptor associated with the <paramref name="property"/>, otherwise <see langword="null"/>.
    /// </returns>
    public static string? GetDescriptor(this IPropertySymbol property)
    {
        AttributeData? attribute = property.GetAttribute(Name)
            ?? property.GetAttributeFromParameter();

        if (attribute is null)
        {
            return default;
        }

        if (attribute.HasDescriptorOnConstuctorArguments(out string descriptor) || attribute.HasDescriptorOnSyntax(out descriptor))
        {
            if (Pattern.IsMatch(descriptor))
            {
                return descriptor;
            }

            if (!string.IsNullOrEmpty(descriptor))
            {
                return default;
            }
        }

        return property.Name;
    }

    private static AttributeData? GetAttributeFromParameter(this IPropertySymbol property)
    {
        IParameterSymbol? parameter = property.GetParameter();

        if (parameter is null)
        {
            return default;
        }

        return parameter.GetAttribute(Name);
    }

    private static bool HasDescriptorOnConstuctorArguments(this AttributeData attribute, out string value)
    {
        value = string.Empty;

        if (attribute.ConstructorArguments.Length == 0)
        {
            return false;
        }

        TypedConstant argument = attribute.ConstructorArguments.First();

        if (argument.Value is string descriptor)
        {
            value = descriptor;

            return true;
        }

        return false;
    }

    private static bool HasDescriptorOnSyntax(this AttributeData attribute, out string value)
    {
        if (attribute.ApplicationSyntaxReference is not null
         && attribute.ApplicationSyntaxReference.GetSyntax() is AttributeSyntax syntax
         && syntax.ArgumentList is not null
         && syntax.ArgumentList.Arguments.Count == 1)
        {
            AttributeArgumentSyntax argument = syntax.ArgumentList.Arguments[0];

            value = argument.Expression is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.StringLiteralExpression)
                ? literal.Token.ValueText
                : argument.Expression.ToString();

            return true;
        }

        value = string.Empty;

        return false;
    }
}