namespace Fluentify.Semantics.IPropertySymbolExtensionsTests;

using Fluentify;
using Microsoft.CodeAnalysis;

public sealed class WhenGetDescriptorIsCalled
{
    public static readonly TheoryData<string, string, Type> GivenPropertyWithDescriptorThenTheDescriptorIsReturnedData = new()
    {
        { "Aged", "Age", Classes.Instance.DescriptorOnRequired },
        { "AttributedWith", "Attributes", Classes.Instance.DescriptorOnOptional },
        { "Named", "Name", Classes.Instance.DescriptorOnIgnored },
        { "Aged", "Age", Records.Instance.DescriptorOnRequired },
        { "AttributedWith", "Attributes", Records.Instance.DescriptorOnOptional },
        { "Named", "Name", Records.Instance.DescriptorOnIgnored },
    };

    public static readonly TheoryData<Type> GivenPropertiesWithoutDescriptorsThenNoDescriptorsAreReturnedData = new()
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
    public void GivenPropertyWithDescriptorThenTheDescriptorIsReturned(string descriptor, string property, Type type)
    {
        // Arrange
        IPropertySymbol symbol = type.GetProperty(property);

        // Act
        string? actual = symbol.GetDescriptor();

        // Assert
        _ = actual.Should().Be(descriptor);
    }

    [Theory]
    [MemberData(nameof(GivenPropertiesWithoutDescriptorsThenNoDescriptorsAreReturnedData))]
    public void GivenPropertiesWithoutDescriptorsThenNoDescriptorsAreReturned(Type type)
    {
        // Arrange
        IEnumerable<IPropertySymbol> properties = type
            .Symbol
            .GetMembers()
            .OfType<IPropertySymbol>();

        foreach (IPropertySymbol property in properties)
        {
            // Act
            string? descriptor = property.GetDescriptor();

            // Assert
            _ = descriptor.Should().BeNull();
        }
    }
}