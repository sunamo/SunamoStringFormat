namespace SunamoStringFormat._sunamo.SunamoExceptions;

/// <summary>
/// Provides utility methods for exception formatting and stack trace analysis.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// Formats the prefix string by appending a colon and space if not empty.
    /// </summary>
    /// <param name="prefix">The prefix to format.</param>
    /// <returns>The formatted prefix with colon separator, or empty string if prefix is null/whitespace.</returns>
    internal static string CheckBefore(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }

    /// <summary>
    /// Builds a text representation of an exception, optionally including inner exceptions.
    /// </summary>
    /// <param name="exception">The exception to format.</param>
    /// <param name="isIncludingInner">Whether to include inner exception messages.</param>
    /// <returns>A string containing the exception message chain.</returns>
    internal static string TextOfExceptions(Exception exception, bool isIncludingInner = true)
    {
        if (exception == null) return string.Empty;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Exception:");
        stringBuilder.AppendLine(exception.Message);
        if (isIncludingInner)
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                stringBuilder.AppendLine(exception.Message);
            }
        var result = stringBuilder.ToString();
        return result;
    }

    /// <summary>
    /// Extracts the type name, method name, and full stack trace from the current execution context.
    /// </summary>
    /// <param name="isFillingFirstTwo">Whether to fill the type and method name from the first non-ThrowEx frame.</param>
    /// <returns>A tuple containing (typeName, methodName, stackTraceText).</returns>
    internal static Tuple<string, string, string> PlaceOfException(
bool isFillingFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var line = lines[i];
            if (isFillingFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    isFillingFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Parses a stack trace line to extract the type name and method name.
    /// </summary>
    /// <param name="stackTraceLine">A single stack trace line to parse.</param>
    /// <param name="type">The extracted type name.</param>
    /// <param name="methodName">The extracted method name.</param>
    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var methodSignature = stackTraceLine.Split("at ")[1].Trim();
        var fullyQualifiedName = methodSignature.Split("(")[0];
        var parts = fullyQualifiedName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified stack frame depth.
    /// </summary>
    /// <param name="depth">The stack frame depth to inspect.</param>
    /// <returns>The name of the calling method.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region OnlyReturnString
    /// <summary>
    /// Creates a formatted exception message with a prefix.
    /// </summary>
    /// <param name="prefix">The prefix to prepend to the message.</param>
    /// <param name="message">The exception message.</param>
    /// <returns>The formatted exception message string.</returns>
    internal static string? Custom(string prefix, string message)
    {
        return CheckBefore(prefix) + message;
    }

    /// <summary>
    /// Creates a formatted exception message that includes exception details.
    /// </summary>
    /// <param name="prefix">The prefix to prepend to the message.</param>
    /// <param name="exception">The exception to include.</param>
    /// <param name="message">The additional message text.</param>
    /// <returns>The formatted exception message string with exception details.</returns>
    internal static string? ExcAsArg(string prefix, Exception exception, string message)
    {
        return CheckBefore(prefix) + message + string.Empty + TextOfExceptions(exception);
    }
    #endregion
}
