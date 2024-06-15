namespace Fluentify;

using Microsoft.CodeAnalysis.CSharp.Syntax;

internal sealed class Classes
    : Types<ClassDeclarationSyntax>
{
    public static readonly Classes Instance = new();

    private const string SimpleSource = """
        [Fluentify]
        internal sealed partial class Simple
        {
            public int Age { get; init; }
            public string Name { get; init; }
            public IReadOnlyList<object>? Attributes { get; init; }
        }
        """;

    private const string SingleGenericSource = """
        [Fluentify]
        internal sealed class SingleGeneric<T>
            where T : IEnumerable
        {
            public int Age { get; init; }
            public string Name { get; init; }
            public T? Attributes { get; init; }
        }
        """;

    private const string MultipleGenericsSource = """
        [Fluentify]
        internal sealed class MultipleGenerics<T1, T2, T3>
            where T1 : struct
            where T2 : class, new()
            where T3 : IEnumerable<string>
        {
            public T1? Age { get; init; }
            public T2? Name { get; init; }
            public T3 Attributes { get; init; }
        }
        """;

    private const string CrossReferencedSource = """
        [Fluentify]
        internal sealed class CrossReferenced
        {
            public string Description { get; init; }
            public Simple Simple { get; init; }
        }
        """;

    private const string OneOfThreeIgnoredSource = """
        [Fluentify]
        internal sealed class OneOfThreeIgnored
        {
            public int Age { get; init; }
            [Ignore] public string Name { get; init; }
            public IReadOnlyList<object>? Attributes { get; init; }
        """;

    private const string TwoOfThreeIgnoredSource = """
        [Fluentify]
        internal sealed class TwoOfThreeIgnored
        {
            [Ignore] public int Age { get; init; }
            [Ignore] public string Name { get; init; }
            public IReadOnlyList<object>? Attributes { get; init; }
        }
        """;

    private const string AllThreeIgnoredSource = """
        [Fluentify]
        internal sealed class AllThreeIgnored
        {
            [Ignore] public int Age { get; init; }
            [Ignore] public string Name { get; init; }
            [Ignore] public IReadOnlyList<object>? Attributes { get; init; }
        }
        """;

    private const string DescriptorOnRequiredSource = """
        [Fluentify]
        internal sealed class DescriptorOnRequired
        {
            [Descriptor("Aged")] public int Age { get; init; }
            public string Name { get; init; }
            public IReadOnlyList<object>? Attributes { get; init; }
        }
        """;

    private const string DescriptorOnOptionalSource = """
        [Fluentify]
        internal sealed class DescriptorOnOptional
        {
            public int Age { get; init; }
            public string Name { get; init; }
            [Descriptor("AttributedWith")] public IReadOnlyList<object>? Attributes { get; init; }
        }
        """;

    private const string DescriptorOnIgnoredSource = """
        [Fluentify]
        internal sealed class DescriptorOnIgnored
        {
            public int Age { get; init; }
            [Descriptor("Named"), Ignore] public string Name { get; init; }
            public IReadOnlyList<object>? Attributes { get; init; }
        }
        """;

    private const string UnannotatedSource = """
        internal sealed class Unannotated
        {
            public int Age { get; init; }
            public string Name { get; init; }
            public IReadOnlyList<object>? Attributes { get; init; }
        }
        """;

    private const string UnsupportedSource = """
        [Fluentify]
        internal sealed class Unsupported
        {
        };
        """;

    private Classes()
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
            UnannotatedSource,
            UnsupportedSource)
    {
    }
}