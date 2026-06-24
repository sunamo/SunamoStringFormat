namespace SunamoStringFormat;

public class SHFormat
{
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

    public static string Format3(string template, params object[] args)
    {
        for (var i = 0; i < args.Length; i++)
            template = SHReplace.ReplaceAll2(template, args[i].ToString() ?? string.Empty, "{" + i + "}");
        return template;
    }

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

    public static string Format4(string template, params object[] args)
    {
        args = CA.ConvertListStringWrappedInArray(args);

        return string.Format(template, args);
    }

    public static string Format5(string template, string leftSeparator, string rightSeparator, params object[] args)
    {
        for (var i = 0; i < args.Length; i++)
            template = SHReplace.ReplaceAll2(template, args[i].ToString() ?? string.Empty, leftSeparator + i + rightSeparator);

        return template;
    }
}
