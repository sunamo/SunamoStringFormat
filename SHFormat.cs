namespace SunamoStringFormat;

public class SHFormat
{
    /// <summary>
    ///     Don't allow format when there is unfinished {
    ///     Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    ///     Format2 - use string.Format with error checking
    ///     Format3 - Replace {x} with my code. Can be used with wildcard
    ///     Format4 - use string.Format without error checking
    ///     Cannot be use on existing code - will corrupt them
    /// </summary>
    /// <param name="templateHandler"></param>
    /// <param name="lsf"></param>
    /// <param name="rsf"></param>
    /// <param name="id"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static string Format(string templateHandler, string lsf, string rsf, params object[] args)
    {

        //ThrowEx.PassedListInsteadOfArray(nameof(args), args);
        ////args = CA.ConvertListStringWrappedInArray(args);

        var result = Format2(templateHandler, args);
        const string replacement = "{        }";
        result = SHReplace.ReplaceAll2(result, replacement, "[]");
        result = SHReplace.ReplaceAll2(result, "{", lsf);
        result = SHReplace.ReplaceAll2(result, "}", rsf);
        result = SHReplace.ReplaceAll2(result, replacement, "{}");
        //result = Format4(result, args);

        return result;
    }

    /// <summary>
    ///     Don't allow format when there is unfinished {
    ///     Usage: Exc.WrongCountInList2
    /// </summary>
    /// <param name="status"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string Format2(string status, params object[] args)
    {
        if (string.IsNullOrWhiteSpace(status)) return string.Empty;

        if (status.Contains('{') && !status.Contains("{0}")) return status;

        try
        {
            return string.Format(status, args);
        }
        catch (Exception ex)
        {
            ThrowEx.ExcAsArg(ex);
            return status;
        }
    }

    /// <summary>
    ///     Multiline: yes
    ///     Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    ///     Format2 - use string.Format with error checking
    ///     Format3 - Replace {x} with my code. Can be used with wildcard
    ///     Format4 - use string.Format without error checking
    ///     Manually replace every {i}
    /// </summary>
    /// <param name="template"></param>
    /// <param name="args"></param>
    public static string Format3(string template, params object[] args)
    {
        //ThrowEx.PassedListInsteadOfArray(nameof(args), args);

        //args = CA.ConvertListStringWrappedInArray(args);

        // this was original implementation but dont know why isnt used string.format
        for (var i = 0; i < args.Length; i++)
            template = SHReplace.ReplaceAll2(template, args[i].ToString(), "{" + i + "}");
        return template;
    }

    /// <summary>
    ///     Multiline: yes
    ///     Throw no exceptions => Dummy
    ///     params cant be object - ConvertListStringWrappedInArray would like to cast into List<object> from List<string>
    /// </summary>
    /// <param name="c"></param>
    /// <param name="innerMain"></param>
    /// <returns></returns>
    public static string Format34(string c, params object[] innerMain)
    {
        //ThrowEx.PassedListInsteadOfArray(nameof(innerMain), innerMain);

        innerMain = CA.ConvertListStringWrappedInArray(innerMain);

        string formatted = null;

        try
        {
            formatted = Format4(c, innerMain);
        }
        catch (Exception ex)
        {
            ThrowEx.Custom(ex);
        }

        try
        {
            formatted = Format3(c, innerMain);
        }
        catch (Exception ex)
        {
            ThrowEx.Custom(ex);
        }

        return formatted;
    }

    /// <summary>
    /// 
    ///     Don't allow format when there is unfinished {
    ///     Format - use string.Format with error checking, as only one can be use wich { } [ ] chars in text
    ///     Format2 - use string.Format with error checking
    ///     Format3 - Replace {x} with my code. Can be used with wildcard
    ///     Format4 - use string.Format without error checking
    ///     Call string.Format, nothing more
    ///     use for special string formatting like {0:X2}
    /// </summary>
    /// <param name="v"></param>
    /// <param name="a"></param>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    public static string Format4(string v, params object[] o)
    {
        //ThrowEx.PassedListInsteadOfArray(nameof(o), o);

        o = CA.ConvertListStringWrappedInArray(o);

        return string.Format(v, o);
    }

    /// <summary>
    ///     Multiline: yes
    /// </summary>
    /// <param name="templateHandler"></param>
    /// <param name="lsf"></param>
    /// <param name="rsf"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string Format5(string templateHandler, string lsf, string rsf, params object[] args)
    {
        //ThrowEx.PassedListInsteadOfArray(nameof(args), args);

        ////args = CA.ConvertListStringWrappedInArray(args);

        // this was original implementation but dont know why isnt used string.format
        for (var i = 0; i < args.Length; i++)
            templateHandler = SHReplace.ReplaceAll2(templateHandler, args[i].ToString(), lsf + i + rsf);


        return templateHandler;
    }
}