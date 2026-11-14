namespace Fluentify.Syntax;

using Fluentify.Model;

using static Fluentify.Syntax.PropertyExtensions_Resources;

/// <summary>
/// Provides extensions relating to <see cref="Property"/>.
/// </summary>
internal static partial class PropertyExtensions
{
    private static string GetScalarDelegateExtensionMethodBody(this Property property, Type type)
    {
        string initializer = string.Empty;

        if (!type.IsValueType)
        {
            initializer = type.IsBuildable
                ? string.Format(ScalarDelegateInitializationBody, type.Initialization)
                : ScalarDelegateUnbuildableBody;
            initializer = string.Concat(initializer, "\n\n");
        }

        return string.Format(ScalarDelegateMethodBody, GetParameterNullCheck("builder", "builder"), property.Name, initializer, property.Descriptor);
    }
}