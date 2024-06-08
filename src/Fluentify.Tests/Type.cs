namespace Fluentify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed record Type(INamedTypeSymbol Symbol, TypeDeclarationSyntax Syntax);