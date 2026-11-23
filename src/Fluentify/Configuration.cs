namespace Fluentify;

using MooVC.Syntax;
using MooVC.Syntax.CSharp;
using Options = MooVC.Syntax.CSharp.Options;

internal static class Configuration
{
    public static Options Options { get; } = new Options()
        .WithNamespace(Qualifier.Options.Block)
        .WithTypes(types => types.WithQualifications(qualifications => qualifications.WithFormat(Qualification.Options.Formats.Global)));
}