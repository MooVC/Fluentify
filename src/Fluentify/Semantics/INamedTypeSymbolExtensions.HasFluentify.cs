﻿namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

using static Fluentify.FluentifyAttributeGenerator;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="record"/> provided is annotated with the Fluentify attribute.
    /// </summary>
    /// <param name="record">The symbol for the record to be checked for the presence of the Fluentify attribute.</param>
    /// <returns>True if the Fluentify attribute is present on the <paramref name="record"/>, otherwise False.</returns>
    public static bool HasFluentify(this INamedTypeSymbol record)
    {
        return record.HasAttribute(Name);
    }
}