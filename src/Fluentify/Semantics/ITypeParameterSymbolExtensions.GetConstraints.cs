namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeParameterSymbol"/>.
/// </summary>
internal static partial class ITypeParameterSymbolExtensions
{
    /// <summary>
    /// Identifies the type constraints that applied to the specified <see cref="ITypeParameterSymbol"/>.
    /// </summary>
    /// <param name="parameter">The parameter for which the constraints are identified.</param>
    /// <returns>A collection of constraints that directly relate to <paramref name="parameter"/>.</returns>
    public static IReadOnlyList<string> GetConstraints(this ITypeParameterSymbol parameter)
    {
        var constraints = new List<string>();

        if (parameter.HasReferenceTypeConstraint)
        {
            constraints.Add("class");
        }

        if (parameter.HasValueTypeConstraint)
        {
            constraints.Add("struct");
        }

        if (parameter.HasNotNullConstraint)
        {
            constraints.Add("notnull");
        }

        if (parameter.ConstraintTypes.Length > 0)
        {
            IEnumerable<string> types = parameter
            .ConstraintTypes
            .Select(type => type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            constraints.AddRange(types);
        }

        if (parameter.HasConstructorConstraint)
        {
            constraints.Add("new()");
        }

        return [.. constraints];
    }
}