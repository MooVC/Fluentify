namespace Fluentify.Semantics.ITypeSymbolExtensionsTests;

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public sealed class WhenTryResolveIsCalled
{
    private const string DefaultMember = "Default";
    private const string InstanceFieldMember = "InstanceField";
    private const string MethodMember = "Create";
    private const string MethodWithParametersMember = "Build";
    private const string PrivateMember = "Secret";
    private const string WhitespaceMember = " ";

    private static readonly Compilation Compilation = CreateCompilation();
    private static readonly INamedTypeSymbol FactoryType = Compilation.GetTypeByMetadataName("Demo.Factory")!;

    [Fact]
    public void GivenNullTargetThenFalseIsReturned()
    {
        // Arrange
        ITypeSymbol? target = null;
        var member = MethodMember;

        // Act
        bool result = target.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenWhitespaceMemberThenFalseIsReturned()
    {
        // Arrange
        var member = WhitespaceMember;

        // Act
        bool result = FactoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenStaticMethodReturningTypeThenInitializationIsReturned()
    {
        // Arrange
        var member = MethodMember;

        // Act
        bool result = FactoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Factory.Create()");
    }

    [Fact]
    public void GivenStaticPropertyReturningTypeThenInitializationIsReturned()
    {
        // Arrange
        var member = DefaultMember;

        // Act
        bool result = FactoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Factory.Default");
    }

    [Fact]
    public void GivenStaticFieldReturningTypeThenInitializationIsReturned()
    {
        // Arrange
        var member = InstanceFieldMember;

        // Act
        bool result = FactoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Factory.InstanceField");
    }

    [Fact]
    public void GivenPrivateMemberThenFalseIsReturned()
    {
        // Arrange
        var member = PrivateMember;

        // Act
        bool result = FactoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenMethodWithParametersThenFalseIsReturned()
    {
        // Arrange
        var member = MethodWithParametersMember;

        // Act
        bool result = FactoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenNameofExpressionThenMemberIsExtracted()
    {
        // Arrange
        var member = "nameof(Default)";

        // Act
        bool result = FactoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        member.ShouldBe(DefaultMember);
        initialization.ShouldBe("global::Demo.Factory.Default");
    }

    private static Compilation CreateCompilation()
    {
        var tree = CSharpSyntaxTree.ParseText(
            """
            namespace Demo
            {
                public sealed class Factory
                {
                    public static Factory Create() => new Factory();

                    public static Factory Default => new Factory();

                    public static Factory InstanceField = new Factory();

                    public static Factory Build(int value) => new Factory();

                    private static Factory Secret => new Factory();
                }
            }
            """);

        return CSharpCompilation.Create(
            "Fluentify.Tests",
            [tree],
            GetReferences(),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private static ImmutableArray<MetadataReference> GetReferences()
    {
        return
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
        ];
    }
}
