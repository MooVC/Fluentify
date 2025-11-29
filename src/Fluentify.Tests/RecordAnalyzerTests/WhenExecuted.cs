namespace Fluentify.RecordAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<RecordAnalyzer, IgnoreAttributeGenerator>
{
    public WhenExecuted()
        : base(Records.ReferenceAssemblies, Records.LanguageVersion)
    {
    }

    [Fact]
    public async Task GivenARecordWithoutFluentifyThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = "public record UnannotatedRecord(string Name);";

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAPartialRecordWithFluentifyThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            using Fluentify;

            [Fluentify]
            public partial record AnnotatedPartialRecord(string Name);
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenARecordWithAccessibleDefaultConstructorWhenFluentifyIsAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            using Fluentify;

            [Fluentify]
            public record AnnotatedRecordWithDefault
            {
                public AnnotatedRecordWithDefault()
                {
                }

                public string Name { get; init; } = string.Empty;
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenARecordWithoutAccessibleDefaultConstructorWhenFluentifyIsAppliedThenPartialRecordRequiredRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedPartialRecordRequiredRule(new LinePosition(3, 14), "AnnotatedRecord"));

        TestCode = """
            using Fluentify;

            [Fluentify]
            public record AnnotatedRecord(string Name);
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    private static DiagnosticResult GetExpectedPartialRecordRequiredRule(LinePosition position, string record)
    {
        return new DiagnosticResult(RecordAnalyzer.PartialRecordRequiredRule)
            .WithArguments(record)
            .WithLocation(position);
    }
}