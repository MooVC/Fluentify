namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using Fluentify.Semantics;
using Microsoft.CodeAnalysis;

public sealed class WhenGetDescriptorIsCalled
{
    public static readonly TheoryData<string?, string, Definition> GivenPropertyWithDescriptorThenTheDescriptorIsReturnedData = new()
    {
        { "Aged", "Age", Classes.Instance.DescriptorOnRequired },
        { "AttributedWith", "Attributes", Classes.Instance.DescriptorOnOptional },
        { "Named", "Name", Classes.Instance.DescriptorOnIgnored },
        { default, "Age", Classes.Instance.InvalidDescriptor },
        { "Age", "Age", Classes.Instance.SelfDescriptorOnRequired },
        { "Attributes", "Attributes", Classes.Instance.SelfDescriptorOnOptional },
        { "Name", "Name", Classes.Instance.SelfDescriptorOnIgnored },
        { "Aged", "Age", Records.Instance.DescriptorOnRequired },
        { "AttributedWith", "Attributes", Records.Instance.DescriptorOnOptional },
        { "Named", "Name", Records.Instance.DescriptorOnIgnored },
        { default, "Age", Records.Instance.InvalidDescriptor },
        { "Age", "Age", Records.Instance.SelfDescriptorOnRequired },
        { "Attributes", "Attributes", Records.Instance.SelfDescriptorOnOptional },
        { "Name", "Name", Records.Instance.SelfDescriptorOnIgnored },
    };

    public static readonly TheoryData<Definition> GivenPropertiesWithoutDescriptorsThenNoDescriptorsAreReturnedData = new()
    {
        { Classes.Instance.Boolean },
        { Classes.Instance.CrossReferenced },
        { Classes.Instance.MultipleGenerics },
        { Classes.Instance.Simple },
        { Classes.Instance.SingleGeneric },
        { Classes.Instance.OneOfThreeIgnored },
        { Classes.Instance.TwoOfThreeIgnored },
        { Classes.Instance.AllThreeIgnored },
        { Classes.Instance.Unannotated },
        { Records.Instance.Boolean },
        { Records.Instance.CrossReferenced },
        { Records.Instance.MultipleGenerics },
        { Records.Instance.Simple },
        { Records.Instance.SingleGeneric },
        { Records.Instance.OneOfThreeIgnored },
        { Records.Instance.TwoOfThreeIgnored },
        { Records.Instance.AllThreeIgnored },
        { Records.Instance.Unannotated },
    };

    [Theory]
    [MemberData(nameof(GivenPropertyWithDescriptorThenTheDescriptorIsReturnedData))]
    public void GivenPropertyWithDescriptorThenTheDescriptorIsReturned(string? descriptor, string property, Definition definition)
    {
        // Arrange
        IPropertySymbol symbol = definition.GetProperty(property);

        // Act
        string? actual = symbol.GetDescriptor();

        // Assert
        actual.ShouldBe(descriptor);
    }

    [Theory]
    [MemberData(nameof(GivenPropertiesWithoutDescriptorsThenNoDescriptorsAreReturnedData))]
    public void GivenPropertiesWithoutDescriptorsThenNoDescriptorsAreReturned(Definition definition)
    {
        // Arrange
        IEnumerable<IPropertySymbol> properties = definition
            .Symbol
            .GetMembers()
            .OfType<IPropertySymbol>();

        foreach (IPropertySymbol property in properties)
        {
            // Act
            string? descriptor = property.GetDescriptor();

            // Assert
            descriptor.ShouldBeNull();
        }
    }
}