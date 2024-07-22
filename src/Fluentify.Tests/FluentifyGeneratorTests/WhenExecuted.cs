namespace Fluentify.FluentifyGeneratorTests;

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public abstract class WhenExecuted<TGenerator, TSyntax>
    where TGenerator : FluentifyGenerator<TSyntax>, new()
    where TSyntax : TypeDeclarationSyntax
{
    protected void GivenCompilationThenTheExpectedSourceIsGenerated(Compilation compilation, params (string Class, string Descriptor)[] hints)
    {
        // Arrange
        IIncrementalGenerator generator = new TGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        string name = typeof(TGenerator).Name;
        string prefix = $"Fluentify\\Fluentify.{name}";
        const string @namespace = "Fluentify.Tests.Compilation";
        IEnumerable<string> expected = hints.Select(hint => $"{prefix}\\{@namespace}.{hint.Class}Extensions.{hint.Descriptor}.g.cs");

        // Act
        _ = driver.RunGeneratorsAndUpdateCompilation(
            compilation,
            out Compilation? output,
            out ImmutableArray<Diagnostic> diagnostics);

        // Assert
        string[] sources = output.SyntaxTrees
            .Select(tree => tree.FilePath)
            .ToArray();

        _ = diagnostics.Should().BeEmpty();

        if (hints.Length == 0)
        {
            _ = sources.Should().ContainSingle();
        }
        else
        {
            _ = sources.Should().Contain(expected);
        }
    }
}