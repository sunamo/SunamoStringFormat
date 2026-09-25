namespace SunamoStringFormat._sunamo.SunamoStringReplace;

/// <summary>
/// Provides string replacement utility methods.
/// </summary>
internal class SHReplace
{
    /// <summary>
    /// Replaces all occurrences of <paramref name="what"/> with <paramref name="replacement"/> in the given <paramref name="text"/>.
    /// </summary>
    /// <param name="text">The input string to perform replacements on.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="what">The string to find and replace.</param>
    /// <returns>The resulting string after all replacements.</returns>
    internal static string ReplaceAll2(string text, string replacement, string what)
    {
        return text.Replace(what, replacement);
    }
}
