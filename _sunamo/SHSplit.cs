using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoStringFormat._sunamo;

internal class SHSplit
{
    internal static List<string> SplitMore(string item, params string[] space)
    {
        return item.Split(space, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitCharMore(string v1, params char[] v2)
    {
        return v1.Split(v2).ToList();
    }
}
