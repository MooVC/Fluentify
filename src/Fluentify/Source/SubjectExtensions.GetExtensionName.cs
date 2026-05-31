namespace Fluentify.Source;

using System.Collections.Generic;
using System.Linq;
using Fluentify.Model;

/// <summary>
/// Provides extensions relating to <see cref="Subject"/>.
/// </summary>
internal static partial class SubjectExtensions
{
    /// <summary>
    /// Gets the generated extension class name for the <paramref name="subject"/>.
    /// </summary>
    /// <param name="subject">The <see cref="Subject"/> for which the extension class name is required.</param>
    /// <returns>The generated extension class name for <paramref name="subject"/>.</returns>
    public static string GetExtensionClassName(this Subject subject)
    {
        IEnumerable<string> names = subject.Nesting
            .Select(nesting => nesting.Name)
            .Append(subject.Name)
            .Append("Extensions");

        return string.Concat(names);
    }
}