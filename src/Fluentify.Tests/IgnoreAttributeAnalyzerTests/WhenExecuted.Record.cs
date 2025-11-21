namespace Fluentify.IgnoreAttributeAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Text;

public partial class WhenExecuted
{
    public sealed class Record
        : WhenExecuted
    {
        public Record()
            : base(Records.ReferenceAssemblies, Records.LanguageVersion)
        {
        }

        [Fact]
        public async Task GivenAnImutablePropertyWithoutIgnoreWhenFluentifyIsNotAppliedToTheRecordThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                public record TestRecord
                {
                    public string Property { get; } = string.Empty;
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAMutablePropertyWithoutIgnoreWhenFluentifyIsNotAppliedToTheRecordThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = "public record TestRecord(string Property);";

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAMutablePropertyWithoutIgnoreWhenFluentifyIsAppliedToTheRecordThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                using Fluentify;

                [Fluentify]
                public record TestRecord(string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAMutablePropertyWithIgnoreWhenFluentifyIsAppliedToTheRecordThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                using Fluentify;

                [Fluentify]
                public record TestRecord([Ignore] string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAnImmutablePropertyWithIgnoreWhenFluentifyIsNotAppliedToTheRecordThenMissingFluentifyRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("Property", new LinePosition(4, 5)));

            TestCode = """
                using Fluentify;

                public record TestRecordWithImmutablePropertyWithoutFluentify
                {
                    [Ignore]
                    public string Property { get; } = string.Empty;
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAMutablePropertyWithIgnoreWhenFluentifyIsNotAppliedToTheRecordThenMissingFluentifyRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("Property", new LinePosition(2, 61)));

            TestCode = """
                using Fluentify;

                public record TestRecordWithMutablePropertyWithoutFluentify([Ignore] string Property);
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAnImmutablePropertyWithIgnoreWhenFluentifyIsAppliedToTheRecordThenRedundantUsageRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedRedundantUsageRule(new LinePosition(5, 4), "ImmutablePropertyOnRecordWithFluentify"));

            TestCode = """
                using Fluentify;

                [Fluentify]
                public record TestRecordWithImmutablePropertyWithFluentify
                {
                    [Ignore]
                    public string ImmutablePropertyOnRecordWithFluentify { get; } = string.Empty;
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }
    }
}