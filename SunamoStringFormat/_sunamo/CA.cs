namespace SunamoStringFormat._sunamo;

/// <summary>
/// Provides collection and array conversion utility methods.
/// </summary>
internal class CA
{
    /// <summary>
    /// Converts a list of strings wrapped inside a single-element object array back to a flat object array.
    /// </summary>
    /// <param name="array">The object array that may contain a wrapped list.</param>
    /// <returns>A flat object array with unwrapped elements, or the original array if no wrapping was detected.</returns>
    internal static object[] ConvertListStringWrappedInArray(object[] array)
    {
        if (CA.IsListStringWrappedInArray(array))
        {
            List<object>? result = null;
            var firstElement = (IEnumerable)array[0];
            if (firstElement is List<object> objectList)
            {
                result = objectList;
            }
            else
            {
                result = new List<object>();
                foreach (var item in firstElement)
                {
                    result.Add(item);
                }
            }
            return result.ToArray();
        }
        return array;
    }

    /// <summary>
    /// Determines whether the given enumerable contains exactly one element that represents a wrapped list of strings or objects.
    /// </summary>
    /// <param name="enumerable">The enumerable to check.</param>
    /// <returns><c>true</c> if the enumerable contains a single element representing a wrapped list; otherwise, <c>false</c>.</returns>
    internal static bool IsListStringWrappedInArray(IEnumerable enumerable)
    {
        var count = 0;
        object? firstElement = null;
        foreach (var item in enumerable)
        {
            if (count == 0)
            {
                firstElement = item;
            }

            count++;
        }

        if (count == 1 && firstElement != null &&
            (firstElement.ToString() == "System.Collections.Generic.List`1[System.String]" ||
             firstElement.ToString() == "System.Collections.Generic.List`1[System.Object]")) return true;
        return false;
    }
}
