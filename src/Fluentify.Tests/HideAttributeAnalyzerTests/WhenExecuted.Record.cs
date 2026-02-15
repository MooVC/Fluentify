namespace Fluentify.HideAttributeAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Text;

public partial class WhenExecuted
{
    public sealed class Record
        : WhenExecuted
    {
        public Record()
            : base(
                Records.ReferenceAssemblies,
                Records.LanguageVersion,
                typeof(FluentifyAttributeGenerator),
                typeof(HideAttributeGenerator),
                typeof(IgnoreAttributeGenerator))
        {
        }

        [Fact]
        public async Task GivenAPropertyWithoutHideWhenFluentifyIsNotAppliedToTheRecordThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                public record TestRecord(string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAPropertyWithHideWhenFluentifyIsAppliedToTheRecordThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                using Fluentify;

                [Fluentify]
                public record TestRecord([Hide] string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAPropertyWithHideWhenFluentifyIsNotAppliedToTheRecordThenMissingFluentifyRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("Property", new LinePosition(4, 5)));

            TestCode = """
                using Fluentify;

                public record TestRecordWithPropertyWithoutFluentify
                {
                    [Hide]
                    public string Property { get; init; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAPropertyWithHideAndIgnoreWhenFluentifyIsAppliedToTheRecordThenConflictingAttributesRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedConflictingAttributesRule(new LinePosition(5, 5), "Property"));

            TestCode = """
                using Fluentify;

                [Fluentify]
                public record TestRecordWithPropertyWithHideAndIgnore
                {
                    [Hide, Ignore]
                    public string Property { get; init; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }
    }
}