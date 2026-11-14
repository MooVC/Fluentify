namespace Fluentify.Syntax;

using Fluentify.Model;

using static Fluentify.Syntax.PropertyExtensions_Resources;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetDelegateAndValuesExtensionMethodBody(this Property property, Type type)
    {
        if (property.Kind.Pattern == Pattern.Scalar || type.IsFrameworkType || !type.IsBuildable)
        {
            return default;
        }

        return string.Format(
            DelegateAndValuesMethodBody,
            GetParameterNullCheck("builder", "builder"),
            GetParameterNullCheck("values", "values"),
            property.Descriptor);
    }
}