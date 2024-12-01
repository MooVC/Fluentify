namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal abstract partial class Types<T>
    where T : TypeDeclarationSyntax
{
    protected Types(params string[] types)
    {
        string code = GetCode(types);

        T[] declarations = GetDeclarations(code, out Compilation compilation, out SemanticModel model);

        Compilation = compilation;
        Model = model;

        AllThreeIgnored = GetDefinition(declarations, nameof(AllThreeIgnored));
        Boolean = GetDefinition(declarations, nameof(Boolean));
        CrossReferenced = GetDefinition(declarations, nameof(CrossReferenced));
        DescriptorOnIgnored = GetDefinition(declarations, nameof(DescriptorOnIgnored));
        DescriptorOnOptional = GetDefinition(declarations, nameof(DescriptorOnOptional));
        DescriptorOnRequired = GetDefinition(declarations, nameof(DescriptorOnRequired));
        Global = GetDefinition(declarations, nameof(Global));
        InvalidDescriptor = GetDefinition(declarations, nameof(InvalidDescriptor));
        OneOfThreeIgnored = GetDefinition(declarations, nameof(OneOfThreeIgnored));
        MultipleGenerics = GetDefinition(declarations, nameof(MultipleGenerics));
        Nested = GetDefinition(declarations, nameof(Nested));
        SelfDescriptorOnIgnored = GetDefinition(declarations, nameof(SelfDescriptorOnIgnored));
        SelfDescriptorOnOptional = GetDefinition(declarations, nameof(SelfDescriptorOnOptional));
        SelfDescriptorOnRequired = GetDefinition(declarations, nameof(SelfDescriptorOnRequired));
        Simple = GetDefinition(declarations, nameof(Simple));
        SingleGeneric = GetDefinition(declarations, nameof(SingleGeneric));
        TwoOfThreeIgnored = GetDefinition(declarations, nameof(TwoOfThreeIgnored));
        Unannotated = GetDefinition(declarations, nameof(Unannotated));
        Unsupported = GetDefinition(declarations, nameof(Unsupported));
    }

    public Compilation Compilation { get; }

    public SemanticModel Model { get; }

    public Definition AllThreeIgnored { get; }

    public Definition Boolean { get; }

    public Definition CrossReferenced { get; }

    public Definition DescriptorOnIgnored { get; }

    public Definition DescriptorOnOptional { get; }

    public Definition DescriptorOnRequired { get; }

    public Definition Global { get; }

    public Definition InvalidDescriptor { get; }

    public Definition MultipleGenerics { get; }

    public Definition Nested { get; }

    public Definition OneOfThreeIgnored { get; }

    public Definition SelfDescriptorOnIgnored { get; }

    public Definition SelfDescriptorOnOptional { get; }

    public Definition SelfDescriptorOnRequired { get; }

    public Definition Simple { get; }

    public Definition SingleGeneric { get; }

    public Definition TwoOfThreeIgnored { get; }

    public Definition Unannotated { get; }

    public Definition Unsupported { get; }

    private static string GetCode(string[] types)
    {
        return string.Join("\r\n\r\n", types);
    }

    private static T[] GetDeclarations(string code, out Compilation compilation, out SemanticModel model)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

        compilation = CSharpCompilation.Create("Fluentify.Tests", [tree]);
        model = compilation.GetSemanticModel(tree);

        return root
            .DescendantNodes()
            .OfType<T>()
            .ToArray();
    }

    private Definition GetDefinition(T[] declarations, string name)
    {
        T syntax = declarations.First(declaration => declaration.Identifier.Text == name);
        INamedTypeSymbol symbol = Model.GetDeclaredSymbol(syntax)!;

        return new(symbol, syntax);
    }
}