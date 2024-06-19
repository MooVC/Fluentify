namespace Fluentify.ClassGeneratorTests;

using Base = Fluentify.FluentifyGeneratorTests.WhenExecuted<Fluentify.ClassGenerator, Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>;

public sealed class WhenExecuted
    : Base
{
    [Fact]
    public void GivenClassesTheExpectedSourceIsGenerated()
    {
        GivenCompilationThenTheExpectedSourceIsGenerated(
            Classes.Instance.Compilation,
            (Class: "Boolean", Descriptor: "WithAge"),
            (Class: "Boolean", Descriptor: "IsRetired"),
            (Class: "Boolean", Descriptor: "WithName"),
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

    [Fact]
    public void GivenRecordsThenNoSourceIsGenerated()
    {
        GivenCompilationThenTheExpectedSourceIsGenerated(Records.Instance.Compilation);
    }
}