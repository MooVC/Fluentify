namespace Fluentify.SkipAutoInitializationAttributeAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<SkipAutoInitializationAttributeAnalyzer, AutoInitializeWithAttributeGenerator>
{
    public WhenExecuted()
        : base(
            Classes.ReferenceAssemblies,
            Classes.LanguageVersion,
            typeof(AutoInitializeWithAttributeGenerator),
            typeof(FluentifyAttributeGenerator),
            typeof(SkipAutoInitializationAttributeGenerator))
    {
    }

    [Fact]
    public async Task GivenAutoInitializeWithOnlyThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            using Fluentify;

            [AutoInitializeWith(nameof(Default))]
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
            [AutoInitializeWith(nameof(Default))]
            public sealed class Sample
            {
                public static Sample Default => new Sample();
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAttributeOnPropertyThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            using Fluentify;

            public sealed class Sample
            {
                [AutoInitializeWith(nameof(Create))]
                public Sample Property { get; } = new Sample();

                public static Sample Create() => new Sample();
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