namespace Fluentify;

using Fluentify.Semantics;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

/// <summary>
/// Supports the impelmentation of analyzers relating to <see cref="AttributeSyntax"/>.
/// </summary>
public abstract class AttributeAnalyzer
    : Analyzer<AttributeSyntax>
{
    private readonly string _name;

    /// <summary>
    /// Facilitates construction of an analyzer that matches for the specified <paramref name="kind"/>.
    /// </summary>
    /// <param name="name">The name of the attribute to match.</param>
    private protected AttributeAnalyzer(string name)
        : base(SyntaxKind.Attribute)
    {
        _name = name;
    }

    /// <summary>
    /// Determines if the MissingFluentifyRule is violated by the type associated with the specified <paramref name="syntax"/>.
    /// </summary>
    /// <param name="context">
    /// A syntax node action can use a <see cref="SyntaxNodeAnalysisContext"/> to report <see cref="Diagnostic"/>s for a <see cref="SyntaxNode"/>.
    /// </param>
    /// <param name="syntax">The attribute that serves as the subject of the analyzer.</param>
    /// <param name="member">The name of the property or parameter deemed to be in violation of the rule.</param>
    /// <param name="class">The name of the type deemed to be in violation of the rule.</param>
    /// <param name="location">The location of the specific syntax deemed to be the focus of the violation.</param>
    /// <returns>True if the rule has been violated, otherwise False.</returns>
    protected static bool IsViolatingMissingFluentifyRule(
        SyntaxNodeAnalysisContext context,
        AttributeSyntax syntax,
        out string member,
        out string @class,
        out Location location)
    {
        ISymbol? parent = syntax.GetParent<TypeDeclarationSyntax>(context);

        ISymbol? target = syntax.GetParent<PropertyDeclarationSyntax>(context)
            ?? syntax.GetParent<ParameterSyntax>(context);

        if (parent is not INamedTypeSymbol type || target is null || type.HasFluentify())
        {
            member = string.Empty;
            @class = string.Empty;
            location = Location.None;

            return false;
        }

        member = target.Name;
        @class = type.Name;
        location = syntax.GetLocation();

        return true;
    }

    /// <inheritdoc/>
    protected sealed override void AnalyzeNode(SyntaxNodeAnalysisContext context, AttributeSyntax syntax)
    {
        IMethodSymbol? symbol = GetSymbol(context, syntax);

        if (symbol is null || !IsAttribute(symbol))
        {
            return;
        }

        AnalyzeNode(context, symbol, syntax);
    }

    /// <summary>
    /// Performs analysis upon the <paramref name="syntax"/>, which is deemed to have matched the
    /// <typeparamref name="TSyntax"/> and <see cref="kind"/> values.
    /// </summary>
    /// <param name="context">
    /// Context for a syntax node action. A syntax node action can use a Microsoft.CodeAnalysis.Diagnostics.SyntaxNodeAnalysisContext
    /// to report Microsoft.CodeAnalysis.Diagnostics for a Microsoft.CodeAnalysis.SyntaxNode.
    /// </param>
    /// <param name="symbol">
    /// The <see cref="IMethodSymbol"/> associated with the <see cref="AttributeSyntax"/> that matches the name provided by the derivation.
    /// </param>
    /// <param name="syntax">
    /// The <typeparamref name="TSyntax"/> matched within the <paramref name="context"/> to which the analysis is to be applied.
    /// </param>
    protected abstract void AnalyzeNode(SyntaxNodeAnalysisContext context, IMethodSymbol symbol, AttributeSyntax syntax);

    private static IMethodSymbol? GetSymbol(SyntaxNodeAnalysisContext context, AttributeSyntax syntax)
    {
        return context
            .SemanticModel
            .GetSymbolInfo(syntax, cancellationToken: context.CancellationToken)
            .Symbol as IMethodSymbol;
    }

    private bool IsAttribute(IMethodSymbol symbol)
    {
        return symbol.ContainingType is not null
            && symbol.ContainingType.IsAttribute(_name);
    }
}