using System;
using System.Text;

namespace Wekan.SDK.Utils
{
    public static class StringArrayExtensions
    {
        public static string JoinToString (this string[] array)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendJoin(",", array);
            return stringBuilder.ToString();
        }
    }
}
