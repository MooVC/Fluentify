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

    private static readonly Compilation _compilation = CreateCompilation();
    private static readonly INamedTypeSymbol _factoryType = _compilation.GetTypeByMetadataName("Demo.Factory")!;

    [Fact]
    public void GivenNullTargetThenFalseIsReturned()
    {
        // Arrange
        ITypeSymbol? target = default;
        string member = MethodMember;

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
        string member = WhitespaceMember;

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenNullTypeForOverloadThenFalseIsReturned()
    {
        // Arrange
        ITypeSymbol? type = default;
        string member = MethodMember;

        // Act
        bool result = _factoryType.TryResolve(type, ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenNullSourceForOverloadThenFalseIsReturned()
    {
        // Arrange
        ITypeSymbol? source = default;
        string member = MethodMember;

        // Act
        bool result = source.TryResolve(_factoryType, ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenNameofExpressionWithWhitespaceThenMemberIsExtracted()
    {
        // Arrange
        string member = "nameof( Default )";

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        member.ShouldBe(DefaultMember);
        initialization.ShouldBe("global::Demo.Factory.Default");
    }

    [Fact]
    public void GivenStaticMethodReturningTypeThenInitializationIsReturned()
    {
        // Arrange
        string member = MethodMember;

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Factory.Create()");
    }

    [Fact]
    public void GivenStaticPropertyReturningTypeThenInitializationIsReturned()
    {
        // Arrange
        string member = DefaultMember;

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Factory.Default");
    }

    [Fact]
    public void GivenStaticFieldReturningTypeThenInitializationIsReturned()
    {
        // Arrange
        string member = InstanceFieldMember;

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        initialization.ShouldBe("global::Demo.Factory.InstanceField");
    }

    [Fact]
    public void GivenPrivateMemberThenFalseIsReturned()
    {
        // Arrange
        string member = PrivateMember;

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenMethodWithParametersThenFalseIsReturned()
    {
        // Arrange
        string member = MethodWithParametersMember;

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        initialization.ShouldBe(string.Empty);
    }

    [Fact]
    public void GivenNameofExpressionThenMemberIsExtracted()
    {
        // Arrange
        string member = "nameof(Default)";

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeTrue();
        member.ShouldBe(DefaultMember);
        initialization.ShouldBe("global::Demo.Factory.Default");
    }

    [Fact]
    public void GivenNameofExpressionWithMemberAccessThenFalseIsReturned()
    {
        // Arrange
        string member = "nameof(Factory.Default)";

        // Act
        bool result = _factoryType.TryResolve(ref member, out string initialization);

        // Assert
        result.ShouldBeFalse();
        member.ShouldBe("Factory.Default");
        initialization.ShouldBe(string.Empty);
    }

    private static Compilation CreateCompilation()
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText("""
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