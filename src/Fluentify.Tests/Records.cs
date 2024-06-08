namespace Fluentify;

using Microsoft.CodeAnalysis.CSharp.Syntax;

internal sealed class Records
    : Types<RecordDeclarationSyntax>
{
    public static readonly Records Instance = new();

    private const string SimpleSource = """
        [Fluentify]
        internal sealed partial record Simple(int Age, string Name, IReadOnlyList<object>? Attributes = default);
        """;

    private const string SingleGenericSource = """
        [Fluentify]
        internal sealed record SingleGeneric<T>(int Age, string Name, T? Attributes = default)
            where T : IEnumerable;
        """;

    private const string MultipleGenericsSource = """
        [Fluentify]
        internal sealed record MultipleGenerics<T1, T2, T3>(T1? Age, T2? Name, T3 Attributes)
            where T1 : struct
            where T2 : class, new()
            where T3 : IEnumerable<string>;
        """;

    private const string CrossReferencedSource = """
        [Fluentify]
        internal sealed record CrossReferenced(string Description, Simple Simple);
        """;

    private const string OneOfThreeIgnoredSource = """
        [Fluentify]
        internal sealed record OneOfThreeIgnored(int Age, [Ignore] string Name, IReadOnlyList<object>? Attributes = default);
        """;

    private const string TwoOfThreeIgnoredSource = """
        [Fluentify]
        internal sealed record TwoOfThreeIgnored([Ignore]int Age, [Ignore] string Name, IReadOnlyList<object>? Attributes = default);
        """;

    private const string AllThreeIgnoredSource = """
        [Fluentify]
        internal sealed record AllThreeIgnored([Ignore]int Age, [Ignore] string Name, [Ignore] IReadOnlyList<object>? Attributes = default);
        """;

    private const string DescriptorOnRequiredSource = """
        [Fluentify]
        internal sealed record DescriptorOnRequired([Descriptor("Aged")] int Age, string Name, IReadOnlyList<object>? Attributes = default);
        """;

    private const string DescriptorOnOptionalSource = """
        [Fluentify]
        internal sealed record DescriptorOnOptional(int Age, string Name, [Descriptor("AttributedWith")] IReadOnlyList<object>? Attributes = default);
        """;

    private const string DescriptorOnIgnoredSource = """
        [Fluentify]
        internal sealed record DescriptorOnIgnored(int Age, [Descriptor("Named"), Ignore] string Name, IReadOnlyList<object>? Attributes = default);
        """;

    private const string UnannotatedSource = """
        internal sealed record Unannotated(int Age, string Name, IReadOnlyList<object>? Attributes = default);
        """;

    private Records()
        : base(
            CrossReferencedSource,
            MultipleGenericsSource,
            SimpleSource,
            SingleGenericSource,
            OneOfThreeIgnoredSource,
            TwoOfThreeIgnoredSource,
            AllThreeIgnoredSource,
            DescriptorOnRequiredSource,
            DescriptorOnOptionalSource,
            DescriptorOnIgnoredSource,
            UnannotatedSource)
    {
    }
}