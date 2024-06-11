namespace Fluentify.Semantics;

using Fluentify.Model;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    private static readonly IsMatch[] strategies =
    [
        IsArray,
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

        bool isArray = Array.Exists(strategies, strategy => strategy(compilation, kind, property, cancellationToken));

        if (!isArray)
        {
            kind.Type.IsBuildable = property.Type.IsBuildable(compilation, cancellationToken);
        }

        return kind;
    }

    private static bool IsArray(Compilation compilation, Kind kind, IPropertySymbol property, CancellationToken cancellationToken)
    {
        if (property.Type is IArrayTypeSymbol array)
        {
            kind.Pattern = Pattern.Array;
            kind.Member = GetType(compilation, array.ElementType, cancellationToken);

            return true;
        }

        return false;
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
}