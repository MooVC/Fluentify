namespace Fluentify.Semantics;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    /// <param name="target">The type symbol to search for a static member. Can be null.</param>
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
    public static bool TryResolve(this ITypeSymbol? target, ref string member, out string initialization)
    {
        initialization = string.Empty;

        if (target is null || string.IsNullOrWhiteSpace(member))
        {
            return false;
        }

        member = member.Extract();
        string name = target.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        IEnumerable<ISymbol> candidates = target
            .GetMembers(member)
            .Where(candidate => candidate.IsStatic);

        foreach (ISymbol candidate in candidates)
        {
            if (candidate is IMethodSymbol method && method.Parameters.Length == ExpectedParametersForFactoryMethod
             && SymbolEqualityComparer.Default.Equals(method.ReturnType, target))
            {
                initialization = $"{name}.{member}()";

                return true;
            }

            if (candidate is IPropertySymbol property && SymbolEqualityComparer.Default.Equals(property.Type, target))
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