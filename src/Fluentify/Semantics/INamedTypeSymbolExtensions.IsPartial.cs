namespace Fluentify.Semantics;

using System.Collections.Generic;
using Fluentify.Model;
using Fluentify.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Provides extensions relating to <see cref="INamedTypeSymbol"/>.</summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>Determines whether or not the <paramref name="symbol"/> provided is supported by Valuify.</summary>
    /// <param name="symbol">The symbol for the type to be checked for Valuify support.</param>
    /// <param name="nesting">The declaration syntax for the parents of the <paramref name="syntax"/>.</param>
    /// <returns>True if the type is annotated and partial, otherwise False.</returns>
    public static bool IsPartial(this INamedTypeSymbol symbol, Stack<Nesting> nesting)
    {
        INamedTypeSymbol? current = symbol;
        bool isPartial = true;

        do
        {
            isPartial &= current.DeclaringSyntaxReferences
                .Select(syntax => syntax.GetSyntax())
                .OfType<TypeDeclarationSyntax>()
                .All(type => type.IsPartial());

            if (current.IsSupported(out string declaration) && !current.Equals(symbol, SymbolEqualityComparer.Default))
            {
                var parent = new Nesting
                {
                    Declaration = declaration,
                    Generics = current.GetGenerics(),
                    Name = current.Name,
                    Qualification = current.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                };

                nesting.Push(parent);
            }

            current = current.ContainingType;
        }
        while (current is not null);

        return isPartial;
    }

    private static string IdentifyPrefix(this INamedTypeSymbol symbol)
    {
        string @ref = symbol.IsRefLikeType
            ? "ref"
            : string.Empty;

        string @readonly = symbol.IsReadOnly
            ? "readonly"
            : string.Empty;

        return string
            .Join(" ", @readonly, @ref)
            .Trim();
    }

    private static string IdentifyType(this INamedTypeSymbol symbol)
    {
        return symbol.TypeKind switch
        {
            TypeKind.Interface => "interface",
            TypeKind.Struct => symbol.IsRecord
                ? "record struct"
                : "struct",
            _ => symbol.IsRecord
                ? "record"
                : "class",
        };
    }

    private static bool IsSupported(this INamedTypeSymbol symbol, out string declaration)
    {
        string prefix = symbol.IdentifyPrefix();
        string type = symbol.IdentifyType();

        declaration = string
            .Join(" ", prefix, "partial", type)
            .TrimStart();

        return !string.IsNullOrEmpty(declaration);
    }
}