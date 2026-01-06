namespace Fluentify.SkipAutoInitializationAttributeAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<SkipAutoInitializationAttributeAnalyzer, AutoInitiateWithAttributeGenerator>
{
    public WhenExecuted()
        : base(
            Classes.ReferenceAssemblies,
            Classes.LanguageVersion,
            typeof(AutoInitiateWithAttributeGenerator),
            typeof(FluentifyAttributeGenerator),
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
                public static Sample Default => new Sample();
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
                public static Sample Default => new Sample();
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