using System;
using System.Linq;

namespace SpecFlowTests.Utils.Common.Converter
{
    public static class DateConverter
    {
        private static readonly DateTime Today = DateTime.Today;
        private static readonly Random Rng = new Random();
        public const string YesterdayString = "yesterday";
        public const string TodayString = "today";
        public const string TomorrowString = "tomorrow";

        public static DateTime? GetDateFrom(string represenation)
        {
            if (string.IsNullOrWhiteSpace(represenation))
                return null;

            var pattern = represenation.Trim().ToLower();

            switch (pattern)
            {
                case TodayString:
                    return Today;
                case YesterdayString:
                    return Today.AddDays(-1);
                case TomorrowString:
                    return Today.AddDays(1);
                case var dateTimeString when DateTime.TryParse(dateTimeString, out var dateTimeValue):
                    return dateTimeValue;
                case MagicStrings.KeineAngabeSymbol:
                    return null;
                default:
                    throw new NotSupportedException($"pattern '{pattern}' is not supported");
            }
        }

        public static string ReplaceDate(string representation)
        {
            return Replace(representation, YesterdayString, TodayString, TomorrowString);
        }

        private static string Replace(string representation, params string[] patternsToReplace)
        {
            string GetDate(string dateToken)
            {
                return GetDateFrom(dateToken)?.ToString("d");
            }

            string ReplaceDateToken(string current, string dateTokenToReplace)
            {
                return current.Replace($"#{dateTokenToReplace}", GetDate(dateTokenToReplace));
            }

            return patternsToReplace.Aggregate(representation, ReplaceDateToken);
        }

        public static DateTimeOffset? GetDateOffset(string represenation)
        {
            var dateTimeFrom = GetDateFrom(represenation);
            if (dateTimeFrom.HasValue)
            {
                return DateTime.SpecifyKind(dateTimeFrom.Value, DateTimeKind.Local);
            }

            return null;
        }
    }
}
