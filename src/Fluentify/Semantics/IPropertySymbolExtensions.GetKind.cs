namespace Fluentify.Semantics;

using Fluentify.Model;
using Microsoft.CodeAnalysis;
using static Fluentify.SkipAutoInstantiationAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    private const string Collection = "global::System.Collections.Generic.ICollection<T>";
    private const string Enumerable = "global::System.Collections.Generic.IEnumerable<T>";
    private const string ReadOnlyCollection = "global::System.Collections.Generic.IReadOnlyCollection<T>";
    private const string ReadOnlyList = "global::System.Collections.Generic.IReadOnlyList<T>";
    private const string ImmutableArray = "global::System.Collections.Immutable.ImmutableArray<T>";
    private const string ImmutableHashSet = "global::System.Collections.Immutable.ImmutableHashSet<T>";
    private const string ImmutableList = "global::System.Collections.Immutable.ImmutableList<T>";
    private const string ImmutableSortedSet = "global::System.Collections.Immutable.ImmutableSortedSet<T>";
    private const int ExpectedArgumentsForCollectionType = 1;

    private static readonly string[] _collections =
    [
        Enumerable,
        ImmutableArray,
        ImmutableHashSet,
        ImmutableList,
        ImmutableSortedSet,
        ReadOnlyCollection,
        ReadOnlyList,
    ];

    private static readonly IsMatch[] _strategies =
    [
        IsArray,
        IsCollection,
        IsEnumerable,
    ];

    private static readonly SymbolDisplayFormat _fullyQualifiedNonNullable = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    private delegate bool IsMatch(Compilation compilation, Kind kind, IPropertySymbol property, CancellationToken cancellationToken);

    /// <summary>
    /// Gets information relating to the data type captured by the property in the form of a <see cref="Kind"/>.
    /// </summary>
    /// <param name="property">The property from which the type infromation is to be obtained.</param>
    /// <param name="compilation">The <see cref="Compilation"/> used to determine if the data type allows for internal construction.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> that can be used to cancel the operation.</param>
    /// <returns>The <see cref="Kind"/> that encapsulates the information relating to the data type of the <paramref name="property"/>.</returns>
    public static Kind GetKind(this IPropertySymbol property, Compilation compilation, CancellationToken cancellationToken)
    {
        bool hasSkipAutoInstantiation = property.HasSkipAutoInstantiation();
        bool hasInitialization = property.TryGetInitialization(out string initialization);

        string typeInitialization = initialization;
        bool hasTypeInitialization = !hasInitialization && property.Type.TryGetInitialization(out typeInitialization);

        bool isBuildable = property.Type.IsBuildable(compilation, cancellationToken)
            || hasInitialization
            || hasTypeInitialization;

        string effectiveInitialization = hasInitialization
            ? initialization
            : typeInitialization;

        var kind = new Kind
        {
            Pattern = Pattern.Scalar,
            Type = new()
            {
                Initialization = GetInitialization(effectiveInitialization, property.Type),
                IsFrameworkType = property.Type.IsFrameworkType(),
                IsNullable = property.Type.IsNullable(),
                IsValueType = property.Type.IsValueType(),
                Name = property.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            },
        };

        kind.Type.IsBuildable = !hasSkipAutoInstantiation
            && !property.HasSkipAutoInitialization()
            && isBuildable;

        _ = Array.Exists(_strategies, strategy => strategy(compilation, kind, property, cancellationToken));

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
         || type.TypeArguments.Length != ExpectedArgumentsForCollectionType
         || !type.OriginalDefinition.IsType(_collections))
        {
            return false;
        }

        kind.Pattern = Pattern.Enumerable;
        kind.Member = GetType(compilation, type.TypeArguments[0], cancellationToken);

        return true;
    }

    private static Type GetType(Compilation compilation, ITypeSymbol type, CancellationToken cancellationToken)
    {
        bool hasInitialization = type.TryGetInitialization(out string initialization);

        bool isBuildable = (hasInitialization || type.IsBuildable(compilation, cancellationToken))
            && !type.HasAttribute(Name);

        return new()
        {
            Initialization = GetInitialization(initialization, type),
            IsBuildable = isBuildable && !type.HasAttribute(Name),
            IsFrameworkType = type.IsFrameworkType(),
            IsNullable = type.IsNullable(),
            IsValueType = type.IsValueType(),
            Name = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        };
    }

    private static string GetInitialization(string initialization, ITypeSymbol type)
    {
        return GetInitialization(initialization, name => $"new {name}()", type);
    }

    private static string GetInitialization(string initialization, Func<string, string> initializer, ITypeSymbol type)
    {
        if (!string.IsNullOrWhiteSpace(initialization))
        {
            return initialization;
        }

        string name;

        if (type is INamedTypeSymbol named && named.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
        {
            type = named.TypeArguments[0].WithNullableAnnotation(NullableAnnotation.NotAnnotated);
        }

        name = type.ToDisplayString(NullableFlowState.NotNull, _fullyQualifiedNonNullable);

        return initializer(name);
    }

    private static bool IsType(this IPropertySymbol property, out ITypeSymbol element, params string[] names)
    {
        foreach (INamedTypeSymbol @interface in property.Type.AllInterfaces
            .Where(@interface => @interface.TypeArguments.Length == ExpectedArgumentsForCollectionType))
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