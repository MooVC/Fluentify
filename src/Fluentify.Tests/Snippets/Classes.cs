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
        AllThreeIgnored = new(
            AllThreeIgnoredContent,
            nameof(AllThreeIgnored),
            AllThreeIgnoredWithExtensions);

        Boolean = new(
            BooleanContent,
            nameof(Boolean),
            BooleanWithAgeExtensions,
            BooleanWithNameExtensions,
            BooleanIsRetiredExtensions,
            BooleanWithExtensions);

        CrossReferenced = new(
            [CrossReferencedContent, SimpleContent],
            nameof(CrossReferenced),
            CrossReferencedWithDescriptionExtensions,
            CrossReferencedWithSimpleExtensions,
            CrossReferencedWithExtensions,
            SimpleWithAgeExtensions,
            SimpleWithAttributesExtensions,
            SimpleWithNameExtensions,
            SimpleWithExtensions);

        DescriptorOnIgnored = new(
            DescriptorOnIgnoredContent,
            nameof(DescriptorOnIgnored),
            DescriptorOnIgnoredWithAgeExtensions,
            DescriptorOnIgnoredWithAttributesExtensions,
            DescriptorOnIgnoredWithExtensions);

        DescriptorOnOptional = new(
            DescriptorOnOptionalContent,
            nameof(DescriptorOnOptional),
            DescriptorOnOptionalWithAgeExtensions,
            DescriptorOnOptionalAttributedWithExtensions,
            DescriptorOnOptionalWithNameExtensions,
            DescriptorOnOptionalWithExtensions);

        DescriptorOnRequired = new(
            DescriptorOnRequiredContent,
            nameof(DescriptorOnRequired),
            DescriptorOnRequiredAgedExtensions,
            DescriptorOnRequiredWithAttributesExtensions,
            DescriptorOnRequiredWithNameExtensions,
            DescriptorOnRequiredWithExtensions);

        Global = new(
            GlobalContent,
            nameof(Global),
            GlobalWithAgeExtensions,
            GlobalWithAttributesExtensions,
            GlobalWithNameExtensions,
            GlobalWithExtensions);

        InvalidDescriptor = new(
            InvalidDescriptorContent,
            nameof(InvalidDescriptor),
            InvalidDescriptorWithAgeExtensions,
            InvalidDescriptorWithAttributesExtensions,
            InvalidDescriptorWithNameExtensions,
            InvalidDescriptorWithExtensions);

        MultipleGenerics = new(
            MultipleGenericsContent,
            nameof(MultipleGenerics),
            MultipleGenericsWithAgeExtensions,
            MultipleGenericsWithAttributesExtensions,
            MultipleGenericsWithNameExtensions,
            MultipleGenericsWithExtensions);

        NestedInClass = new(
            NestedInClassContent,
            nameof(NestedInClass),
            NestedInClassWithAgeExtensions,
            NestedInClassWithAttributesExtensions,
            NestedInClassWithNameExtensions,
            NestedInClassWithExtensions);

        NestedInClassWithGenerics = new(
            NestedInClassWithGenericsContent,
            nameof(NestedInClassWithGenerics),
            NestedInClassWithGenericsWithAgeExtensions,
            NestedInClassWithGenericsWithAttributesExtensions,
            NestedInClassWithGenericsWithNameExtensions,
            NestedInClassWithGenericsWithExtensions);

        NestedInStruct = new(
            NestedInStructContent,
            nameof(NestedInStruct),
            NestedInStructWithAgeExtensions,
            NestedInStructWithAttributesExtensions,
            NestedInStructWithNameExtensions,
            NestedInStructWithExtensions);

        NestedInStructWithGenerics = new(
            NestedInStructWithGenericsContent,
            nameof(NestedInStructWithGenerics),
            NestedInStructWithGenericsWithAgeExtensions,
            NestedInStructWithGenericsWithAttributesExtensions,
            NestedInStructWithGenericsWithNameExtensions,
            NestedInStructWithGenericsWithExtensions);

        OneOfThreeIgnored = new(
            OneOfThreeIgnoredContent,
            nameof(OneOfThreeIgnored),
            OneOfThreeIgnoredWithAgeExtensions,
            OneOfThreeIgnoredWithAttributesExtensions,
            OneOfThreeIgnoredWithExtensions);

        SelfDescriptorOnIgnored = new(
            SelfDescriptorOnIgnoredContent,
            nameof(SelfDescriptorOnIgnored),
            SelfDescriptorOnIgnoredWithAgeExtensions,
            SelfDescriptorOnIgnoredWithAttributesExtensions,
            SelfDescriptorOnIgnoredWithExtensions);

        SelfDescriptorOnOptional = new(
            SelfDescriptorOnOptionalContent,
            nameof(SelfDescriptorOnOptional),
            SelfDescriptorOnOptionalAttributesExtensions,
            SelfDescriptorOnOptionalWithAgeExtensions,
            SelfDescriptorOnOptionalWithNameExtensions,
            SelfDescriptorOnOptionalWithExtensions);

        SelfDescriptorOnRequired = new(
            SelfDescriptorOnRequiredContent,
            nameof(SelfDescriptorOnRequired),
            SelfDescriptorOnRequiredAgeExtensions,
            SelfDescriptorOnRequiredNameExtensions,
            SelfDescriptorOnRequiredWithAttributesExtensions,
            SelfDescriptorOnRequiredWithExtensions);

        Simple = new(
            SimpleContent,
            nameof(Simple),
            SimpleWithAgeExtensions,
            SimpleWithAttributesExtensions,
            SimpleWithNameExtensions,
            SimpleWithExtensions);

        Single = new(
            SingleContent,
            nameof(Single),
            SingleWithAgeExtensions,
            SingleWithExtensions);

        SingleGeneric = new(
            SingleGenericContent,
            nameof(SingleGeneric),
            SingleGenericWithAgeExtensions,
            SingleGenericWithAttributesExtensions,
            SingleGenericWithNameExtensions,
            SingleGenericWithExtensions);

        SkipAutoInitializationOnProperty = new(
            SkipAutoInitializationOnPropertyContent,
            nameof(SkipAutoInitializationOnProperty),
            SkipAutoInitializationOnPropertyWithAgeExtensions,
            SkipAutoInitializationOnPropertyWithDependencyExtensions,
            SkipAutoInitializationOnPropertyWithExtensions);

        SkipAutoInitializationOnType = new(
            SkipAutoInitializationOnTypeContent,
            nameof(SkipAutoInitializationOnType),
            SkipAutoInitializationOnTypeWithAgeExtensions,
            SkipAutoInitializationOnTypeWithDependencyExtensions,
            SkipAutoInitializationOnTypeWithExtensions);

        TwoOfThreeIgnored = new(
            TwoOfThreeIgnoredContent,
            nameof(TwoOfThreeIgnored),
            TwoOfThreeIgnoredWithAttributesExtensions,
            TwoOfThreeIgnoredWithExtensions);

        Unannotated = new(UnannotatedContent, nameof(Unannotated));
        Unsupported = new(UnsupportedContent, nameof(Unsupported));
    }
}