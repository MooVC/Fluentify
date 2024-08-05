namespace Fluentify.Snippets;

public static class Extensions
{
    public static readonly Generated Internal = new(InternalExtensionsGenerator.Source, typeof(InternalExtensionsGenerator), "Fluentify.Internal.Extensions");
}