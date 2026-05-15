namespace Fluentify.Semantics;

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

internal sealed class PropertyNameEqualityComparer
    : IEqualityComparer<IPropertySymbol>
{
    public static readonly PropertyNameEqualityComparer Instance = new();

    /// <summary>
    /// Determines whether two IPropertySymbol instances are equal by comparing their Name values using ordinal string
    /// comparison.
    /// </summary>
    /// <remarks>Two null references are considered equal. Comparison uses StringComparer.Ordinal on the Name property.</remarks>
    /// <param name="left">The first IPropertySymbol to compare.</param>
    /// <param name="right">The second IPropertySymbol to compare.</param>
    /// <returns>True if both symbols are equal; otherwise, false.</returns>
    public bool Equals(IPropertySymbol? left, IPropertySymbol? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return StringComparer.Ordinal.Equals(left.Name, right.Name);
    }

    /// <summary>
    /// Returns a hash code for the specified IPropertySymbol based on its Name using ordinal string comparison.
    /// </summary>
    /// <remarks>
    /// Computes the hash using StringComparer.Ordinal.GetHashCode on obj.Name. Requires obj and its Name to be non-null.
    /// </remarks>
    /// <param name="property">The property symbol whose Name is used to compute the hash code.</param>
    /// <returns>A 32-bit signed integer hash code for the specified property symbol.</returns>
    public int GetHashCode(IPropertySymbol property)
    {
        return StringComparer.Ordinal.GetHashCode(property.Name);
    }
}