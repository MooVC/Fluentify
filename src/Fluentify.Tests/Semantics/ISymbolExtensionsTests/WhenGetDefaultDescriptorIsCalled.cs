namespace Fluentify.Semantics.ISymbolExtensionsTests;

using Fluentify.Semantics;
using Microsoft.CodeAnalysis;

public sealed class WhenGetDefaultDescriptorIsCalled
{
    public static readonly TheoryData<string, string, Definition> GivenParametersThenExpectedDefaultDescriptorsIsReturnedData = new()
    {
        { "IsRetired", "IsRetired", Records.Instance.Boolean },
        { "WithSimple", "Simple", Records.Instance.CrossReferenced },
        { "WithAge", "Age",  Records.Instance.MultipleGenerics },
        { "WithName", "Name", Records.Instance.Simple },
        { "WithAttributes", "Attributes", Records.Instance.SingleGeneric },
        { "WithAge", "Age", Records.Instance.OneOfThreeIgnored },
        { "WithName", "Name", Records.Instance.TwoOfThreeIgnored },
        { "WithAttributes", "Attributes", Records.Instance.AllThreeIgnored },
        { "WithAge", "Age", Records.Instance.Unannotated },
    };

    public static readonly TheoryData<string, string, Definition> GivenPropertiesThenExpectedDefaultDescriptorsIsReturnedData = new()
    {
        { "IsRetired", "IsRetired", Classes.Instance.Boolean },
        { "WithSimple", "Simple", Classes.Instance.CrossReferenced },
        { "WithAge", "Age", Classes.Instance.MultipleGenerics },
        { "WithName", "Name", Classes.Instance.Simple },
        { "WithAttributes", "Attributes", Classes.Instance.SingleGeneric },
        { "WithAge", "Age", Classes.Instance.OneOfThreeIgnored },
        { "WithName", "Name", Classes.Instance.TwoOfThreeIgnored },
        { "WithAttributes", "Attributes", Classes.Instance.AllThreeIgnored },
        { "WithAge", "Age", Classes.Instance.Unannotated },
        { "IsRetired", "IsRetired", Records.Instance.Boolean },
        { "WithSimple", "Simple", Records.Instance.CrossReferenced },
        { "WithAge", "Age",  Records.Instance.MultipleGenerics },
        { "WithName", "Name", Records.Instance.Simple },
        { "WithAttributes", "Attributes", Records.Instance.SingleGeneric },
        { "WithAge", "Age", Records.Instance.OneOfThreeIgnored },
        { "WithName", "Name", Records.Instance.TwoOfThreeIgnored },
        { "WithAttributes", "Attributes", Records.Instance.AllThreeIgnored },
        { "WithAge", "Age", Records.Instance.Unannotated },
    };

    [Fact]
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
        _ = actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(GivenParametersThenExpectedDefaultDescriptorsIsReturnedData))]
    public void GivenParametersThenExpectedDefaultDescriptorsIsReturned(string descriptor, string parameter, Definition definition)
    {
        // Arrange
        ISymbol symbol = definition.GetParameter(parameter);

        // Act
        string? actual = symbol.GetDefaultDescriptor();

        // Assert
        _ = actual.Should().Be(descriptor);
    }

    [Theory]
    [MemberData(nameof(GivenPropertiesThenExpectedDefaultDescriptorsIsReturnedData))]
    public void GivenPropertiesThenExpectedDefaultDescriptorsIsReturned(string descriptor, string property, Definition definition)
    {
        // Arrange
        ISymbol symbol = definition.GetProperty(property);

        // Act
        string? actual = symbol.GetDefaultDescriptor();

        // Assert
        _ = actual.Should().Be(descriptor);
    }
}