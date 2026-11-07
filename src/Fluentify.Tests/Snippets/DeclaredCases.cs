namespace Fluentify.Snippets;

using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

internal static class DeclaredCases
{
    public static IEnumerable<TestDataRow<(Declared Declaration, ReferenceAssemblies Assembly, LanguageVersion Language)>> ForClasses()
    {
        return Create(typeof(Classes), Frameworks.Cases(Classes.LanguageVersion));
    }

    public static IEnumerable<TestDataRow<(Declared Declaration, ReferenceAssemblies Assembly, LanguageVersion Language)>> ForRecords()
    {
        return Create(
            typeof(Records),
            Frameworks.Cases(Records.LanguageVersion)
                .Prepend((Records.ReferenceAssemblies, Records.LanguageVersion))
                .Distinct());
    }

    private static IEnumerable<TestDataRow<(Declared Declaration, ReferenceAssemblies Assembly, LanguageVersion Language)>> Create(
        Type source,
        IEnumerable<(ReferenceAssemblies Assembly, LanguageVersion Language)> targets)
    {
        Declared[] declarations = GetDeclarations(source).ToArray();

        foreach ((ReferenceAssemblies assembly, LanguageVersion language) in targets)
        {
            foreach (Declared declaration in declarations)
            {
                yield return new((declaration, assembly, language), DisplayName: $"{declaration.Name} ({assembly.TargetFramework}, {language})");
            }
        }
    }

    private static IEnumerable<Declared> GetDeclarations(Type source)
    {
        return source.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field => field.FieldType == typeof(Declared))
            .Select(field => field.GetValue(null))
            .OfType<Declared>();
    }
}