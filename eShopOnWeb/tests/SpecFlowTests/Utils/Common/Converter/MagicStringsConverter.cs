using System.Text.RegularExpressions;

namespace SpecFlowTests.Utils.Common.Converter
{
    internal static class MagicStringsConverter
    {
        public static string ReplaceMagicStrings(this string value)
        {
            if (MapDriver.IsNotSet(value)) return null;

            var result = ReplaceMagicStringOfLength(value);
            result = DateConverter.ReplaceDate(result);
            
            return result;
        }

        private static string ReplaceMagicStringOfLength(string originalString)
        {
            var replacedString = originalString;

            var matches = Regex.Matches(originalString, MagicStrings.StringsOfLengthPattern);
            foreach (Match match in matches)
            {
                if (match.Groups.Count >= 2 && int.TryParse(match.Groups[1].Value, out var length))
                {
                    replacedString = replacedString.Replace(match.Groups[0].Value, new string('X', length));
                }
            }

            return replacedString;
        }
    }
}
