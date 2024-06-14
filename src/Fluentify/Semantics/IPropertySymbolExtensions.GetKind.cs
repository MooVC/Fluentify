namespace Fluentify.Semantics;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    private const string Collection = "global::System.Collections.Generic.ICollection<T>";
    private const string Enumerable = "global::System.Collections.Generic.IEnumerable<T>";
    private const string ReadOnlyList = "global::System.Collections.Generic.IReadOnlyList<T>";
    private const string ReadOnlyCollection = "global::System.Collections.Generic.IReadOnlyCollection<T>";

    private static readonly IsMatch[] strategies =
    [
        IsArray,
        IsCollection,
        IsEnumerable,
    ];

    private delegate bool IsMatch(Compilation compilation, Kind kind, IPropertySymbol property, CancellationToken cancellationToken);

    public static Kind GetKind(this IPropertySymbol property, Compilation compilation, CancellationToken cancellationToken)
    {
        var kind = new Kind
        {
            Pattern = Pattern.Scalar,
            Type = new()
            {
                IsNullable = property.Type.IsNullable(),
                Name = property.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            },
        };

        kind.Type.IsBuildable = property.Type.IsBuildable(compilation, cancellationToken);

        _ = Array.Exists(strategies, strategy => strategy(compilation, kind, property, cancellationToken));

        return kind;
    }

    private static bool IsArray(Compilation compilation, Kind kind, IPropertySymbol property, CancellationToken cancellationToken)
    {
        if (property.Type is not IArrayTypeSymbol array)
        {
            return false;
        }

        kind.Pattern = Pattern.Array;
        kind.Member = GetType(compilation, array.ElementType, cancellationToken);

        return true;
    }

    private static bool IsCollection(Compilation compilation, Kind kind, IPropertySymbol property, CancellationToken cancellationToken)
    {
        if (!(kind.Type.IsBuildable && property.IsType(out ITypeSymbol type, Collection)))
        {
            return false;
        }

        kind.Pattern = Pattern.Collection;
        kind.Member = GetType(compilation, type, cancellationToken);

        return true;
    }

    private static bool IsEnumerable(Compilation compilation, Kind kind, IPropertySymbol property, CancellationToken cancellationToken)
    {
        if (property.Type is not INamedTypeSymbol type
         || type.TypeArguments.Length != 1
         || !type.IsType(Enumerable, ReadOnlyCollection, ReadOnlyList))
        {
            return false;
        }

        kind.Pattern = Pattern.Enumerable;
        kind.Member = GetType(compilation, type.TypeArguments[0], cancellationToken);

        return true;
    }

    private static Type GetType(Compilation compilation, ITypeSymbol type, CancellationToken cancellationToken)
    {
        return new()
        {
            IsBuildable = type.IsBuildable(compilation, cancellationToken),
            IsNullable = type.IsNullable(),
            Name = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        };
    }

    private static bool IsType(this IPropertySymbol property, out ITypeSymbol element, params string[] names)
    {
        foreach (INamedTypeSymbol @interface in property.Type.AllInterfaces.Where(@interface => @interface.TypeArguments.Length == 1))
        {
            if (@interface.OriginalDefinition is INamedTypeSymbol type && type.IsType(names))
            {
                element = @interface.TypeArguments[0];

                return true;
            }
        }

        element = property.Type;

        return false;
    }

    private static bool IsType(this INamedTypeSymbol type, params string[] names)
    {
        string name = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return Array.Exists(names, candidate => candidate.Equals(name));
    }
}