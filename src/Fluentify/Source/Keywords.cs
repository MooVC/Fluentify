namespace Fluentify.Source;

using System;
using System.Collections.Immutable;
using System.Text;

/// <summary>
/// Represents a C# syntax keywords.
/// </summary>
internal static class Keywords
{
    /// <summary>
    /// Represents the reserved for the Keywords.
    /// </summary>
    public static readonly ImmutableHashSet<string> Reserved =
        ImmutableHashSet.Create(
            StringComparer.Ordinal,
            "abstract",
            "as",
            "base",
            "break",
            "case",
            "catch",
            "checked",
            "class",
            "const",
            "continue",
            "default",
            "delegate",
            "do",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "file",
            "finally",
            "fixed",
            "for",
            "foreach",
            "global",
            "goto",
            "if",
            "implicit",
            "in",
            "interface",
            "internal",
            "is",
            "lock",
            "namespace",
            "new",
            "null",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "required",
            "return",
            "scoped",
            "sealed",
            "sizeof",
            "stackalloc",
            "static",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "unchecked",
            "unsafe",
            "using",
            "virtual",
            "void",
            "volatile",
            "while");

    /// <summary>
    /// Performs the is reserved operation for the C# syntax.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The bool.</returns>
    public static bool IsReserved(this string value)
    {
        return !string.IsNullOrWhiteSpace(value) && Reserved.Contains(value);
    }

    /// <summary>
    /// Performs the is reserved operation for the C# syntax.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The bool.</returns>
    public static bool IsReserved(this StringBuilder value)
    {
        if (value is null)
        {
            return false;
        }

        return value
            .ToString()
            .IsReserved();
    }
}