namespace Fluentify.Semantics;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    private const int ExpectedParametersForFactoryMethod = 0;

    private static readonly Regex _nameof = new(
        @"^nameof\(\s*([A-Za-z_][A-Za-z0-9_]*(?:\.[A-Za-z_][A-Za-z0-9_]*)*)\s*\)$",
        RegexOptions.CultureInvariant);

    /// <summary>
    /// Attempts to resolve a static parameterless method or static property on the specified type that returns the type
    /// itself, and constructs an initialization expression if successful.
    /// </summary>
    /// <param name="source">The type symbol to search for a static member. Can be <see langword="null"/>.</param>
    /// <param name="member">The name of the static method or property to resolve. Must not be null, empty, or whitespace.</param>
    /// <param name="initialization">When this method returns <see langword="true"/>, contains the constructed initialization expression for the
    /// resolved member; otherwise, contains the default value.</param>
    /// <returns>
    /// <see langword="true" /> if a suitable static method or property is found and an initialization expression is constructed;
    /// otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// This method only considers static members of the specified type that either are parameterless
    /// methods or properties returning the type itself. If multiple matching members exist, only the first one found is used.
    /// </remarks>
    public static bool TryResolve(this ITypeSymbol? source, ref string member, out string initialization)
    {
        return source.TryResolve(source, ref member, out initialization);
    }

    /// <summary>
    /// Attempts to resolve a static parameterless method or static property on the specified type that returns the type
    /// itself, and constructs an initialization expression if successful.
    /// </summary>
    /// <param name="source">The type symbol to search for a static member. Can be <see langword="null"/>.</param>
    /// <param name="type">The type symbol required for instantiation. Can be <see langword="null"/>.</param>
    /// <param name="member">The name of the static method or property to resolve. Must not be null, empty, or whitespace.</param>
    /// <param name="initialization">When this method returns <see langword="true"/>, contains the constructed initialization expression for the
    /// resolved member; otherwise, contains the default value.</param>
    /// <returns>
    /// <see langword="true" /> if a suitable static method or property is found and an initialization expression is constructed;
    /// otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// This method only considers static members of the specified type that either are parameterless
    /// methods or properties returning the type itself. If multiple matching members exist, only the first one found is used.
    /// </remarks>
    public static bool TryResolve(this ITypeSymbol? source, ITypeSymbol? type, ref string member, out string initialization)
    {
        initialization = string.Empty;

        if (source is null || type is null || string.IsNullOrWhiteSpace(member))
        {
            return false;
        }

        member = member.Extract();
        string name = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        IEnumerable<ISymbol> candidates = source
            .GetMembers(member)
            .Where(candidate => candidate.IsStatic && candidate.DeclaredAccessibility >= Accessibility.Internal);

        foreach (ISymbol candidate in candidates)
        {
            if (candidate is IMethodSymbol method && method.Parameters.Length == ExpectedParametersForFactoryMethod
             && SymbolEqualityComparer.Default.Equals(method.ReturnType, type))
            {
                initialization = $"{name}.{member}()";

                return true;
            }

            if (candidate is IPropertySymbol property && SymbolEqualityComparer.Default.Equals(property.Type, type))
            {
                initialization = $"{name}.{member}";

                return true;
            }

            if (candidate is IFieldSymbol field && SymbolEqualityComparer.Default.Equals(field.Type, type))
            {
                initialization = $"{name}.{member}";

                return true;
            }
        }

        return false;
    }

    private static string Extract(this string member)
    {
        if (string.IsNullOrWhiteSpace(member))
        {
            return string.Empty;
        }

        Match match = _nameof.Match(member);

        return match.Success
            ? match.Groups[1].Value
            : member;
    }
}