namespace Fluentify;

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

        Boolean = GetType(declarations, nameof(Boolean));
        CrossReferenced = GetType(declarations, nameof(CrossReferenced));
        MultipleGenerics = GetType(declarations, nameof(MultipleGenerics));
        Simple = GetType(declarations, nameof(Simple));
        SingleGeneric = GetType(declarations, nameof(SingleGeneric));
        OneOfThreeIgnored = GetType(declarations, nameof(OneOfThreeIgnored));
        TwoOfThreeIgnored = GetType(declarations, nameof(TwoOfThreeIgnored));
        AllThreeIgnored = GetType(declarations, nameof(AllThreeIgnored));
        DescriptorOnRequired = GetType(declarations, nameof(DescriptorOnRequired));
        DescriptorOnOptional = GetType(declarations, nameof(DescriptorOnOptional));
        DescriptorOnIgnored = GetType(declarations, nameof(DescriptorOnIgnored));
        Unannotated = GetType(declarations, nameof(Unannotated));
        Unsupported = GetType(declarations, nameof(Unsupported));
    }

    public Compilation Compilation { get; }

    public Definition Boolean { get; }

    public Definition CrossReferenced { get; }

    public SemanticModel Model { get; }

    public Definition MultipleGenerics { get; }

    public Definition Simple { get; }

    public Definition SingleGeneric { get; }

    public Definition OneOfThreeIgnored { get; }

    public Definition TwoOfThreeIgnored { get; }

    public Definition AllThreeIgnored { get; }

    public Definition DescriptorOnRequired { get; }

    public Definition DescriptorOnOptional { get; }

    public Definition DescriptorOnIgnored { get; }

    public Definition Unannotated { get; }

    public Definition Unsupported { get; }

    private static string GetCode(string[] types)
    {
        string code = string.Join("\r\n\r\n", types);

        return $$"""
            namespace Fluentify.Tests.Compilation
            {
                using System;
                using System.Collections.Generic;

                {{code.Indent()}}
            }
            """;
    }

    private static T[] GetDeclarations(string code, out Compilation compilation, out SemanticModel model)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

        compilation = CSharpCompilation.Create("Fluentify.Tests.Compilation", [tree]);
        model = compilation.GetSemanticModel(tree);

        return root
            .DescendantNodes()
            .OfType<T>()
            .ToArray();
    }

    private Definition GetType(T[] declarations, string name)
    {
        T syntax = declarations.First(declaration => declaration.Identifier.Text == name);
        INamedTypeSymbol symbol = Model.GetDeclaredSymbol(syntax)!;

        return new(symbol, syntax);
    }
}