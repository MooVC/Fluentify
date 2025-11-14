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

        InvalidDescriptor = new(
            InvalidDescriptorContent,
            nameof(InvalidDescriptor),
            InvalidDescriptorWithAgeExtensions,
            InvalidDescriptorWithNameExtensions,
            InvalidDescriptorWithAttributesExtensions);

        MultipleGenerics = new(
            MultipleGenericsContent,
            nameof(MultipleGenerics),
            MultipleGenericsWithAgeExtensions,
            MultipleGenericsWithNameExtensions,
            MultipleGenericsWithAttributesExtensions);

        NestedInClass = new(
            NestedInClassContent,
            nameof(NestedInClass),
            NestedInClassWithAgeExtensions,
            NestedInClassWithNameExtensions,
            NestedInClassWithAttributesExtensions);

        NestedInClassWithGenerics = new(
            NestedInClassWithGenericsContent,
            nameof(NestedInClassWithGenerics),
            NestedInClassWithGenericsWithAgeExtensions,
            NestedInClassWithGenericsWithNameExtensions,
            NestedInClassWithGenericsWithAttributesExtensions);

        NestedInStruct = new(
            NestedInStructContent,
            nameof(NestedInStruct),
            NestedInStructWithAgeExtensions,
            NestedInStructWithNameExtensions,
            NestedInStructWithAttributesExtensions);

        NestedInStructWithGenerics = new(
            NestedInStructWithGenericsContent,
            nameof(NestedInStructWithGenerics),
            NestedInStructWithGenericsWithAgeExtensions,
            NestedInStructWithGenericsWithNameExtensions,
            NestedInStructWithGenericsWithAttributesExtensions);

        OneOfThreeIgnored = new(
            OneOfThreeIgnoredContent,
            nameof(OneOfThreeIgnored),
            OneOfThreeIgnoredWithAgeExtensions,
            OneOfThreeIgnoredWithAttributesExtensions);

        SelfDescriptorOnIgnored = new(
            SelfDescriptorOnIgnoredContent,
            nameof(SelfDescriptorOnIgnored),
            SelfDescriptorOnIgnoredWithAgeExtensions,
            SelfDescriptorOnIgnoredWithAttributesExtensions);

        SelfDescriptorOnOptional = new(
            SelfDescriptorOnOptionalContent,
            nameof(SelfDescriptorOnOptional),
            SelfDescriptorOnOptionalWithAgeExtensions,
            SelfDescriptorOnOptionalWithNameExtensions,
            SelfDescriptorOnOptionalAttributesExtensions);

        SelfDescriptorOnRequired = new(
            SelfDescriptorOnRequiredContent,
            nameof(SelfDescriptorOnRequired),
            SelfDescriptorOnRequiredAgeExtensions,
            SelfDescriptorOnRequiredNameExtensions,
            SelfDescriptorOnRequiredWithAttributesExtensions);

        Simple = new(
            SimpleContent,
            nameof(Simple),
            SimpleWithAgeExtensions,
            SimpleWithNameExtensions,
            SimpleWithAttributesExtensions);

        Single = new(SingleContent, nameof(Single), SingleWithAgeExtensions);

        SingleGeneric = new(
            SingleGenericContent,
            nameof(SingleGeneric),
            SingleGenericWithAgeExtensions,
            SingleGenericWithNameExtensions,
            SingleGenericWithAttributesExtensions);

        SkipAutoInstantiationOnProperty = new(
            SkipAutoInstantiationOnPropertyContent,
            nameof(SkipAutoInstantiationOnProperty),
            SkipAutoInstantiationOnPropertyWithAgeExtensions,
            SkipAutoInstantiationOnPropertyWithDependencyExtensions);

        SkipAutoInstantiationOnType = new(
            SkipAutoInstantiationOnTypeContent,
            nameof(SkipAutoInstantiationOnType),
            SkipAutoInstantiationOnTypeWithAgeExtensions,
            SkipAutoInstantiationOnTypeWithDependencyExtensions);

        TwoOfThreeIgnored = new(
            TwoOfThreeIgnoredContent,
            nameof(TwoOfThreeIgnored),
            TwoOfThreeIgnoredWithAttributesExtensions);

        Unannotated = new(UnannotatedContent, nameof(Unannotated));
        Unsupported = new(UnsupportedContent, nameof(Unsupported));
    }
}