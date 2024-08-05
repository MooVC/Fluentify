namespace Fluentify.ClassAnalyzerTests;

using System.Threading.Tasks;
using Fluentify.Snippets;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

public sealed class WhenExecuted
    : AnalyzerTests<ClassAnalyzer, IgnoreAttributeGenerator>
{
    public WhenExecuted()
        : base(Classes.ReferenceAssemblies, Classes.LanguageVersion)
    {
    }

    [Fact]
    public async Task GivenAClassWithAnImplicitDefaultConstructorWhenFluentifyIsNotAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            public class UnannotatedClassWithImplicitDefaultConstructor
            {
                public string Property { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAClassWithAnImplicitDefaultConstructorWhenFluentifyIsAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            using Fluentify;

            [Fluentify]
            public class AnnotatedClassWithImplicitDefaultConstructor
            {
                public string Property { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAClassWithAnExplicitDefaultConstructorWhenFluentifyIsNotAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            public class UnannotatedClassWithExplicitDefaultConstructor
            {
                internal UnannotatedClassWithExplicitDefaultConstructor()
                {
                }

                public string Property { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAClassWithAnExplicitDefaultConstructorWhenFluentifyIsAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            using Fluentify;

            [Fluentify]
            public class AnnotatedClassWithExplicitDefaultConstructor
            {
                internal AnnotatedClassWithExplicitDefaultConstructor()
                {
                }

                public string Property { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAClassWithAnExplicitPrivateDefaultConstructorWhenFluentifyIsNotAppliedThenNoDiagnosticIsRaised()
    {
        // Arrange
        TestCode = """
            public class UnannotatedClassWithPrivateDefaultConstructor
            {
                private UnannotatedClassWithPrivateDefaultConstructor()
                {
                }

                public string Property { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    [Fact]
    public async Task GivenAClassWithAnExplicitPrivateDefaultConstructorWhenFluentifyIsAppliedThenGetExpectedAccessibleDefaultConstructorRuleIsRaised()
    {
        // Arrange
        ExpectedDiagnostics.Add(GetExpectedAccessibleDefaultConstructorRule(new LinePosition(3, 13), "AnnotatedClassWithPrivateDefaultConstructor"));

        TestCode = """
            using Fluentify;

            [Fluentify]
            public class AnnotatedClassWithPrivateDefaultConstructor
            {
                private AnnotatedClassWithPrivateDefaultConstructor()
                {
                }

                public string Property { get; set; }
            }
            """;

        // Act & Assert
        await ActAndAssertAsync();
    }

    private static DiagnosticResult GetExpectedAccessibleDefaultConstructorRule(LinePosition position, string @class)
    {
        return new DiagnosticResult(ClassAnalyzer.AccessibleDefaultConstructorRule)
            .WithArguments(@class)
            .WithLocation(position);
    }
}