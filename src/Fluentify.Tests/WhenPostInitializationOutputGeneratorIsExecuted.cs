namespace Fluentify;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public abstract class WhenPostInitializationOutputGeneratorIsExecuted<T>
    where T : IIncrementalGenerator, new()
{
    private readonly string expected;

    protected WhenPostInitializationOutputGeneratorIsExecuted(string expected)
    {
        this.expected = expected;
    }

    [Fact]
    public void GivenAnAssemblyThenAttributeIsGenerated()
    {
        // Arrange
        IIncrementalGenerator generator = new T();
        var driver = CSharpGeneratorDriver.Create(generator);

        // Act
        _ = driver.RunGeneratorsAndUpdateCompilation(
            CSharpCompilation.Create("test"),
            out Compilation? output,
            out ImmutableArray<Diagnostic> diagnostics);

        // Assert
        string[] sources = output.SyntaxTrees
            .Select(tree => tree.GetText().ToString())
            .ToArray();

        _ = sources.Should().Contain(expected);
        _ = diagnostics.Should().BeEmpty();
    }
}