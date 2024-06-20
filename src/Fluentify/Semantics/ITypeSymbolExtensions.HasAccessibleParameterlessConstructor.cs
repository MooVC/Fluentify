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
        return type.HasAccessibleParameterlessConstructor(compilation, out _);
    }

    /// <summary>
    /// Determines if the <paramref name="type"/> adheres to the new() constraint.
    /// </summary>
    /// <param name="type">The <see cref="ITypeSymbol"/> for which the determination is to be carried out.</param>
    /// <param name="compilation">The <see cref="Compilation"/> used to determine if the constraint allows for internal construction.</param>
    /// <param name="isInternal">A flag indicating if the constraint allows for internal construction.</param>
    /// <returns>True if the <paramref name="type"/> adheres to the new() constraint, otherwise False.</returns>
    public static bool HasAccessibleParameterlessConstructor(this ITypeSymbol type, Compilation compilation, out bool isInternal)
    {
        isInternal = SymbolEqualityComparer.Default.Equals(compilation.Assembly, type.ContainingAssembly);

        return type.HasAccessibleParameterlessConstructor(isInternal);
    }

    private static bool HasAccessibleParameterlessConstructor(this ITypeSymbol type, bool isInternal)
    {
        bool IsAccessible(Accessibility accessibility)
        {
            return accessibility == Accessibility.Public || (isInternal && accessibility == Accessibility.Internal);
        }

        if (type.IsAbstract || type.TypeKind != TypeKind.Class || !IsAccessible(type.DeclaredAccessibility))
        {
            return false;
        }

        IMethodSymbol[] constructors = type
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Where(method => method.MethodKind == MethodKind.Constructor)
            .ToArray();

        bool IsConstructable(IMethodSymbol constructor)
        {
            return constructor.Parameters.IsEmpty && IsAccessible(constructor.DeclaredAccessibility);
        }

        bool HasAccessibleParameterlessConstructor()
        {
            return Array.Exists(constructors, IsConstructable);
        }

        return constructors.Length == 0 || HasAccessibleParameterlessConstructor();
    }
}