namespace Fluentify.RecordGeneratorTests;

using Base = Fluentify.FluentifyGeneratorTests.WhenExecuted<Fluentify.RecordGenerator, Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax>;

public sealed class WhenExecuted
    : Base
{
    [Fact]
    public void GivenClassesThenNoSourceIsGenerated()
    {
        GivenCompilationThenTheExpectedSourceIsGenerated(Classes.Instance.Compilation);
    }

    [Fact]
    public void GivenRecordsTheExpectedSourceIsGenerated()
    {
        GivenCompilationThenTheExpectedSourceIsGenerated(
            Records.Instance.Compilation,
            (Class: "CrossReferenced", Descriptor: "WithDescription"),
            (Class: "CrossReferenced", Descriptor: "WithSimple"),
            (Class: "MultipleGenerics", Descriptor: "WithAge"),
            (Class: "MultipleGenerics", Descriptor: "WithName"),
            (Class: "MultipleGenerics", Descriptor: "WithAttributes"),
            (Class: "Simple", Descriptor: "WithAge"),
            (Class: "Simple", Descriptor: "WithName"),
            (Class: "Simple", Descriptor: "WithAttributes"),
            (Class: "SingleGeneric", Descriptor: "WithAge"),
            (Class: "SingleGeneric", Descriptor: "WithName"),
            (Class: "SingleGeneric", Descriptor: "WithAttributes"),
            (Class: "OneOfThreeIgnored", Descriptor: "WithAge"),
            (Class: "OneOfThreeIgnored", Descriptor: "WithAttributes"),
            (Class: "TwoOfThreeIgnored", Descriptor: "WithAttributes"),
            (Class: "DescriptorOnRequired", Descriptor: "Aged"),
            (Class: "DescriptorOnRequired", Descriptor: "WithName"),
            (Class: "DescriptorOnRequired", Descriptor: "WithAttributes"),
            (Class: "DescriptorOnOptional", Descriptor: "WithAge"),
            (Class: "DescriptorOnOptional", Descriptor: "WithName"),
            (Class: "DescriptorOnOptional", Descriptor: "AttributedWith"),
            (Class: "DescriptorOnIgnored", Descriptor: "WithAge"),
            (Class: "DescriptorOnIgnored", Descriptor: "WithAttributes"));
    }
}