namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Fluentify.Snippets.Classes;

internal sealed class Classes
    : Types<ClassDeclarationSyntax>
{
    public static readonly Classes Instance = new();

    private Classes()
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