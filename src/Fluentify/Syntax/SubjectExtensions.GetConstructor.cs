namespace Fluentify.Syntax;

using Fluentify.Model;
using MooVC.Syntax;
using MooVC.Syntax.CSharp;

using static Fluentify.Syntax.SubjectExtensions_Resources;

using Attribute = MooVC.Syntax.CSharp.Attribute;
using Generic = MooVC.Syntax.CSharp.Generic;
using Type = MooVC.Syntax.CSharp.Type;

/// <summary>
/// Provides extensions relating to <see cref="Subject"/>.
/// </summary>
internal static partial class SubjectExtensions
{
    /// <summary>
    /// Gets the source code needed to add the default constructor to the <paramref name="subject"/>.
    /// </summary>
    /// <param name="subject">The <see cref="Subject"/> to which the default constructor is to be added.</param>
    /// <returns>
    /// The source code needed to add the default constructor to the <paramref name="subject"/>.
    /// </returns>
    public static Snippet GetConstructor(this Subject subject)
    {
        Declaration declaration = Declaration.Unspecified.Named(subject.Name)
            .WithArguments(subject.Generics.Select(generic => Generic.Undefined.Named(generic.Name)).ToArray());
        Snippet[] initializers = subject.Properties
            .Select(property => property.Kind.Type.ToSymbol(subject.SupportsNullableReferenceTypes))
            .Select(type => Snippet.From(Configuration.Options, $"default({type.ToSnippet(Configuration.Options)})"))
            .ToArray();

        // An empty argument snippet keeps the initializer when the record has no properties.
        Constructor constructor = Constructor.Undefined.WithArguments(initializers.Length == 0 ? [Snippet.Empty] : initializers);
        Record record = Record.Undefined.Named(declaration)
            .WithExtensibility(Modifiers.Implicit)
            .WithScope(Scopes.Unspecified)
            .WithConstructors(constructor);
        Attribute attribute = (Symbol)(Name: "SetsRequiredMembersAttribute", Qualifier: "System.Diagnostics.CodeAnalysis");

        // MooVC 10.1.0 emits base for constructor delegation. A positional record delegates to this.
        Snippet original = constructor.ToSnippet(Configuration.Options, record);
        string delegated = original.ToString().Replace(": base(", ": this(");
        Snippet decorated = Snippet.From(
            Configuration.Options,
            string.Format(ConstructorDirectives, attribute.ToSnippet(Configuration.Options), delegated));
        original = original.Shift(Configuration.Options);
        decorated = decorated.Shift(Configuration.Options);

        Type nested = record;
        foreach (Nesting parent in subject.Nesting.Reverse())
        {
            nested = NestConstructor(parent, nested);
            original = original.Shift(Configuration.Options);
            decorated = decorated.Shift(Configuration.Options);
        }

        string code = nested.ToSnippet(Configuration.Options);

        return Snippet.From(Configuration.Options, code.Replace(original.ToString(), decorated.ToString()));
    }

    private static Type NestConstructor(Nesting parent, Type nested)
    {
        Declaration declaration = parent.Qualification;

        return parent.Declaration switch
        {
            "partial class" => Class.Undefined.Named(declaration)
                .WithExtensibility(Modifiers.Implicit)
                .WithScope(Scopes.Unspecified)
                .Containing(nested),
            "partial interface" => Interface.Undefined.Named(declaration)
                .WithScope(Scopes.Unspecified)
                .Containing(nested),
            "partial record" => Record.Undefined.Named(declaration)
                .WithExtensibility(Modifiers.Implicit)
                .WithScope(Scopes.Unspecified)
                .Containing(nested),
            "partial record struct" => NestInStruct(Struct.Kinds.Record),
            "partial struct" => NestInStruct(Struct.Kinds.Default),
            "readonly partial record struct" => NestInStruct(Struct.Kinds.ReadOnly + Struct.Kinds.Record),
            "readonly partial struct" => NestInStruct(Struct.Kinds.ReadOnly),
            "readonly ref partial struct" => NestInStruct(Struct.Kinds.ReadOnly + Struct.Kinds.Ref),
            "ref partial struct" => NestInStruct(Struct.Kinds.Ref),
            _ => throw new InvalidOperationException(parent.Declaration),
        };

        Struct NestInStruct(Struct.Kinds kind)
        {
            return Struct.Undefined.Named(declaration)
                .WithBehavior(kind)
                .WithScope(Scopes.Unspecified)
                .Containing(nested);
        }
    }
}