namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    /// <summary>
    /// Determines if the type associated with <paramref name="property"/> adheres to the new() constraint.
    /// </summary>
    /// <param name="property">The <see cref="IPropertySymbol"/> for which the determination is to be carried out.</param>
    /// <param name="compilation">The <see cref="Compilation"/> used to determine if the constraint allows for internal construction.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> that can be used to cancel the operation.</param>
    /// <returns>True if the <paramref name="property"/> adheres to the new() constraint, otherwise False.</returns>
    public static bool IsBuildable(this IPropertySymbol property, Compilation compilation, CancellationToken cancellationToken)
    {
        if (property.Type is ITypeParameterSymbol generic)
        {
            return generic.HasConstructorConstraint;
        }

        bool isInternal = property
            .Type
            .ContainingAssembly
            .Equals(compilation.Assembly, SymbolEqualityComparer.Default);

        if (property.Type.HasAccessibleParameterlessConstructor(isInternal))
        {
            return true;
        }

        if (isInternal && property.Type is INamedTypeSymbol type && type.HasFluentify())
        {
            return property
                .Type
                .DeclaringSyntaxReferences
                .Select(syntax => syntax.GetSyntax(cancellationToken: cancellationToken))
                .OfType<RecordDeclarationSyntax>()
                .Any(record => record.Modifiers.Any(SyntaxKind.PartialKeyword));
        }

        return false;
    }
}