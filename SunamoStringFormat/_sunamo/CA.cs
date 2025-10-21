// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

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






    internal static bool IsListStringWrappedInArray(IEnumerable v2)
    {
        var count = 0;
        object first = null;
        foreach (var item in v2)
        {
            if (count == 0)
            {
                first = item;
            }

            count++;
        }


        if (count == 1 && (first == "System.Collections.Generic.List`1[System.String]" ||
                       first == "System.Collections.Generic.List`1[System.Object]")) return true;
        return false;
    }


}