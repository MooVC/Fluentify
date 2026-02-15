namespace Fluentify.Syntax;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Provides extensions relating to <see cref="AttributeData"/>.
/// </summary>
internal static partial class AttributeDataExtensions
{
    private const int RequiredArgumentsForAutoInitiation = 1;

    /// <summary>
    /// Attempts to extract the member name from the specified attribute data, if present.
    /// </summary>
    /// <param name="attribute">The attribute data from which to retrieve the member name.</param>
    /// <param name="member">When this method returns, contains the extracted member name if found; otherwise, an empty string.</param>
    /// <returns><see langword="true" /> if the member name was successfully extracted; otherwise, <see langword="false" />.</returns>
    /// <remarks>
    /// This method inspects both the syntax and constructor arguments of the attribute to determine
    /// the member name. It returns <see langword="false" /> if the attribute does not contain a recognizable member name.
    /// </remarks>
    public static bool TryGetMember(this AttributeData attribute, out string member)
    {
        member = string.Empty;

        if (attribute.ApplicationSyntaxReference is not null
         && attribute.ApplicationSyntaxReference.GetSyntax() is AttributeSyntax syntax
         && syntax.ArgumentList is not null
         && syntax.ArgumentList.Arguments.Count == RequiredArgumentsForAutoInitiation)
        {
            AttributeArgumentSyntax argument = syntax.ArgumentList.Arguments[0];

            member = argument.Expression switch
            {
                LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.StringLiteralExpression) => literal.Token.ValueText,
                _ => argument.Expression.ToString(),
            };

            return !string.IsNullOrWhiteSpace(member);
        }

        if (attribute.ConstructorArguments.Length == RequiredArgumentsForAutoInitiation
         && attribute.ConstructorArguments[0].Value is string value)
        {
            member = value;

            return !string.IsNullOrWhiteSpace(value);
        }

        return false;
    }
}