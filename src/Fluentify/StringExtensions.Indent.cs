namespace Fluentify;

using System.IO;
using System.Text;

/// <summary>
/// Provides extensions relating to <see cref="string"/>.
/// </summary>
internal static partial class StringExtensions
{
    private const string Default = "    ";

    /// <summary>
    /// Adds the specified <paramref name="whitespace"/> to the beginning of each non-blank line in the input string.
    /// </summary>
    /// <param name="input">The input string to process.</param>
    /// <param name="skip">The number of initial lines to skip when considering the application of whitespace.</param>
    /// <param name="times">The number of times to apply the indent.</param>
    /// <param name="whitespace">The whitespace to apply when indenting.</param>
    /// <returns>A new string with <paramref name="whitespace"/> added to the start of each non-blank line.</returns>
    public static string Indent(this string input, int skip = 1, int times = 1, string whitespace = Default)
    {
        var reader = new StringReader(input);
        var builder = new StringBuilder();
        string line;
        int offset = 0;

        if (times > 1)
        {
            whitespace = CreateWhitespace(times, whitespace);
        }

        while ((line = reader.ReadLine()) is not null)
        {
            if (++offset > skip && !string.IsNullOrWhiteSpace(line))
            {
                builder = builder.Append(whitespace);
            }

            builder = builder.Append(line);
            builder = builder.AppendLine();
        }

        return builder
            .ToString()
            .TrimEnd();
    }

    private static string CreateWhitespace(int times, string whitespace)
    {
        var builder = new StringBuilder();

        for (int index = 0; index < times; index++)
        {
            _ = builder.Append(whitespace);
        }

        return builder.ToString();
    }
}