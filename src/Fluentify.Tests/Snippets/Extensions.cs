namespace Fluentify.Snippets;

public static class Extensions
{
    public static readonly Generated Internal = new(
        """
            namespace Fluentify.Internal
            {
                internal static class Extensions
                {
                    public static void ThrowIfNull<T>(this T subject, string paramName)
                    {
                        if (subject == null)
                        {
                            throw new global::System.ArgumentNullException(paramName);
                        }
                    }
                }
            }
            """,
        typeof(InternalExtensionsGenerator),
        "Fluentify.Internal.Extensions");
}