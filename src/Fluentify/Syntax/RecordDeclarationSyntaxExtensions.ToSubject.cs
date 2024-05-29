﻿namespace Fluentify.Syntax;

using Fluentify.Model;
using Fluentify.Semantics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>
/// Provides extensions relating to <see cref="RecordDeclarationSyntax"/>.
/// </summary>
internal static partial class RecordDeclarationSyntaxExtensions
{
    /// <summary>
    /// Maps the required Semantics from the <paramref name="syntax"/>, using the <paramref name="compilation"/>
    /// and places it within an instance of <see cref="Subject"/>.
    ///
    /// The semantics will only be mapped if the <paramref name="syntax"/> is annotated with the Fluentify attribute.
    /// </summary>
    /// <param name="syntax">The syntax for the record to be mapped.</param>
    /// <param name="compilation">Information relating to the compilation, used to obtain the semantic model for <paramref name="syntax"/>.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> that can be used to cancel the operation.</param>
    /// <returns>
    /// When the <paramref name="syntax"/> is annotated with the Fluentify attribute, the required semantics mapped from <paramref name="syntax"/>
    /// using <paramref name="compilation"/>, otherwise <see langword="null"/>.
    /// </returns>
    public static Subject? ToSubject(this RecordDeclarationSyntax? syntax, Compilation compilation, CancellationToken cancellationToken)
    {
        if (syntax is not null)
        {
            SemanticModel model = compilation.GetSemanticModel(syntax.SyntaxTree);
            ISymbol? symbol = model.GetDeclaredSymbol(syntax);

            if (symbol is INamedTypeSymbol record && record.HasFluentify())
            {
                IReadOnlyList<Property> properties = record.GetProperties(compilation, cancellationToken);

                if (properties.Count > 0)
                {
                    bool hasDefaultConstructor = record.HasAccessibleParameterlessConstructor(compilation);
                    bool isPartial = syntax.IsPartial();
                    IReadOnlyList<Generic> generics = record.GetGenerics();

                    return new Subject
                    {
                        Generics = generics,
                        HasDefaultConstructor = hasDefaultConstructor,
                        IsPartial = isPartial,
                        Name = record.Name,
                        Namespace = symbol.ContainingNamespace.ToDisplayString(),
                        Properties = properties,
                    };
                }
            }
        }

        return default;
    }
}