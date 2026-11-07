namespace Fluentify.Syntax.TypeDeclarationSyntaxExtensionsTests;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed class WhenIsPartialIsCalled
{
    [Test]
    public void GivenNonPartialClassThenReturnsFalse()
    {
        // Arrange
        const string Source = """
            public class TestClass
            {
            }
            """;

        TypeDeclarationSyntax syntax = CreateTypeDeclarationSyntax(Source);

        // Act
        bool result = syntax.IsPartial();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenNonPartialInterfaceThenReturnsFalse()
    {
        // Arrange
        const string Source = """
            public interface ITestInterface
            {
            }
            """;

        TypeDeclarationSyntax syntax = CreateTypeDeclarationSyntax(Source);

        // Act
        bool result = syntax.IsPartial();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenNonPartialStructThenReturnsFalse()
    {
        // Arrange
        const string Source = """
            public struct TestStruct
            {
            }
            """;

        TypeDeclarationSyntax syntax = CreateTypeDeclarationSyntax(Source);

        // Act
        bool result = syntax.IsPartial();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenPartialClassThenReturnsTrue()
    {
        // Arrange
        const string Source = """
            public partial class TestClass
            {
            }
            """;

        TypeDeclarationSyntax syntax = CreateTypeDeclarationSyntax(Source);

        // Act
        bool result = syntax.IsPartial();

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void GivenPartialInterfaceThenReturnsTrue()
    {
        // Arrange
        const string Source = """
            public partial interface ITestInterface
            {
            }
            """;

        TypeDeclarationSyntax syntax = CreateTypeDeclarationSyntax(Source);

        // Act
        bool result = syntax.IsPartial();

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void GivenPartialStructThenReturnsTrue()
    {
        // Arrange
        const string Source = """
            public partial struct TestStruct
            {
            }
            """;

        TypeDeclarationSyntax syntax = CreateTypeDeclarationSyntax(Source);

        // Act
        bool result = syntax.IsPartial();

        // Assert
        result.ShouldBeTrue();
    }

    private static TypeDeclarationSyntax CreateTypeDeclarationSyntax(string source)
    {
        return CSharpSyntaxTree.ParseText(source)
            .GetRoot()
            .DescendantNodes()
            .OfType<TypeDeclarationSyntax>()
            .Single();
    }
}