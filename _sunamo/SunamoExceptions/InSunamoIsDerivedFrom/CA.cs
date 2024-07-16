namespace SunamoStringFormat._sunamo.SunamoExceptions.InSunamoIsDerivedFrom;

internal class CA
{
    internal static Object[] ConvertListStringWrappedInArray(Object[] innerMain)
    {
        if (CA.IsListStringWrappedInArray(innerMain))
        {
            List<object> result = null;
            var first = (IEnumerable)innerMain[0];
            if (first is List<object>)
            {
                result = (List<object>)first;
            }
            else
            {
                result = new List<object>();
                foreach (var item in first)
                {
                    result.Add(item);
                }
            }
            return result.ToArray();
        }
        return innerMain;
    }

    internal static bool IsListStringWrappedInArray(IEnumerable v2)
    {
        var c = 0;
        object first = null;
        foreach (var item in v2)
        {
            if (c == 0)
            {
                first = item;
            }

            c++;
        }


        if (c == 1 && (first == "System.Collections.Generic.List`1[System.String]" ||
        first == "System.Collections.Generic.List`1[System.Object]")) return true;
        return false;
    }

    internal static bool IsListStringWrappedInArray<T>(List<T> v2)
    {
        var first = v2.First().ToString();
        if (v2.Count == 1 && (first == "System.Collections.Generic.List`1[System.String]" ||
        first == "System.Collections.Generic.List`1[System.Object]")) return true;

        return false;
    }
}
