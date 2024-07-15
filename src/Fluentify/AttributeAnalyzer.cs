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
    private readonly string name;

    /// <summary>
    /// Facilitates construction of an analyzer that matches for the specified <paramref name="kind"/> and
    /// may raise the specified <paramref name="diagnostics"/> if the conditions in the derived class are met.
    /// </summary>
    /// <param name="name">The name of the attribute to match.</param>
    /// <param name="diagnostics">The rules that are applied and potentially raised by the analyzer.</param>
    private protected AttributeAnalyzer(string name, params DiagnosticDescriptor[] diagnostics)
        : base(SyntaxKind.Attribute, diagnostics)
    {
        this.name = name;
    }

    protected static bool IsViolatingMissingFluentifyRule(SyntaxNodeAnalysisContext context, AttributeSyntax syntax, out string @class, out Location location)
    {
        ISymbol? parent = syntax.GetParent<TypeDeclarationSyntax>(context);

        if (parent is not INamedTypeSymbol type || type.HasFluentify())
        {
            @class = string.Empty;
            location = Location.None;

            return false;
        }

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
            && symbol.ContainingType.IsAttribute(name);
    }
}