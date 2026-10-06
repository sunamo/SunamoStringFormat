namespace SunamoStringFormat._sunamo;

internal class CA
{
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
