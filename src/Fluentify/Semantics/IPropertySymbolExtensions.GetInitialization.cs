namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;
using static Fluentify.AutoInitiateWithAttributeGenerator;
using static Fluentify.Semantics.AutoInitiationResolver;

/// <summary>
/// Provides extensions relating to <see cref="IPropertySymbol"/>.
/// </summary>
internal static partial class IPropertySymbolExtensions
{
    public static bool TryGetInitialization(this IPropertySymbol property, out Initialization initialization)
    {
        initialization = default;

        if (property.HasSkipAutoInitialization())
        {
            return false;
        }

        AttributeData? attribute = property.GetAttribute(Name)
            ?? property.GetParameter()?.GetAttribute(Name)
            ?? property.Type.GetAttribute(Name);

        if (attribute is null || !attribute.TryGetMember(out string member))
        {
            return false;
        }

        return TryResolve(property.Type, member, out initialization);
    }
}
