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
            BooleanWithAgeExtensions,
            BooleanIsRetiredExtensions,
            BooleanWithNameExtensions);

        CrossReferenced = new(
            [CrossReferencedContent, SimpleContent],
            nameof(CrossReferenced),
            CrossReferencedConstructor,
            CrossReferencedWithDescriptionExtensions,
            CrossReferencedWithSimpleExtensions,
            SimpleConstructor,
            SimpleWithAgeExtensions,
            SimpleWithNameExtensions,
            SimpleWithAttributesExtensions);

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
            DescriptorOnOptionalWithAgeExtensions,
            DescriptorOnOptionalWithNameExtensions,
            DescriptorOnOptionalAttributedWithExtensions);

        DescriptorOnRequired = new(
            DescriptorOnRequiredContent,
            nameof(DescriptorOnRequired),
            DescriptorOnRequiredConstructor,
            DescriptorOnRequiredAgedExtensions,
            DescriptorOnRequiredWithNameExtensions,
            DescriptorOnRequiredWithAttributesExtensions);

        Global = new(
            GlobalContent,
            nameof(Global),
            GlobalConstructor,
            GlobalWithAgeExtensions,
            GlobalWithNameExtensions,
            GlobalWithAttributesExtensions);

        InvalidDescriptor = new(
            InvalidDescriptorContent,
            nameof(InvalidDescriptor),
            InvalidDescriptorConstructor,
            InvalidDescriptorWithAgeExtensions,
            InvalidDescriptorWithNameExtensions,
            InvalidDescriptorWithAttributesExtensions);

        MultipleGenerics = new(
            MultipleGenericsContent,
            nameof(MultipleGenerics),
            MultipleGenericsConstructor,
            MultipleGenericsWithAgeExtensions,
            MultipleGenericsWithNameExtensions,
            MultipleGenericsWithAttributesExtensions);

        NestedInClass = new(
            NestedInClassContent,
            nameof(NestedInClass),
            NestedInClassConstructor,
            NestedInClassWithAgeExtensions,
            NestedInClassWithNameExtensions,
            NestedInClassWithAttributesExtensions);

        NestedInInterface = new(
            NestedInInterfaceContent,
            nameof(NestedInInterface),
            NestedInInterfaceConstructor,
            NestedInInterfaceWithAgeExtensions,
            NestedInInterfaceWithNameExtensions,
            NestedInInterfaceWithAttributesExtensions);

        NestedInRecord = new(
            NestedInRecordContent,
            nameof(NestedInRecord),
            NestedInRecordConstructor,
            NestedInRecordWithAgeExtensions,
            NestedInRecordWithNameExtensions,
            NestedInRecordWithAttributesExtensions);

        NestedInStruct = new(
            NestedInStructContent,
            nameof(NestedInStruct),
            NestedInStructConstructor,
            NestedInStructWithAgeExtensions,
            NestedInStructWithNameExtensions,
            NestedInStructWithAttributesExtensions);

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
            SelfDescriptorOnOptionalWithAgeExtensions,
            SelfDescriptorOnOptionalWithNameExtensions,
            SelfDescriptorOnOptionalAttributesExtensions);

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
            SimpleWithNameExtensions,
            SimpleWithAttributesExtensions);

        SimpleWithDefaultConstructor = new(
            SimpleWithDefaultConstructorContent,
            nameof(SimpleWithDefaultConstructor),
            SimpleWithDefaultConstructorWithAgeExtensions,
            SimpleWithDefaultConstructorWithNameExtensions,
            SimpleWithDefaultConstructorWithAttributesExtensions);

        SimpleWithoutPartial = new(
            SimpleWithoutPartialContent,
            nameof(SimpleWithoutPartial),
            SimpleWithoutPartialWithAgeExtensions,
            SimpleWithoutPartialWithNameExtensions,
            SimpleWithoutPartialWithAttributesExtensions);

        Single = new(SingleContent, nameof(Single), SingleConstructor, SingleWithAgeExtensions);

        SingleGeneric = new(
            SingleGenericContent,
            nameof(SingleGeneric),
            SingleGenericConstructor,
            SingleGenericWithAgeExtensions,
            SingleGenericWithNameExtensions,
            SingleGenericWithAttributesExtensions);

        TwoOfThreeIgnored = new(
            TwoOfThreeIgnoredContent,
            nameof(TwoOfThreeIgnored),
            TwoOfThreeIgnoredConstructor,
            TwoOfThreeIgnoredWithAttributesExtensions);

        Unannotated = new(UnannotatedContent, nameof(Unannotated));
        Unsupported = new(UnsupportedContent, nameof(Unsupported));
    }
}