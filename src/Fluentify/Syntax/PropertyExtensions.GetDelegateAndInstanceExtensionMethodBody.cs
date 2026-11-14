namespace Fluentify.Syntax;

using Fluentify.Model;

using static Fluentify.Syntax.PropertyExtensions_Resources;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string? GetDelegateAndInstanceExtensionMethodBody(this Property property, Type type)
    {
        if (type.IsFrameworkType)
        {
            return default;
        }

        if (property.Kind.Pattern != Pattern.Scalar && !type.IsBuildable)
        {
            return default;
        }

        return string.Format(DelegateAndInstanceMethodBody, GetParameterNullCheck("builder", "builder"), property.Descriptor);
    }
}