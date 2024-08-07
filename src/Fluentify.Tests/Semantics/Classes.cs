﻿namespace Fluentify.Semantics;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Fluentify.Snippets.Classes;

internal sealed class Classes
    : Types<ClassDeclarationSyntax>
{
    public static readonly Classes Instance = new();

    private Classes()
        : base(
            AllThreeIgnoredContent,
            BooleanContent,
            CrossReferencedContent,
            DescriptorOnIgnoredContent,
            DescriptorOnOptionalContent,
            DescriptorOnRequiredContent,
            GlobalContent,
            MultipleGenericsContent,
            OneOfThreeIgnoredContent,
            SimpleContent,
            SingleGenericContent,
            TwoOfThreeIgnoredContent,
            UnannotatedContent,
            UnsupportedContent)
    {
    }
}