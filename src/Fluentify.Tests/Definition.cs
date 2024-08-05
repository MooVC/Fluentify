namespace Fluentify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed record Definition(INamedTypeSymbol Symbol, TypeDeclarationSyntax Syntax);