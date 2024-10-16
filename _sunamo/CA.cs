namespace SunamoStringFormat._sunamo;

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

    internal static bool IsListStringWrappedInArray<T>(List<T> v2)
    {
        var first = v2.First().ToString();
        if (v2.Count == 1 && (first == "System.Collections.Generic.List`1[System.String]" ||
                              first == "System.Collections.Generic.List`1[System.Object]")) return true;
        return false;
    }


    internal static void InitFillWith(List<string> datas, int pocet, string initWith = "")
    {
        InitFillWith<string>(datas, pocet, initWith);
    }
    internal static void InitFillWith<T>(List<T> datas, int pocet, T initWith)
    {
        for (int i = 0; i < pocet; i++)
        {
            datas.Add(initWith);
        }
    }
    internal static void InitFillWith<T>(List<T> arr, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            arr.Add(default);
        }
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


}