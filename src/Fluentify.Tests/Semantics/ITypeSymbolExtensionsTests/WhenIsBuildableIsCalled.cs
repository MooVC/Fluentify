namespace Fluentify.Semantics.ITypeSymbolExtensionsTests;

using Microsoft.CodeAnalysis;

public sealed class WhenIsBuildableIsCalled
{
    public static readonly TheoryData<Compilation, string, Type> GivenABuildablePropertyThenTrueIsReturnedData = new()
    {
        { Classes.Instance.Compilation, "Simple", Classes.Instance.CrossReferenced },
        { Classes.Instance.Compilation, "Name", Classes.Instance.MultipleGenerics },
        { Records.Instance.Compilation, "Simple", Records.Instance.CrossReferenced },
        { Records.Instance.Compilation, "Name", Records.Instance.MultipleGenerics },
    };

    public static readonly TheoryData<Compilation, string, Type> GivenANonBuildablePropertyThenFalseIsReturnedData = new()
    {
        { Classes.Instance.Compilation, "Age", Classes.Instance.Boolean },
        { Classes.Instance.Compilation, "IsRetired", Classes.Instance.Boolean },
        { Classes.Instance.Compilation, "Name", Classes.Instance.Boolean },
        { Classes.Instance.Compilation, "Age", Classes.Instance.MultipleGenerics },
        { Classes.Instance.Compilation, "Attributes", Classes.Instance.MultipleGenerics },
        { Classes.Instance.Compilation, "Age", Classes.Instance.Simple },
        { Classes.Instance.Compilation, "Attributes", Classes.Instance.Simple },
        { Classes.Instance.Compilation, "Name", Classes.Instance.Simple },
        { Classes.Instance.Compilation, "Age", Classes.Instance.SingleGeneric },
        { Classes.Instance.Compilation, "Attributes", Classes.Instance.SingleGeneric },
        { Classes.Instance.Compilation, "Name", Classes.Instance.SingleGeneric },
        { Records.Instance.Compilation, "Age", Records.Instance.Boolean },
        { Records.Instance.Compilation, "IsRetired", Records.Instance.Boolean },
        { Records.Instance.Compilation, "Name", Records.Instance.Boolean },
        { Records.Instance.Compilation, "Age", Records.Instance.MultipleGenerics },
        { Records.Instance.Compilation, "Attributes", Records.Instance.MultipleGenerics },
        { Records.Instance.Compilation, "Age", Records.Instance.Simple },
        { Records.Instance.Compilation, "Attributes", Records.Instance.Simple },
        { Records.Instance.Compilation, "Name", Records.Instance.Simple },
        { Records.Instance.Compilation, "Age", Records.Instance.SingleGeneric },
        { Records.Instance.Compilation, "Attributes", Records.Instance.SingleGeneric },
        { Records.Instance.Compilation, "Name", Records.Instance.SingleGeneric },
    };

    [Theory]
    [MemberData(nameof(GivenABuildablePropertyThenTrueIsReturnedData))]
    public void GivenABuildablePropertyThenTrueIsReturned(Compilation compilation, string property, Type type)
    {
        // Arrange
        IPropertySymbol symbol = type.GetProperty(property);

        // Act
        bool isBuildable = symbol.Type.IsBuildable(compilation, CancellationToken.None);

        // Assert
        _ = isBuildable.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(GivenANonBuildablePropertyThenFalseIsReturnedData))]
    public void GivenANonBuildablePropertyThenFalseIsReturned(Compilation compilation, string property, Type type)
    {
        // Arrange
        IPropertySymbol symbol = type.GetProperty(property);

        // Act
        bool isBuildable = symbol.Type.IsBuildable(compilation, CancellationToken.None);

        // Assert
        _ = isBuildable.Should().BeFalse();
    }
}