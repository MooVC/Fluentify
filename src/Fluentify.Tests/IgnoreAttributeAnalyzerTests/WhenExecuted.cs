namespace Fluentify.IgnoreAttributeAnalyzerTests;

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<IgnoreAttributeAnalyzer, IgnoreAttributeGenerator>
{
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
    public async Task GivenAMutablePropertyWithoutIgnoreWhenFluentifyIsNotAppliedToTheRecordThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = "public record TestRecord(string Property);";

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
    public async Task GivenAnImmutablePropertyWithIgnoreWhenFluentifyIsNotAppliedToTheClassThenMissingFluentifyRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("TestClassWithImmutablePropertyWithoutFluentify", new LinePosition(4, 5)));

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
    public async Task GivenAnImmutablePropertyWithIgnoreWhenFluentifyIsNotAppliedToTheRecordThenMissingFluentifyRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("TestRecordWithImmutablePropertyWithoutFluentify", new LinePosition(4, 5)));

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
    public async Task GivenAMutablePropertyWithIgnoreWhenFluentifyIsNotAppliedToTheClassThenMissingFluentifyRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("TestClassWithMutablePropertyWithoutFluentify", new LinePosition(4, 5)));

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
    public async Task GivenAMutablePropertyWithIgnoreWhenFluentifyIsNotAppliedToTheRecordThenMissingFluentifyRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedMissingFluentifyRule("TestRecordWithMutablePropertyWithoutFluentify", new LinePosition(2, 61)));

        TestCode = """
            using Fluentify;

            public record TestRecordWithMutablePropertyWithoutFluentify([Ignore] string Property);
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

    private static DiagnosticResult GetExpectedMissingFluentifyRule(string @class, LinePosition position)
    {
        return GetExpected(@class, position, IgnoreAttributeAnalyzer.MissingFluentifyRule);
    }

    private static DiagnosticResult GetExpectedRedundantUsageRule(LinePosition position, string property)
    {
        return GetExpected(property, position, IgnoreAttributeAnalyzer.RedundantUsageRule);
    }

    private static DiagnosticResult GetExpected(string argument, LinePosition position, DiagnosticDescriptor rule)
    {
        return new DiagnosticResult(rule)
            .WithArguments(argument)
            .WithLocation(position);
    }
}