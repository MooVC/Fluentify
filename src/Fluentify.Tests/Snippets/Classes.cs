namespace Fluentify.Snippets;

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

public static partial class Classes
{
    public static readonly LanguageVersion LanguageVersion = LanguageVersion.CSharp7_3;

    public static readonly ReferenceAssemblies ReferenceAssemblies = ReferenceAssemblies.NetStandard.NetStandard20;

    [SuppressMessage("Minor Code Smell", "S3963:\"static\" fields should be initialized inline", Justification = "Needed due to order constraint.")]
    static Classes()
    {
        AllThreeIgnored = new(AllThreeIgnoredContent, nameof(AllThreeIgnored));

        Boolean = new(
            BooleanContent,
            nameof(Boolean),
            BooleanWithAgeExtensions,
            BooleanWithNameExtensions,
            BooleanIsRetiredExtensions);

        CrossReferenced = new(
            [CrossReferencedContent, SimpleContent],
            nameof(CrossReferenced),
            CrossReferencedWithDescriptionExtensions,
            CrossReferencedWithSimpleExtensions,
            SimpleWithAgeExtensions,
            SimpleWithNameExtensions,
            SimpleWithAttributesExtensions);

        DescriptorOnIgnored = new(
            DescriptorOnIgnoredContent,
            nameof(DescriptorOnIgnored),
            DescriptorOnIgnoredWithAgeExtensions,
            DescriptorOnIgnoredWithAttributesExtensions);

        DescriptorOnOptional = new(
            DescriptorOnOptionalContent,
            nameof(DescriptorOnOptional),
            DescriptorOnOptionalWithAgeExtensions,
            DescriptorOnOptionalWithNameExtensions,
            DescriptorOnOptionalAttributedWithExtensions);

        DescriptorOnRequired = new(
            DescriptorOnRequiredContent,
            nameof(DescriptorOnRequired),
            DescriptorOnRequiredAgedExtensions,
            DescriptorOnRequiredWithNameExtensions,
            DescriptorOnRequiredWithAttributesExtensions);

        Global = new(
            GlobalContent,
            nameof(Global),
            GlobalWithAgeExtensions,
            GlobalWithNameExtensions,
            GlobalWithAttributesExtensions);

        MultipleGenerics = new(
            MultipleGenericsContent,
            nameof(MultipleGenerics),
            MultipleGenericsWithAgeExtensions,
            MultipleGenericsWithNameExtensions,
            MultipleGenericsWithAttributesExtensions);

        OneOfThreeIgnored = new(
            OneOfThreeIgnoredContent,
            nameof(OneOfThreeIgnored),
            OneOfThreeIgnoredWithAgeExtensions,
            OneOfThreeIgnoredWithAttributesExtensions);

        Simple = new(
            SimpleContent,
            nameof(Simple),
            SimpleWithAgeExtensions,
            SimpleWithNameExtensions,
            SimpleWithAttributesExtensions);

        SingleGeneric = new(
            SingleGenericContent,
            nameof(SingleGeneric),
            SingleGenericWithAgeExtensions,
            SingleGenericWithNameExtensions,
            SingleGenericWithAttributesExtensions);

        TwoOfThreeIgnored = new(
            TwoOfThreeIgnoredContent,
            nameof(TwoOfThreeIgnored),
            TwoOfThreeIgnoredWithAttributesExtensions);

        Unannotated = new(UnannotatedContent, nameof(Unannotated));
        Unsupported = new(UnsupportedContent, nameof(Unsupported));
    }
}