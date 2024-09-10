namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;

internal static partial class DefinitionExtensions
{
    public static IParameterSymbol GetParameter(this Definition definition, string name)
    {
        IPropertySymbol property = definition.GetProperty(name);
        IParameterSymbol? parameter = property.GetParameter();

        return parameter!;
    }
}