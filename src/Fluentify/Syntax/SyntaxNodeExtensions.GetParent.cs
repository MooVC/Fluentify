namespace Fluentify.Syntax;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

/// <summary>
/// Provides extensions relating to <see cref="SyntaxNode"/>.
/// </summary>
internal static partial class SyntaxNodeExtensions
{
    /// <summary>
    /// Retrieves the <see cref="ISymbol"/> for the parent node of <paramref name="syntax"/> that matches the type <typeparamref name="TParent"/>.
    /// </summary>
    /// <typeparam name="TParent">The syntax type of the parent node.</typeparam>
    /// <param name="syntax">The node for which the parent symbol is to be identified and returned.</param>
    /// <param name="context">
    /// Context for a syntax node action. A syntax node action can use a Microsoft.CodeAnalysis.Diagnostics.SyntaxNodeAnalysisContext
    /// to report Microsoft.CodeAnalysis.Diagnostics for a Microsoft.CodeAnalysis.SyntaxNode.
    /// </param>
    /// <returns>The <see cref="ISymbol"/> assoicated with the parent node, if found, otherwise <see langword="null"/>.</returns>
    public static ISymbol? GetParent<TParent>(this SyntaxNode syntax, SyntaxNodeAnalysisContext context)
        where TParent : SyntaxNode
    {
        return syntax.GetParent<TParent>(context, out _);
    }

    /// <summary>
    /// Retrieves the <see cref="ISymbol"/> for the parent node of <paramref name="syntax"/> that matches the type <typeparamref name="TParent"/>.
    /// </summary>
    /// <typeparam name="TParent">The syntax type of the parent node.</typeparam>
    /// <param name="syntax">The node for which the parent symbol is to be identified and returned.</param>
    /// <param name="context">
    /// Context for a syntax node action. A syntax node action can use a Microsoft.CodeAnalysis.Diagnostics.SyntaxNodeAnalysisContext
    /// to report Microsoft.CodeAnalysis.Diagnostics for a Microsoft.CodeAnalysis.SyntaxNode.
    /// </param>
    /// <param name="parent">
    /// The syntax node associated with the parent.
    /// </param>
    /// <returns>The <see cref="ISymbol"/> assoicated with the parent node, if found, otherwise <see langword="null"/>.</returns>
    public static ISymbol? GetParent<TParent>(this SyntaxNode syntax, SyntaxNodeAnalysisContext context, out TParent? parent)
        where TParent : SyntaxNode
    {
        parent = syntax
            .Ancestors()
            .OfType<TParent>()
            .FirstOrDefault();

        if (parent is null)
        {
            return default;
        }

        return context
            .SemanticModel
            .GetDeclaredSymbol(parent, cancellationToken: context.CancellationToken);
    }
}