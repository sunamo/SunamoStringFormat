
namespace SunamoStringFormat._sunamo;
using System;
using System.Collections.Generic;
using System.Linq;

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