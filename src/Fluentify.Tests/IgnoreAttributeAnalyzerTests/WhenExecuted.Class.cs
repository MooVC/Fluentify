namespace Fluentify.IgnoreAttributeAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Text;

public partial class WhenExecuted
{
    public sealed class Class
        : WhenExecuted
    {
        public Class()
            : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
        {
        }

        [Fact]
        public async Task GivenAnImutablePropertyWithoutIgnoreWhenFluentifyIsNotAppliedToTheClassThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                public class TestClass
                {
                    public string Property { get; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAMutablePropertyWithoutIgnoreWhenFluentifyIsNotAppliedToTheClassThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                public class TestClass
                {
                    public string Property { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAMutablePropertyWithoutIgnoreWhenFluentifyIsAppliedToTheClassThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    public string Property { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAMutablePropertyWithIgnoreWhenFluentifyIsAppliedToTheClassThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    [Ignore]
                    public string Property { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAnImmutablePropertyWithIgnoreWhenFluentifyIsNotAppliedToTheClassThenMissingFluentifyRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("Property", new LinePosition(4, 5)));

            TestCode = """
                using Fluentify;

                public class TestClassWithImmutablePropertyWithoutFluentify
                {
                    [Ignore]
                    public string Property { get; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAMutablePropertyWithIgnoreWhenFluentifyIsNotAppliedToTheClassThenMissingFluentifyRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("Property", new LinePosition(4, 5)));

            TestCode = """
                using Fluentify;

                public class TestClassWithMutablePropertyWithoutFluentify
                {
                    [Ignore]
                    public string Property { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAnImmutablePropertyWithIgnoreWhenFluentifyIsAppliedToTheClassThenRedundantUsageRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedRedundantUsageRule(new LinePosition(5, 4), "ImmutablePropertyOnClassWithFluentify"));

            TestCode = """
                using Fluentify;

                [Fluentify]
                public class TestClassWithImmutablePropertyWithFluentify
                {
                    [Ignore]
                    public string ImmutablePropertyOnClassWithFluentify { get; } = string.Empty;
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }
    }
}