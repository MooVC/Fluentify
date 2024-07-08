namespace Fluentify.IgnoreAttributeAnalyzerTests;

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<IgnoreAttributeAnalyzer, IgnoreAttributeGenerator>
{
    [Fact]
    public async Task GivenAnImutablePropertyWithoutIgnoreWhenFluentifyIsNotAppliedThenNoDiagnosticIsRaised()
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
    public async Task GivenAMutablePropertyWithoutIgnoreWhenFluentifyIsNotAppliedThenNoDiagnosticIsRaised()
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
    public async Task GivenAMutablePropertyWithoutIgnoreWhenFluentifyIsAppliedThenNoDiagnosticIsRaised()
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
    public async Task GivenAnImmutablePropertyWithIgnoreWhenFluentifyIsNotAppliedThenRedundantUsageRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedRedundantUsageRule(new LinePosition(5, 18), "ImmutablePropertyOnClassWithoutFluentify"));

        TestCode = """
            using Fluentify;

            public class TestClass
            {
                [Ignore]
                public string ImmutablePropertyOnClassWithoutFluentify { get; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAMutablePropertyWithIgnoreWhenFluentifyIsNotAppliedThenRedundantUsageRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedRedundantUsageRule(new LinePosition(5, 18), "MutablePropertyOnClassWithoutFluentify"));

        TestCode = """
            using Fluentify;

            public class TestClass
            {
                [Ignore]
                public string MutablePropertyOnClassWithoutFluentify { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAnImmutablePropertyWithIgnoreWhenFluentifyIsAppliedThenRedundantUsageRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedRedundantUsageRule(new LinePosition(6, 18), "ImmutablePropertyOnClassWithFluentify"));

        TestCode = """
            using Fluentify;

            [Fluentify]
            public class TestClass
            {
                [Ignore]
                public string ImmutablePropertyOnClassWithFluentify => string.Empty;
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    private static DiagnosticResult GetExpectedRedundantUsageRule(LinePosition position, string property)
    {
        return new DiagnosticResult(IgnoreAttributeAnalyzer.RedundantUsageRule)
            .WithArguments(property)
            .WithLocation(position);
    }
}