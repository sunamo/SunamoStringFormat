namespace SunamoStringFormat;

/// <summary>
/// Provides multiple string formatting methods that support custom bracket characters and error handling.
/// </summary>
public class SHFormat
{
    /// <summary>
    /// Formats a template string using <see cref="Format2"/>, then replaces standard curly braces with custom separators.
    /// Cannot be used when template contains unfinished curly braces.
    /// </summary>
    /// <param name="template">The format template string.</param>
    /// <param name="leftSeparator">The custom left separator to replace '{' with.</param>
    /// <param name="rightSeparator">The custom right separator to replace '}' with.</param>
    /// <param name="args">The format arguments.</param>
    /// <returns>The formatted string with custom separators.</returns>
    public static string Format(string template, string leftSeparator, string rightSeparator, params object[] args)
    {
        var result = Format2(template, args);
        const string replacement = "{        }";
        result = SHReplace.ReplaceAll2(result, replacement, "[]");
        result = SHReplace.ReplaceAll2(result, "{", leftSeparator);
        result = SHReplace.ReplaceAll2(result, "}", rightSeparator);
        result = SHReplace.ReplaceAll2(result, replacement, "{}");

        return result;
    }

    /// <summary>
    /// Formats a template string using <see cref="string.Format(string, object[])"/> with error checking.
    /// Does not allow formatting when template contains unfinished curly braces without indexed placeholders.
    /// </summary>
    /// <param name="template">The format template string.</param>
    /// <param name="args">The format arguments.</param>
    /// <returns>The formatted string, or the original template if formatting is not applicable.</returns>
    public static string Format2(string template, params object[] args)
    {
        if (string.IsNullOrWhiteSpace(template)) return string.Empty;

        if (template.Contains('{') && !template.Contains("{0}")) return template;

        try
        {
            return string.Format(template, args);
        }
        catch (Exception exception)
        {
            ThrowEx.ExcAsArg(exception);
            return template;
        }
    }

    /// <summary>
    /// Formats a template string by manually replacing each {i} placeholder with the corresponding argument.
    /// Supports multiline templates and can be used with wildcard characters.
    /// </summary>
    /// <param name="template">The format template string containing {0}, {1}, etc. placeholders.</param>
    /// <param name="args">The format arguments to substitute into the template.</param>
    /// <returns>The formatted string with all placeholders replaced.</returns>
    public static string Format3(string template, params object[] args)
    {
        for (var i = 0; i < args.Length; i++)
            template = SHReplace.ReplaceAll2(template, args[i].ToString() ?? string.Empty, "{" + i + "}");
        return template;
    }

    /// <summary>
    /// Formats a template string by attempting both <see cref="Format4"/> and <see cref="Format3"/>, suppressing exceptions.
    /// Supports multiline templates.
    /// </summary>
    /// <param name="template">The format template string.</param>
    /// <param name="args">The format arguments.</param>
    /// <returns>The formatted string, or the original template if both formatting attempts fail.</returns>
    public static string Format34(string template, params object[] args)
    {
        args = CA.ConvertListStringWrappedInArray(args);

        string result = template;

        try
        {
            result = Format4(template, args);
        }
        catch (Exception exception)
        {
            ThrowEx.Custom(exception, isReallyThrowing: false);
        }

        try
        {
            result = Format3(template, args);
        }
        catch (Exception exception)
        {
            ThrowEx.Custom(exception, isReallyThrowing: false);
        }

        return result;
    }

    /// <summary>
    /// Formats a template string using <see cref="string.Format(string, object[])"/> without error checking.
    /// Useful for special string formatting patterns like {0:X2}.
    /// </summary>
    /// <param name="template">The format template string.</param>
    /// <param name="args">The format arguments.</param>
    /// <returns>The formatted string.</returns>
    public static string Format4(string template, params object[] args)
    {
        args = CA.ConvertListStringWrappedInArray(args);

        return string.Format(template, args);
    }

    /// <summary>
    /// Formats a template string by manually replacing custom-bracketed placeholders with the corresponding arguments.
    /// Supports multiline templates and custom left/right separators instead of curly braces.
    /// </summary>
    /// <param name="template">The format template string.</param>
    /// <param name="leftSeparator">The custom left separator used in placeholders.</param>
    /// <param name="rightSeparator">The custom right separator used in placeholders.</param>
    /// <param name="args">The format arguments to substitute into the template.</param>
    /// <returns>The formatted string with all custom placeholders replaced.</returns>
    public static string Format5(string template, string leftSeparator, string rightSeparator, params object[] args)
    {
        for (var i = 0; i < args.Length; i++)
            template = SHReplace.ReplaceAll2(template, args[i].ToString() ?? string.Empty, leftSeparator + i + rightSeparator);

        return template;
    }
}
