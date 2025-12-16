namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

/// <summary>
/// Provides helper methods for resolving auto initiation details.
/// </summary>
internal static class AutoInitiationResolver
{
    public static bool TryGetMember(this AttributeData attribute, out string member)
    {
        member = string.Empty;

        if (attribute.ApplicationSyntaxReference is not null
         && attribute.ApplicationSyntaxReference.GetSyntax() is AttributeSyntax syntax
         && syntax.ArgumentList is not null
         && syntax.ArgumentList.Arguments.Count == 1)
        {
            AttributeArgumentSyntax argument = syntax.ArgumentList.Arguments[0];

            member = argument.Expression switch
            {
                LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.StringLiteralExpression) => literal.Token.ValueText,
                _ => argument.Expression.ToString(),
            };

            return !string.IsNullOrWhiteSpace(member);
        }

        if (attribute.ConstructorArguments is [TypedConstant argument] && argument.Value is string value)
        {
            member = value;

            return !string.IsNullOrWhiteSpace(member);
        }

        return false;
    }

    public static bool TryResolve(ITypeSymbol target, string member, out Initialization initialization)
    {
        initialization = default;

        if (string.IsNullOrWhiteSpace(member))
        {
            return false;
        }

        string name = target.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        foreach (ISymbol candidate in target.GetMembers(member).Where(candidate => candidate.IsStatic))
        {
            if (candidate is IMethodSymbol method && method.Parameters.Length == 0 && SymbolEqualityComparer.Default.Equals(method.ReturnType, target))
            {
                string expression = $"{name}.{member}()";

                initialization = new(expression, expression);

                return true;
            }

            if (candidate is IPropertySymbol property && SymbolEqualityComparer.Default.Equals(property.Type, target))
            {
                string expression = $"{name}.{member}";

                initialization = new(expression, expression);

                return true;
            }
        }

        return false;
    }
}
