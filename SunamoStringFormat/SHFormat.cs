namespace SunamoStringFormat;

/// <summary>
/// Represents the SH format type.
/// </summary>
public class SHFormat
{
    /// <summary>
    /// Format.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="leftSeparator">The left separator.</param>
    /// <param name="rightSeparator">The right separator.</param>
    /// <param name="args">The args.</param>
    /// <returns>The format result.</returns>
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
    /// Format 2.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="args">The args.</param>
    /// <returns>The format 2 result.</returns>
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
    /// Format 3.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="args">The args.</param>
    /// <returns>The format 3 result.</returns>
    public static string Format3(string template, params object[] args)
    {
        for (var i = 0; i < args.Length; i++)
            template = SHReplace.ReplaceAll2(template, args[i].ToString() ?? string.Empty, "{" + i + "}");
        return template;
    }

    /// <summary>
    /// Format 34.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="args">The args.</param>
    /// <returns>The format 34 result.</returns>
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
    /// Format 4.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="args">The args.</param>
    /// <returns>The format 4 result.</returns>
    public static string Format4(string template, params object[] args)
    {
        args = CA.ConvertListStringWrappedInArray(args);

        return string.Format(template, args);
    }

    /// <summary>
    /// Format 5.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="leftSeparator">The left separator.</param>
    /// <param name="rightSeparator">The right separator.</param>
    /// <param name="args">The args.</param>
    /// <returns>The format 5 result.</returns>
    public static string Format5(string template, string leftSeparator, string rightSeparator, params object[] args)
    {
        for (var i = 0; i < args.Length; i++)
            template = SHReplace.ReplaceAll2(template, args[i].ToString() ?? string.Empty, leftSeparator + i + rightSeparator);

        return template;
    }
}
