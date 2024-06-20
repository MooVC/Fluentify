namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    /// <summary>
    /// Determines if the <paramref name="type"/> adheres to the new() constraint.
    /// </summary>
    /// <param name="type">The <see cref="ITypeSymbol"/> for which the determination is to be carried out.</param>
    /// <param name="compilation">The <see cref="Compilation"/> used to determine if the constraint allows for internal construction.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> that can be used to cancel the operation.</param>
    /// <returns>True if the <paramref name="type"/> adheres to the new() constraint, otherwise False.</returns>
    public static bool IsBuildable(this ITypeSymbol type, Compilation compilation, CancellationToken cancellationToken)
    {
        if (type is ITypeParameterSymbol generic)
        {
            return generic.HasConstructorConstraint;
        }

        if (type.HasAccessibleParameterlessConstructor(compilation, out bool isInternal))
        {
            return true;
        }

        if (isInternal && type is INamedTypeSymbol symbol && symbol.HasFluentify())
        {
            return type
                .DeclaringSyntaxReferences
                .Select(syntax => syntax.GetSyntax(cancellationToken: cancellationToken))
                .OfType<RecordDeclarationSyntax>()
                .Any(record => record.Modifiers.Any(SyntaxKind.PartialKeyword));
        }

        return false;
    }
}