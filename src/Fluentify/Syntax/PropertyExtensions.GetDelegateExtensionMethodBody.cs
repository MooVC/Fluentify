namespace Fluentify.Syntax;

using Fluentify.Model;

using static Fluentify.Syntax.PropertyExtensions_Resources;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetDelegateExtensionMethodBody(this Property property, Type type)
    {
        if (type.IsFrameworkType)
        {
            return default;
        }

        if (property.Kind.Pattern == Pattern.Scalar)
        {
            return property.GetScalarDelegateExtensionMethodBody(type);
        }

        if (!type.IsBuildable)
        {
            return default;
        }

        return string.Format(DelegateMethodBody, GetParameterNullCheck("builder", "builder"), type.Initialization, property.Descriptor);
    }
}