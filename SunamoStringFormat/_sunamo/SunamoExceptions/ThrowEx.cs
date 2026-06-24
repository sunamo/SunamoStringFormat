namespace SunamoStringFormat._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    internal static bool Custom(Exception exception, bool isReallyThrowing = true)
    { return Custom(Exceptions.TextOfExceptions(exception), isReallyThrowing); }

    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exceptionText = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exceptionText, isReallyThrowing);
    }

    internal static bool ExcAsArg(Exception exception, string message = "")
    { return ThrowIsNotNull(Exceptions.ExcAsArg, exception, message); }

    #region Other
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type matchedType)
        {
            typeFullName = matchedType.FullName ?? "Type cannot be get via type is Type matchedType";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type runtimeType = type.GetType();
            typeFullName = runtimeType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    internal static bool ThrowIsNotNull(string? exceptionText, bool isReallyThrowing = true)
    {
        if (exceptionText != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exceptionText);
            }
            return true;
        }
        return false;
    }

    #region For avoid FullNameOfExecutedCode
    internal static bool ThrowIsNotNull<TFirst, TSecond>(Func<string, TFirst, TSecond, string?> formatter, TFirst exception, TSecond message)
    {
        string? exceptionText = formatter(FullNameOfExecutedCode(), exception, message);
        return ThrowIsNotNull(exceptionText);
    }

    #endregion
    #endregion
}
