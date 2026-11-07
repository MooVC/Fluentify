namespace Fluentify.Semantics.ISymbolExtensionsTests;

using Fluentify.Semantics;
using Microsoft.CodeAnalysis;

public sealed class WhenGetDefaultDescriptorIsCalled
{
    public static IEnumerable<(string Descriptor, string Parameter, Definition Definition)> GivenParametersThenExpectedDefaultDescriptorsIsReturnedData()
    {
        return
        [
            ("IsRetired", "IsRetired", Records.Instance.Boolean),
            ("WithSimple", "Simple", Records.Instance.CrossReferenced),
            ("WithAge", "Age",  Records.Instance.MultipleGenerics),
            ("WithName", "Name", Records.Instance.Simple),
            ("WithAttributes", "Attributes", Records.Instance.SingleGeneric),
            ("WithAge", "Age", Records.Instance.OneOfThreeIgnored),
            ("WithName", "Name", Records.Instance.TwoOfThreeIgnored),
            ("WithAttributes", "Attributes", Records.Instance.AllThreeIgnored),
            ("WithAge", "Age", Records.Instance.Unannotated),
        ];
    }

    public static IEnumerable<(string Descriptor, string Property, Definition Definition)> GivenPropertiesThenExpectedDefaultDescriptorsIsReturnedData()
    {
        return
        [
            ("IsRetired", "IsRetired", Classes.Instance.Boolean),
            ("WithSimple", "Simple", Classes.Instance.CrossReferenced),
            ("WithAge", "Age", Classes.Instance.MultipleGenerics),
            ("WithName", "Name", Classes.Instance.Simple),
            ("WithAttributes", "Attributes", Classes.Instance.SingleGeneric),
            ("WithAge", "Age", Classes.Instance.OneOfThreeIgnored),
            ("WithName", "Name", Classes.Instance.TwoOfThreeIgnored),
            ("WithAttributes", "Attributes", Classes.Instance.AllThreeIgnored),
            ("WithAge", "Age", Classes.Instance.Unannotated),
            ("IsRetired", "IsRetired", Records.Instance.Boolean),
            ("WithSimple", "Simple", Records.Instance.CrossReferenced),
            ("WithAge", "Age",  Records.Instance.MultipleGenerics),
            ("WithName", "Name", Records.Instance.Simple),
            ("WithAttributes", "Attributes", Records.Instance.SingleGeneric),
            ("WithAge", "Age", Records.Instance.OneOfThreeIgnored),
            ("WithName", "Name", Records.Instance.TwoOfThreeIgnored),
            ("WithAttributes", "Attributes", Records.Instance.AllThreeIgnored),
            ("WithAge", "Age", Records.Instance.Unannotated),
        ];
    }

    [Test]
    public void GivenAnUnrecognizedSymbolThenTheDefaultDescriptorIsReturned()
    {
        // Arrange
        const string name = "Age";
        const string expected = $"With{name}";

        ISymbol symbol = Substitute.For<ITypeSymbol>();

        _ = symbol.Name.Returns(name);

        // Act
        string? actual = symbol.GetDefaultDescriptor();

        // Assert
        actual.ShouldBe(expected);
    }

    [Test]
    [MethodDataSource(nameof(GivenParametersThenExpectedDefaultDescriptorsIsReturnedData))]
    public void GivenParametersThenExpectedDefaultDescriptorsIsReturned(string descriptor, string parameter, Definition definition)
    {
        // Arrange
        ISymbol symbol = definition.GetParameter(parameter);

        // Act
        string? actual = symbol.GetDefaultDescriptor();

        // Assert
        actual.ShouldBe(descriptor);
    }

    [Test]
    [MethodDataSource(nameof(GivenPropertiesThenExpectedDefaultDescriptorsIsReturnedData))]
    public void GivenPropertiesThenExpectedDefaultDescriptorsIsReturned(string descriptor, string property, Definition definition)
    {
        // Arrange
        ISymbol symbol = definition.GetProperty(property);

        // Act
        string? actual = symbol.GetDefaultDescriptor();

        // Assert
        actual.ShouldBe(descriptor);
    }
}