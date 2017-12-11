using System;
using System.Collections.Generic;
using System.Text;

namespace EpIAA
{
    public static class ExtensionsMethods
    {
        public static void Pop<T>(this IList<T> x)
        {
            x.RemoveAt(x.Count - 1);
        }
    }
}
