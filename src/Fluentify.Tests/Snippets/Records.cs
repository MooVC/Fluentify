namespace Fluentify.Snippets;

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

public static partial class Records
{
    public static readonly LanguageVersion LanguageVersion = LanguageVersion.CSharp9;

    public static readonly ReferenceAssemblies ReferenceAssemblies = ReferenceAssemblies.Net.Net80;

    [SuppressMessage("Minor Code Smell", "S3963:\"static\" fields should be initialized inline", Justification = "Needed due to order constraint.")]
    static Records()
    {
        AllThreeIgnored = new(AllThreeIgnoredContent, nameof(AllThreeIgnored), AllThreeIgnoredConstructor);

        Boolean = new(
            BooleanContent,
            nameof(Boolean),
            BooleanConstructor,
            BooleanIsRetiredExtensions,
            BooleanWithAgeExtensions,
            BooleanWithNameExtensions);

        CrossReferenced = new(
            [CrossReferencedContent, SimpleContent],
            nameof(CrossReferenced),
            CrossReferencedConstructor,
            CrossReferencedWithDescriptionExtensions,
            CrossReferencedWithSimpleExtensions,
            SimpleConstructor,
            SimpleWithAgeExtensions,
            SimpleWithAttributesExtensions,
            SimpleWithNameExtensions);

        DescriptorOnIgnored = new(
            DescriptorOnIgnoredContent,
            nameof(DescriptorOnIgnored),
            DescriptorOnIgnoredConstructor,
            DescriptorOnIgnoredWithAgeExtensions,
            DescriptorOnIgnoredWithAttributesExtensions);

        DescriptorOnOptional = new(
            DescriptorOnOptionalContent,
            nameof(DescriptorOnOptional),
            DescriptorOnOptionalConstructor,
            DescriptorOnOptionalAttributedWithExtensions,
            DescriptorOnOptionalWithAgeExtensions,
            DescriptorOnOptionalWithNameExtensions);

        DescriptorOnRequired = new(
            DescriptorOnRequiredContent,
            nameof(DescriptorOnRequired),
            DescriptorOnRequiredConstructor,
            DescriptorOnRequiredAgedExtensions,
            DescriptorOnRequiredWithAttributesExtensions,
            DescriptorOnRequiredWithNameExtensions);

        Global = new(
            GlobalContent,
            nameof(Global),
            GlobalConstructor,
            GlobalWithAgeExtensions,
            GlobalWithAttributesExtensions,
            GlobalWithNameExtensions);

        InvalidDescriptor = new(
            InvalidDescriptorContent,
            nameof(InvalidDescriptor),
            InvalidDescriptorConstructor,
            InvalidDescriptorWithAgeExtensions,
            InvalidDescriptorWithAttributesExtensions,
            InvalidDescriptorWithNameExtensions);

        MultipleGenerics = new(
            MultipleGenericsContent,
            nameof(MultipleGenerics),
            MultipleGenericsConstructor,
            MultipleGenericsWithAgeExtensions,
            MultipleGenericsWithAttributesExtensions,
            MultipleGenericsWithNameExtensions);

        NestedInClass = new(
            NestedInClassContent,
            nameof(NestedInClass),
            NestedInClassConstructor,
            NestedInClassWithAgeExtensions,
            NestedInClassWithAttributesExtensions,
            NestedInClassWithNameExtensions);

        NestedInClassWithGenerics = new(
            NestedInClassWithGenericsContent,
            nameof(NestedInClassWithGenerics),
            NestedInClassWithGenericsConstructor,
            NestedInClassWithGenericsWithAgeExtensions,
            NestedInClassWithGenericsWithAttributesExtensions,
            NestedInClassWithGenericsWithNameExtensions);

        NestedInInterface = new(
            NestedInInterfaceContent,
            nameof(NestedInInterface),
            NestedInInterfaceConstructor,
            NestedInInterfaceWithAgeExtensions,
            NestedInInterfaceWithAttributesExtensions,
            NestedInInterfaceWithNameExtensions);

        NestedInInterfaceWithGenerics = new(
            NestedInInterfaceWithGenericsContent,
            nameof(NestedInInterfaceWithGenerics),
            NestedInInterfaceWithGenericsConstructor,
            NestedInInterfaceWithGenericsWithAgeExtensions,
            NestedInInterfaceWithGenericsWithAttributesExtensions,
            NestedInInterfaceWithGenericsWithNameExtensions);

        NestedInRecord = new(
            NestedInRecordContent,
            nameof(NestedInRecord),
            NestedInRecordConstructor,
            NestedInRecordWithAgeExtensions,
            NestedInRecordWithAttributesExtensions,
            NestedInRecordWithNameExtensions);

        NestedInRecordWithGenerics = new(
            NestedInRecordWithGenericsContent,
            nameof(NestedInRecordWithGenerics),
            NestedInRecordWithGenericsConstructor,
            NestedInRecordWithGenericsWithAgeExtensions,
            NestedInRecordWithGenericsWithAttributesExtensions,
            NestedInRecordWithGenericsWithNameExtensions);

        NestedInStruct = new(
            NestedInStructContent,
            nameof(NestedInStruct),
            NestedInStructConstructor,
            NestedInStructWithAgeExtensions,
            NestedInStructWithAttributesExtensions,
            NestedInStructWithNameExtensions);

        NestedInStructWithGenerics = new(
            NestedInStructWithGenericsContent,
            nameof(NestedInStructWithGenerics),
            NestedInStructWithGenericsConstructor,
            NestedInStructWithGenericsWithAgeExtensions,
            NestedInStructWithGenericsWithAttributesExtensions,
            NestedInStructWithGenericsWithNameExtensions);

        OneOfThreeIgnored = new(
            OneOfThreeIgnoredContent,
            nameof(OneOfThreeIgnored),
            OneOfThreeIgnoredConstructor,
            OneOfThreeIgnoredWithAgeExtensions,
            OneOfThreeIgnoredWithAttributesExtensions);

        SelfDescriptorOnIgnored = new(
            SelfDescriptorOnIgnoredContent,
            nameof(SelfDescriptorOnIgnored),
            SelfDescriptorOnIgnoredConstructor,
            SelfDescriptorOnIgnoredWithAgeExtensions,
            SelfDescriptorOnIgnoredWithAttributesExtensions);

        SelfDescriptorOnOptional = new(
            SelfDescriptorOnOptionalContent,
            nameof(SelfDescriptorOnOptional),
            SelfDescriptorOnOptionalConstructor,
            SelfDescriptorOnOptionalAttributesExtensions,
            SelfDescriptorOnOptionalWithAgeExtensions,
            SelfDescriptorOnOptionalWithNameExtensions);

        SelfDescriptorOnRequired = new(
            SelfDescriptorOnRequiredContent,
            nameof(SelfDescriptorOnRequired),
            SelfDescriptorOnRequiredConstructor,
            SelfDescriptorOnRequiredAgeExtensions,
            SelfDescriptorOnRequiredNameExtensions,
            SelfDescriptorOnRequiredWithAttributesExtensions);

        Simple = new(
            SimpleContent,
            nameof(Simple),
            SimpleConstructor,
            SimpleWithAgeExtensions,
            SimpleWithAttributesExtensions,
            SimpleWithNameExtensions);

        SimpleWithDefaultConstructor = new(
            SimpleWithDefaultConstructorContent,
            nameof(SimpleWithDefaultConstructor),
            SimpleWithDefaultConstructorWithAgeExtensions,
            SimpleWithDefaultConstructorWithAttributesExtensions,
            SimpleWithDefaultConstructorWithNameExtensions);

        SimpleWithoutPartial = new(
            SimpleWithoutPartialContent,
            nameof(SimpleWithoutPartial),
            SimpleWithoutPartialWithAgeExtensions,
            SimpleWithoutPartialWithAttributesExtensions,
            SimpleWithoutPartialWithNameExtensions);

        Single = new(SingleContent, nameof(Single), SingleConstructor, SingleWithAgeExtensions);

        SingleGeneric = new(
            SingleGenericContent,
            nameof(SingleGeneric),
            SingleGenericConstructor,
            SingleGenericWithAgeExtensions,
            SingleGenericWithAttributesExtensions,
            SingleGenericWithNameExtensions);

        SkipAutoInitializationOnProperty = new(
            SkipAutoInitializationOnPropertyContent,
            nameof(SkipAutoInitializationOnProperty),
            SkipAutoInitializationOnPropertyConstructor,
            SkipAutoInitializationOnPropertyWithAgeExtensions,
            SkipAutoInitializationOnPropertyWithDependencyExtensions);

        SkipAutoInitializationOnType = new(
            SkipAutoInitializationOnTypeContent,
            nameof(SkipAutoInitializationOnType),
            SkipAutoInitializationOnTypeConstructor,
            SkipAutoInitializationOnTypeWithAgeExtensions,
            SkipAutoInitializationOnTypeWithDependencyExtensions);

        TwoOfThreeIgnored = new(
            TwoOfThreeIgnoredContent,
            nameof(TwoOfThreeIgnored),
            TwoOfThreeIgnoredConstructor,
            TwoOfThreeIgnoredWithAttributesExtensions);

        Unannotated = new(UnannotatedContent, nameof(Unannotated));
        Unsupported = new(UnsupportedContent, nameof(Unsupported));
    }
}