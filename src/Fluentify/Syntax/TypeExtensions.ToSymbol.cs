namespace Fluentify.Syntax;

using MooVC.Syntax;
using MooVC.Syntax.CSharp;
using Type = Fluentify.Model.Type;

internal static partial class TypeExtensions
{
    public static Symbol ToSymbol(this Type type, bool supportsNullableReferenceTypes)
    {
        return type.ToString(supportsNullableReferenceTypes).ToTypeSymbol();
    }
}