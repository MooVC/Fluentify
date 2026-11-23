namespace Fluentify;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

internal static class Frameworks
{
    private static readonly ImmutableArray<(ReferenceAssemblies Assembly, LanguageVersion Maximum, DateOnly SupportTo)> _inScope =
    [
        (ReferenceAssemblies.Net.Net50, LanguageVersion.CSharp9, new DateOnly(2022, 5, 10)),
        (ReferenceAssemblies.Net.Net60, LanguageVersion.CSharp10, new DateOnly(2024, 11, 12)),
        (ReferenceAssemblies.Net.Net70, LanguageVersion.CSharp11, new DateOnly(2024, 5, 14)),
        (ReferenceAssemblies.Net.Net80, LanguageVersion.CSharp12, new DateOnly(2026, 11, 10)),
        (ReferenceAssemblies.Net.Net90, LanguageVersion.CSharp13, new DateOnly(2026, 11, 10)),
        (ReferenceAssemblies.Net.Net100, LanguageVersion.CSharp14, new DateOnly(2028, 11, 14)),
        (
            new ReferenceAssemblies("net11.0", new PackageIdentity("Microsoft.NETCore.App.Ref", "11.0.0"), Path.Combine("ref", "net11.0")),
            LanguageVersion.CSharp15,
            new DateOnly(2028, 11, 14)),
        (ReferenceAssemblies.NetCore.NetCoreApp20, LanguageVersion.CSharp7_3, new DateOnly(2018, 10, 1)),
        (ReferenceAssemblies.NetCore.NetCoreApp21, LanguageVersion.CSharp7_3, new DateOnly(2021, 8, 21)),
        (ReferenceAssemblies.NetCore.NetCoreApp30, LanguageVersion.CSharp8, new DateOnly(2020, 3, 3)),
        (ReferenceAssemblies.NetCore.NetCoreApp31, LanguageVersion.CSharp8, new DateOnly(2022, 12, 3)),
        (ReferenceAssemblies.NetFramework.Net462.Default, LanguageVersion.CSharp7_3, new DateOnly(2022, 4, 26)),
        (ReferenceAssemblies.NetFramework.Net47.Default, LanguageVersion.CSharp7_3, DateOnly.MaxValue),
        (ReferenceAssemblies.NetFramework.Net471.Default, LanguageVersion.CSharp7_3, DateOnly.MaxValue),
        (ReferenceAssemblies.NetFramework.Net472.Default, LanguageVersion.CSharp7_3, DateOnly.MaxValue),
        (ReferenceAssemblies.NetFramework.Net48.Default, LanguageVersion.CSharp7_3, DateOnly.MaxValue),
        (ReferenceAssemblies.NetStandard.NetStandard20, LanguageVersion.CSharp7_3, DateOnly.MaxValue),
        (ReferenceAssemblies.NetStandard.NetStandard21, LanguageVersion.CSharp8, DateOnly.MaxValue),
    ];

#if CI
    private static readonly LanguageVersion[] _languages = Enum.GetValues<LanguageVersion>();
#endif

    public static IEnumerable<(ReferenceAssemblies Assembly, LanguageVersion Language)> Cases(LanguageVersion minimum)
    {
#if CI
        foreach ((ReferenceAssemblies assembly, LanguageVersion maximum, _) in _inScope)
        {
            foreach (LanguageVersion language in _languages.Where(language => language >= minimum && language <= maximum))
            {
                yield return (assembly, language);
            }
        }
#else
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach ((ReferenceAssemblies assembly, LanguageVersion maximum, DateOnly supportTo) in _inScope)
        {
            if (supportTo >= today && maximum >= minimum)
            {
                yield return (assembly, maximum);
            }
        }
#endif
    }

    public static IEnumerable<TestDataRow<(ReferenceAssemblies Assembly, LanguageVersion Language)>> DefaultCases()
    {
        foreach ((ReferenceAssemblies assembly, LanguageVersion language) in Cases(LanguageVersion.CSharp7_3))
        {
            yield return new((assembly, language), DisplayName: $"{assembly.TargetFramework} ({language})");
        }
    }
}