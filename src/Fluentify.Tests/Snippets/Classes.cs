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
            SimpleWithAttributesExtensions,
            SimpleWithNameExtensions);

        DescriptorOnIgnored = new(
            DescriptorOnIgnoredContent,
            nameof(DescriptorOnIgnored),
            DescriptorOnIgnoredWithAgeExtensions,
            DescriptorOnIgnoredWithAttributesExtensions);

        DescriptorOnOptional = new(
            DescriptorOnOptionalContent,
            nameof(DescriptorOnOptional),
            DescriptorOnOptionalWithAgeExtensions,
            DescriptorOnOptionalAttributedWithExtensions,
            DescriptorOnOptionalWithNameExtensions);

        DescriptorOnRequired = new(
            DescriptorOnRequiredContent,
            nameof(DescriptorOnRequired),
            DescriptorOnRequiredAgedExtensions,
            DescriptorOnRequiredWithAttributesExtensions,
            DescriptorOnRequiredWithNameExtensions);

        Global = new(
            GlobalContent,
            nameof(Global),
            GlobalWithAgeExtensions,
            GlobalWithAttributesExtensions,
            GlobalWithNameExtensions);

        InvalidDescriptor = new(
            InvalidDescriptorContent,
            nameof(InvalidDescriptor),
            InvalidDescriptorWithAgeExtensions,
            InvalidDescriptorWithAttributesExtensions,
            InvalidDescriptorWithNameExtensions);

        MultipleGenerics = new(
            MultipleGenericsContent,
            nameof(MultipleGenerics),
            MultipleGenericsWithAgeExtensions,
            MultipleGenericsWithAttributesExtensions,
            MultipleGenericsWithNameExtensions);

        NestedInClass = new(
            NestedInClassContent,
            nameof(NestedInClass),
            NestedInClassWithAgeExtensions,
            NestedInClassWithAttributesExtensions,
            NestedInClassWithNameExtensions);

        NestedInClassWithGenerics = new(
            NestedInClassWithGenericsContent,
            nameof(NestedInClassWithGenerics),
            NestedInClassWithGenericsWithAgeExtensions,
            NestedInClassWithGenericsWithAttributesExtensions,
            NestedInClassWithGenericsWithNameExtensions);

        NestedInStruct = new(
            NestedInStructContent,
            nameof(NestedInStruct),
            NestedInStructWithAgeExtensions,
            NestedInStructWithAttributesExtensions,
            NestedInStructWithNameExtensions);

        NestedInStructWithGenerics = new(
            NestedInStructWithGenericsContent,
            nameof(NestedInStructWithGenerics),
            NestedInStructWithGenericsWithAgeExtensions,
            NestedInStructWithGenericsWithAttributesExtensions,
            NestedInStructWithGenericsWithNameExtensions);

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
            SelfDescriptorOnOptionalAttributesExtensions,
            SelfDescriptorOnOptionalWithAgeExtensions,
            SelfDescriptorOnOptionalWithNameExtensions);

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
            SimpleWithAttributesExtensions,
            SimpleWithNameExtensions);

        Single = new(SingleContent, nameof(Single), SingleWithAgeExtensions);

        SingleGeneric = new(
            SingleGenericContent,
            nameof(SingleGeneric),
            SingleGenericWithAgeExtensions,
            SingleGenericWithAttributesExtensions,
            SingleGenericWithNameExtensions);

        SkipAutoInitializationOnProperty = new(
            SkipAutoInitializationOnPropertyContent,
            nameof(SkipAutoInitializationOnProperty),
            SkipAutoInitializationOnPropertyWithAgeExtensions,
            SkipAutoInitializationOnPropertyWithDependencyExtensions);

        SkipAutoInitializationOnType = new(
            SkipAutoInitializationOnTypeContent,
            nameof(SkipAutoInitializationOnType),
            SkipAutoInitializationOnTypeWithAgeExtensions,
            SkipAutoInitializationOnTypeWithDependencyExtensions);

        TwoOfThreeIgnored = new(
            TwoOfThreeIgnoredContent,
            nameof(TwoOfThreeIgnored),
            TwoOfThreeIgnoredWithAttributesExtensions);

        Unannotated = new(UnannotatedContent, nameof(Unannotated));
        Unsupported = new(UnsupportedContent, nameof(Unsupported));
    }
}