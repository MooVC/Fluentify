namespace Fluentify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Attribute = Fluentify.FluentifyAttributeGeneratorTests.WhenExecuted;
using Builder = Fluentify.BuilderDelegateGenerator;
using Descriptor = Fluentify.DescriptorAttributeGenerator;
using Ignore = Fluentify.IgnoreAttributeGeneratorTests.WhenExecuted;

internal abstract partial class Types<T>
    where T : TypeDeclarationSyntax
{
    protected Types(
        string crossReferenced,
        string multipeGenerics,
        string simple,
        string singleGeneric,
        string oneOfThreeIgnored,
        string twoOfThreeIgnored,
        string allThreeIgnored,
        string descriptorOnRequired,
        string descriptorOnOptional,
        string descriptorOnIgnored,
        string unannotated)
    {
        string code = GetCode(
            crossReferenced,
            multipeGenerics,
            simple,
            singleGeneric,
            oneOfThreeIgnored,
            twoOfThreeIgnored,
            allThreeIgnored,
            descriptorOnRequired,
            descriptorOnOptional,
            descriptorOnIgnored,
            unannotated);

        T[] declarations = GetDeclarations(code, out Compilation compilation, out SemanticModel model);

        Compilation = compilation;
        Model = model;

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
    }

    public Compilation Compilation { get; }

    public Type CrossReferenced { get; }

    public SemanticModel Model { get; }

    public Type MultipleGenerics { get; }

    public Type Simple { get; }

    public Type SingleGeneric { get; }

    public Type OneOfThreeIgnored { get; }

    public Type TwoOfThreeIgnored { get; }

    public Type AllThreeIgnored { get; }

    public Type DescriptorOnRequired { get; }

    public Type DescriptorOnOptional { get; }

    public Type DescriptorOnIgnored { get; }

    public Type Unannotated { get; }

    private static string GetCode(
        string crossReferenced,
        string multipeGenerics,
        string simple,
        string singleGeneric,
        string oneOfThreeIgnored,
        string twoOfThreeIgnored,
        string allThreeIgnored,
        string descriptorOnRequired,
        string descriptorOnOptional,
        string descriptorOnIgnored,
        string unannotated)
    {
        return $$"""
            namespace Fluentify.Tests.Compilation
            {
                using System;
                using System.Collections.Generic;

                {{crossReferenced.Indent()}}

                {{multipeGenerics.Indent()}}

                {{simple.Indent()}}

                {{singleGeneric.Indent()}}

                {{oneOfThreeIgnored.Indent()}}

                {{twoOfThreeIgnored.Indent()}}

                {{allThreeIgnored.Indent()}}

                {{descriptorOnRequired.Indent()}}

                {{descriptorOnOptional.Indent()}}

                {{descriptorOnIgnored.Indent()}}

                {{unannotated.Indent()}}
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

    private Type GetType(T[] declarations, string name)
    {
        T syntax = declarations.First(declaration => declaration.Identifier.Text == name);
        INamedTypeSymbol symbol = Model.GetDeclaredSymbol(syntax)!;

        return new(symbol, syntax);
    }
}