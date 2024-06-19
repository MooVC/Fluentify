namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    /// <summary>
    /// Determines if the <paramref name="type"/> adheres to the new() constraint.
    /// </summary>
    /// <param name="type">The <see cref="ITypeSymbol"/> for which the determination is to be carried out.</param>
    /// <param name="compilation">The <see cref="Compilation"/> used to determine if the constraint allows for internal construction.</param>
    /// <returns>True if the <paramref name="type"/> adheres to the new() constraint, otherwise False.</returns>
    public static bool HasAccessibleParameterlessConstructor(this ITypeSymbol type, Compilation compilation)
    {
        bool isInternal = type
            .ContainingAssembly
            .Equals(compilation.Assembly, SymbolEqualityComparer.Default);

        return type.HasAccessibleParameterlessConstructor(isInternal);
    }

    /// <summary>
    /// Determines if the <paramref name="type"/> adheres to the new() constraint.
    /// </summary>
    /// <param name="type">The <see cref="ITypeSymbol"/> for which the determination is to be carried out.</param>
    /// <param name="isInternal">A flag indicating if the constraint allows for internal construction.</param>
    /// <returns>True if the <paramref name="type"/> adheres to the new() constraint, otherwise False.</returns>
    public static bool HasAccessibleParameterlessConstructor(this ITypeSymbol type, bool isInternal)
    {
        if (type.IsValueType)
        {
            return false;
        }

        IMethodSymbol[] constructors = type
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Where(method => method.MethodKind == MethodKind.Constructor)
            .ToArray();

        bool IsAccessible(Accessibility accessibility)
        {
            return accessibility == Accessibility.Public || (isInternal && accessibility == Accessibility.Internal);
        }

        bool IsConstructable(IMethodSymbol constructor)
        {
            return constructor.Parameters.IsEmpty && IsAccessible(constructor.DeclaredAccessibility);
        }
 
        return constructors.Length == 0 || Array.Exists(constructors, IsConstructable);
    }
}