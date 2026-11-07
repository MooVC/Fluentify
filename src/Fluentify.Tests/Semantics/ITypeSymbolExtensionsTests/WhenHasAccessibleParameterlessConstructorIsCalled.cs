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

    [Test]
    [Arguments(PublicClassWithNoConstructor, nameof(PublicClassWithNoConstructor), true)]
    [Arguments(PublicClassWithPublicConstructor, nameof(PublicClassWithPublicConstructor), true)]
    [Arguments(PublicClassWithInternalConstructor, nameof(PublicClassWithInternalConstructor), true)]
    [Arguments(PublicClassWithPrivateConstructor, nameof(PublicClassWithPrivateConstructor), false)]
    [Arguments(InternalClassWithNoConstructor, nameof(InternalClassWithNoConstructor), true)]
    [Arguments(InternalClassWithPublicConstructor, nameof(InternalClassWithPublicConstructor), true)]
    [Arguments(InternalClassWithInternalConstructor, nameof(InternalClassWithInternalConstructor), true)]
    [Arguments(InternalClassWithPrivateConstructor, nameof(InternalClassWithPrivateConstructor), false)]
    [Arguments(PublicAbstractClass, nameof(PublicAbstractClass), false)]
    [Arguments(Struct, nameof(Struct), false)]
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

    [Test]
    [Arguments(PublicClassWithNoConstructor, nameof(PublicClassWithNoConstructor), true)]
    [Arguments(PublicClassWithPublicConstructor, nameof(PublicClassWithPublicConstructor), true)]
    [Arguments(PublicClassWithInternalConstructor, nameof(PublicClassWithInternalConstructor), false)]
    [Arguments(PublicClassWithPrivateConstructor, nameof(PublicClassWithPrivateConstructor), false)]
    [Arguments(InternalClassWithNoConstructor, nameof(InternalClassWithNoConstructor), false)]
    [Arguments(InternalClassWithPublicConstructor, nameof(InternalClassWithPublicConstructor), false)]
    [Arguments(InternalClassWithInternalConstructor, nameof(InternalClassWithInternalConstructor), false)]
    [Arguments(InternalClassWithPrivateConstructor, nameof(InternalClassWithPrivateConstructor), false)]
    [Arguments(PublicAbstractClass, nameof(PublicAbstractClass), false)]
    [Arguments(Struct, nameof(Struct), false)]
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

    private static Compilation CreateCompilation(string code)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

        return CSharpCompilation.Create("TestAssembly", [tree]);
    }
}