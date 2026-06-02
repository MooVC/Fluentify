namespace Fluentify.NullabilityAttributeAnalyzerTests;

using Fluentify.Snippets;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<NullabilityAttributeAnalyzer, FluentifyAttributeGenerator>
{
    public WhenExecuted()
        : base(Records.ReferenceAssemblies, LanguageVersion.CSharp9)
    {
    }

    [Fact]
    public async Task GivenAllowNullOnANonNullablePropertyWhenFluentifyIsAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            [Fluentify]
            public sealed class TestClass
            {
                [AllowNull]
                public string Name { get; set; } = string.Empty;
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAllowNullOnANullablePropertyWhenFluentifyIsAppliedThenRedundantAllowNullRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedRedundantAllowNullRule("Name", new LinePosition(7, 5)));

        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            [Fluentify]
            public sealed class TestClass
            {
                [AllowNull]
                public string? Name { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAllowNullOnANullablePropertyWhenFluentifyIsNotAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            public sealed class TestClass
            {
                [AllowNull]
                public string? Name { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAllowNullOnANullableRecordParameterWhenFluentifyIsAppliedThenRedundantAllowNullRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedRedundantAllowNullRule("Name", new LinePosition(5, 33)));

        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            [Fluentify]
            public record TestRecord([param: AllowNull] string? Name);
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenDisallowNullOnANonNullablePropertyWhenFluentifyIsAppliedThenRedundantDisallowNullRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedRedundantDisallowNullRule("Name", new LinePosition(7, 5)));

        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            [Fluentify]
            public sealed class TestClass
            {
                [DisallowNull]
                public string Name { get; set; } = string.Empty;
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenDisallowNullOnANonNullableRecordParameterWhenFluentifyIsAppliedThenRedundantDisallowNullRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedRedundantDisallowNullRule("Name", new LinePosition(5, 33)));

        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            [Fluentify]
            public record TestRecord([param: DisallowNull] string Name);
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenDisallowNullOnANullablePropertyWhenFluentifyIsAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            [Fluentify]
            public sealed class TestClass
            {
                [DisallowNull]
                public string? Name { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenMaybeNullOnANonNullablePropertyWhenFluentifyIsAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            [Fluentify]
            public sealed class TestClass
            {
                [MaybeNull]
                public string Name { get; set; } = string.Empty;
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenMaybeNullOnANullablePropertyWhenFluentifyIsAppliedThenRedundantMaybeNullRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedRedundantMaybeNullRule("Name", new LinePosition(7, 5)));

        TestCode = """
            #nullable enable
            using System.Diagnostics.CodeAnalysis;
            using Fluentify;

            [Fluentify]
            public sealed class TestClass
            {
                [MaybeNull]
                public string? Name { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    private static DiagnosticResult GetExpectedRedundantAllowNullRule(string property, LinePosition position)
    {
        return GetExpected(property, position, NullabilityAttributeAnalyzer.RedundantAllowNullRule);
    }

    private static DiagnosticResult GetExpectedRedundantDisallowNullRule(string property, LinePosition position)
    {
        return GetExpected(property, position, NullabilityAttributeAnalyzer.RedundantDisallowNullRule);
    }

    private static DiagnosticResult GetExpectedRedundantMaybeNullRule(string property, LinePosition position)
    {
        return GetExpected(property, position, NullabilityAttributeAnalyzer.RedundantMaybeNullRule);
    }

    private static DiagnosticResult GetExpected(string property, LinePosition position, DiagnosticDescriptor rule)
    {
        return new DiagnosticResult(rule)
            .WithArguments(property)
            .WithLocation(position);
    }
}