namespace Fluentify.SkipAutoInitializationAttributeAnalyzerTests;

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<SkipAutoInitializationAttributeAnalyzer, AutoInitiateWithAttributeGenerator>
{
    public WhenExecuted()
        : base(
            ReferenceAssemblies.Net.Net80,
            LanguageVersion.CSharp12,
            typeof(FluentifyAttributeGenerator),
            typeof(AutoInitiateWithAttributeGenerator),
            typeof(SkipAutoInitializationAttributeGenerator))
    {
    }

    [Fact]
    public async Task GivenAutoInitiateWithOnlyThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
using Fluentify;

[AutoInitiateWith(nameof(Default))]
public sealed class Sample
{
    public static Sample Default => new();
}
""";

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenBothAttributesThenDiagnosticIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedConflictingAttributesRule(new LinePosition(3, 1), "Sample"));

        TestCode = """
using Fluentify;

[SkipAutoInitialization]
[AutoInitiateWith(nameof(Default))]
public sealed class Sample
{
    public static Sample Default => new();
}
""";

        // Act & Assert
        await ActAndAssertAsync();
    }

    private static DiagnosticResult GetExpectedConflictingAttributesRule(LinePosition position, string type)
    {
        return new DiagnosticResult(SkipAutoInitializationAttributeAnalyzer.ConflictingAttributesRule)
            .WithArguments(type)
            .WithLocation(position);
    }
}
