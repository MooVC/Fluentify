namespace Fluentify.AutoInitiateWithAttributeAnalyzerTests;

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<AutoInitiateWithAttributeAnalyzer, AutoInitiateWithAttributeGenerator>
{
    public WhenExecuted()
        : base(ReferenceAssemblies.Net.Net80, LanguageVersion.CSharp12)
    {
    }

    [Fact]
    public async Task GivenStaticPropertyReturningTypeThenNoDiagnosticIsRaised()
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
    public async Task GivenMissingMemberThenDiagnosticIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedInvalidTargetRule("Missing", new LinePosition(2, 1), "Sample"));

        TestCode = """
using Fluentify;

[AutoInitiateWith(nameof(Missing))]
public sealed class Sample
{
}
""";

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenMethodWithParametersThenDiagnosticIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedInvalidTargetRule("Create", new LinePosition(2, 1), "Sample"));

        TestCode = """
using Fluentify;

[AutoInitiateWith(nameof(Create))]
public sealed class Sample
{
    public static Sample Create(int value) => new();
}
""";

        // Act & Assert
        await ActAndAssertAsync();
    }

    private static DiagnosticResult GetExpectedInvalidTargetRule(string member, LinePosition position, string type)
    {
        return new DiagnosticResult(AutoInitiateWithAttributeAnalyzer.InvalidTargetRule)
            .WithArguments(member, type)
            .WithLocation(position);
    }
}
