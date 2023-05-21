using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSSLibrary.FileReader
{
    internal static class MyStringExtension
    {
        public static bool CompareAlphaNumericOnly(this string text1, string text2, bool isCaseSensitive)
        {
            //Removing Symbols and Spaces
            text1 = text1.GetOnlyAlphaNumeric();
            text2 = text2.GetOnlyAlphaNumeric();

            //Convert to All Lowercase
            if (!isCaseSensitive)
            {
                text1 = text1.ToLower();
                text2 = text2.ToLower();
            }

            if (text1 == text2)
                return true;

            return false;
        }

        public static string GetOnlyAlphaNumeric(this string text)
        {
            if (text == null) return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }

 
    }

}
