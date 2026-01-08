namespace Fluentify.AutoInitiateWithAttributeAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<AutoInitiateWithAttributeAnalyzer, AutoInitiateWithAttributeGenerator>
{
    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
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
                public static Sample Default => new Sample();
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

            public sealed class Missing
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
                public static Sample Create(int value) => new Sample();
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenPropertyWithMissingMemberThenDiagnosticIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedInvalidTargetRule("Missing", new LinePosition(4, 5), "Sample"));

        TestCode = """
            using Fluentify;

            public sealed class Sample
            {
                [AutoInitiateWith("Missing")]
                public Sample Property { get; } = new Sample();

                public static Sample Create() => new Sample();
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenParameterWithValidMemberThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            using Fluentify;

            public sealed class Sample
            {
                public Sample([AutoInitiateWith(nameof(Create))] Sample dependency)
                {
                }

                public static Sample Create() => new Sample(null);
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    private static DiagnosticResult GetExpectedInvalidTargetRule(string member, LinePosition position, string type)
    {
        return new DiagnosticResult(AutoInitiateWithAttributeAnalyzer.InvalidTargetRule)
            .WithArguments(type, member)
            .WithLocation(position);
    }
}
