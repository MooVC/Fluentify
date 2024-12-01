namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Fluentify.Snippets.Records;

internal sealed class Records
    : Types<RecordDeclarationSyntax>
{
    public static readonly Records Instance = new();

    private Records()
        : base(
            AllThreeIgnoredContent,
            BooleanContent,
            CrossReferencedContent,
            DescriptorOnIgnoredContent,
            DescriptorOnOptionalContent,
            DescriptorOnRequiredContent,
            GlobalContent,
            InvalidDescriptorContent,
            MultipleGenericsContent,
            NestedContent,
            OneOfThreeIgnoredContent,
            SelfDescriptorOnIgnoredContent,
            SelfDescriptorOnOptionalContent,
            SelfDescriptorOnRequiredContent,
            SimpleContent,
            SingleGenericContent,
            TwoOfThreeIgnoredContent,
            UnannotatedContent,
            UnsupportedContent)
    {
    }
}