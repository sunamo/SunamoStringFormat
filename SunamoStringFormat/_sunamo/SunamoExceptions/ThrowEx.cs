namespace SunamoStringFormat._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods for throwing and handling exceptions with contextual information.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws or logs a custom exception built from the given exception's text.
    /// </summary>
    /// <param name="exception">The exception to process.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception or just return the result.</param>
    /// <returns><c>true</c> if an exception was detected; otherwise, <c>false</c>.</returns>
    internal static bool Custom(Exception exception, bool isReallyThrowing = true)
    { return Custom(Exceptions.TextOfExceptions(exception), isReallyThrowing); }

    /// <summary>
    /// Throws or logs a custom exception with the given message.
    /// </summary>
    /// <param name="message">The primary exception message.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception or just return the result.</param>
    /// <param name="secondMessage">An optional additional message to append.</param>
    /// <returns><c>true</c> if an exception was detected; otherwise, <c>false</c>.</returns>
    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exceptionText = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exceptionText, isReallyThrowing);
    }

    /// <summary>
    /// Throws or logs an exception using the exception-as-argument pattern.
    /// </summary>
    /// <param name="exception">The exception to process.</param>
    /// <param name="message">An optional additional message.</param>
    /// <returns><c>true</c> if an exception was detected; otherwise, <c>false</c>.</returns>
    internal static bool ExcAsArg(Exception exception, string message = "")
    { return ThrowIsNotNull(Exceptions.ExcAsArg, exception, message); }

    #region Other
    /// <summary>
    /// Gets the fully qualified name of the currently executing code location.
    /// </summary>
    /// <returns>A string in the format "TypeName.MethodName".</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Gets the fully qualified name of the executed code from the given type and method information.
    /// </summary>
    /// <param name="type">The type object, which can be a Type, MethodBase, or string.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="isFromThrowEx">Whether this call originates from ThrowEx, affecting stack depth calculation.</param>
    /// <returns>A string in the format "TypeName.MethodName".</returns>
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

    /// <summary>
    /// Throws an exception if the exception text is not null, or returns whether an exception was present.
    /// </summary>
    /// <param name="exceptionText">The exception text to evaluate.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception.</param>
    /// <returns><c>true</c> if exception text was not null; otherwise, <c>false</c>.</returns>
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
    /// <summary>
    /// Evaluates the formatter function with the current execution context and throws if the result is not null.
    /// </summary>
    /// <typeparam name="TFirst">The type of the first argument.</typeparam>
    /// <typeparam name="TSecond">The type of the second argument.</typeparam>
    /// <param name="formatter">The function that formats the exception message.</param>
    /// <param name="exception">The first argument to pass to the formatter.</param>
    /// <param name="message">The second argument to pass to the formatter.</param>
    /// <returns><c>true</c> if the formatter returned a non-null result; otherwise, <c>false</c>.</returns>
    internal static bool ThrowIsNotNull<TFirst, TSecond>(Func<string, TFirst, TSecond, string?> formatter, TFirst exception, TSecond message)
    {
        string? exceptionText = formatter(FullNameOfExecutedCode(), exception, message);
        return ThrowIsNotNull(exceptionText);
    }

    #endregion
    #endregion
}
