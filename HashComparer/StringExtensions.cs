using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HashComparer
{
    public static class StringExtensions
    {
        public static string RemoveSpecialCharactersAndWhiteSpace(this string str)
        {
            return Regex.Replace(str, @"[^\w\d]", "");
        }
    }
}
