namespace Fluentify.HideAttributeAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Text;

public partial class WhenExecuted
{
    public sealed class Class
        : WhenExecuted
    {
        public Class()
            : base(
                Classes.ReferenceAssemblies,
                Classes.LanguageVersion,
                typeof(FluentifyAttributeGenerator),
                typeof(HideAttributeGenerator),
                typeof(IgnoreAttributeGenerator))
        {
        }

        [Fact]
        public async Task GivenAPropertyWithoutHideWhenFluentifyIsNotAppliedToTheClassThenNoDiagnosticIsRaised()
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
        public async Task GivenAPropertyWithHideWhenFluentifyIsAppliedToTheClassThenNoDiagnosticIsRaised()
        {
            // Arrange
            TestCode = """
                using Fluentify;

                [Fluentify]
                public class TestClass
                {
                    [Hide]
                    public string Property { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAPropertyWithHideWhenFluentifyIsNotAppliedToTheClassThenMissingFluentifyRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("Property", new LinePosition(4, 5)));

            TestCode = """
                using Fluentify;

                public class TestClassWithPropertyWithoutFluentify
                {
                    [Hide]
                    public string Property { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }

        [Fact]
        public async Task GivenAPropertyWithHideAndIgnoreWhenFluentifyIsAppliedToTheClassThenConflictingAttributesRuleIsRaised()
        {
            // Arrange
            ExpectedDiagnostics.Add(GetExpectedConflictingAttributesRule(new LinePosition(5, 5), "Property"));

            TestCode = """
                using Fluentify;

                [Fluentify]
                public class TestClassWithPropertyWithHideAndIgnore
                {
                    [Hide, Ignore]
                    public string Property { get; set; }
                }
                """;

            // Act & Assert
            await ActAndAssertAsync();
        }
    }
}