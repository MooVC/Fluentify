namespace Fluentify.Semantics.ITypeSymbolExtensionsTests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public sealed class WhenHasAccessibleParameterlessConstructorIsCalled
{
    private const string PublicClassWithNoConstructor = """
        public class PublicClassWithNoConstructor
        {
        }
        """;

    private const string PublicClassWithPublicConstructor = """
        public class PublicClassWithPublicConstructor
        {
            public PublicClassWithPublicConstructor()
            {
            }
        }
        """;

    private const string PublicClassWithInternalConstructor = """
        public class PublicClassWithInternalConstructor
        {
            internal PublicClassWithInternalConstructor()
            {
            }
        }
        """;

    private const string PublicClassWithPrivateConstructor = """
        public class PublicClassWithPrivateConstructor
        {
            private PublicClassWithPrivateConstructor()
            {
            }
        }
        """;

    private const string InternalClassWithNoConstructor = """
        internal class InternalClassWithNoConstructor
        {
        }
        """;

    private const string InternalClassWithPublicConstructor = """
        internal class InternalClassWithPublicConstructor
        {
            public InternalClassWithPublicConstructor()
            {
            }
        }
        """;

    private const string InternalClassWithInternalConstructor = """
        internal class InternalClassWithInternalConstructor
        {
            internal InternalClassWithInternalConstructor()
            {
            }
        }
        """;

    private const string InternalClassWithPrivateConstructor = """
        internal class InternalClassWithPrivateConstructor
        {
            private InternalClassWithPrivateConstructor()
            {
            }
        }
        """;

    private const string PublicAbstractClass = """
        public abstract class PublicAbstractClass
        {
            private PublicAbstractClass()
            {
            }
        }
        """;

    private const string Struct = """
        public struct Struct
        {
            public Struct(int x)
            {
            }
        }
        """;

    [Theory]
    [InlineData(PublicClassWithNoConstructor, nameof(PublicClassWithNoConstructor), true)]
    [InlineData(PublicClassWithPublicConstructor, nameof(PublicClassWithPublicConstructor), true)]
    [InlineData(PublicClassWithInternalConstructor, nameof(PublicClassWithInternalConstructor), false)]
    [InlineData(PublicClassWithPrivateConstructor, nameof(PublicClassWithPrivateConstructor), false)]
    [InlineData(InternalClassWithNoConstructor, nameof(InternalClassWithNoConstructor), false)]
    [InlineData(InternalClassWithPublicConstructor, nameof(InternalClassWithPublicConstructor), false)]
    [InlineData(InternalClassWithInternalConstructor, nameof(InternalClassWithInternalConstructor), false)]
    [InlineData(InternalClassWithPrivateConstructor, nameof(InternalClassWithPrivateConstructor), false)]
    [InlineData(PublicAbstractClass, nameof(PublicAbstractClass), false)]
    [InlineData(Struct, nameof(Struct), false)]
    public void GivenCodeWhenInTheSameAssemblyThenExpectedOutcomeIsObserved(string code, string type, bool expected)
    {
        // Arrange
        Compilation compilation = CreateCompilation(code);
        INamedTypeSymbol symbol = compilation.GetTypeByMetadataName(type)!;

        // Act
        bool actual = symbol.HasAccessibleParameterlessConstructor(Records.Instance.Compilation, out bool isInternal);

        // Assert
        isInternal.ShouldBeFalse();
        actual.ShouldBe(expected);
    }

    [Theory]
    [InlineData(PublicClassWithNoConstructor, nameof(PublicClassWithNoConstructor), true)]
    [InlineData(PublicClassWithPublicConstructor, nameof(PublicClassWithPublicConstructor), true)]
    [InlineData(PublicClassWithInternalConstructor, nameof(PublicClassWithInternalConstructor), true)]
    [InlineData(PublicClassWithPrivateConstructor, nameof(PublicClassWithPrivateConstructor), false)]
    [InlineData(InternalClassWithNoConstructor, nameof(InternalClassWithNoConstructor), true)]
    [InlineData(InternalClassWithPublicConstructor, nameof(InternalClassWithPublicConstructor), true)]
    [InlineData(InternalClassWithInternalConstructor, nameof(InternalClassWithInternalConstructor), true)]
    [InlineData(InternalClassWithPrivateConstructor, nameof(InternalClassWithPrivateConstructor), false)]
    [InlineData(PublicAbstractClass, nameof(PublicAbstractClass), false)]
    [InlineData(Struct, nameof(Struct), false)]
    public void GivenCodeWhenInADifferentAssemblyThenExpectedOutcomeIsObserved(string code, string type, bool expected)
    {
        // Arrange
        Compilation compilation = CreateCompilation(code);
        INamedTypeSymbol symbol = compilation.GetTypeByMetadataName(type)!;

        // Act
        bool actual = symbol.HasAccessibleParameterlessConstructor(compilation, out bool isInternal);

        // Assert
        isInternal.ShouldBeTrue();
        actual.ShouldBe(expected);
    }

    private static Compilation CreateCompilation(string code)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

        return CSharpCompilation.Create("TestAssembly", [tree]);
    }
}