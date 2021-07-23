using System;

namespace SpecFlowTests.Utils.Common.Converter
{
    internal static class MagicStrings
    {
        internal const char MultiValueSeperator = ',';

        internal static readonly string[] TrueValues = { "x", "true", "ja", "verfügbar", "inkludiert" };

        internal static readonly string[] FalseValues = { "-", "false", "nein", "nicht verfügbar" };

        internal const string KeineAngabeSymbol = "-";
        internal const string KeineAngabe = "keine angabe";
        internal const string ExplicitNull = "NULL";

        internal const string StringsOfLengthPattern = "#([1-9]{1}[0-9]*)[ ]{0,1}(z|Z){1}eichen";

        internal static Guid NotExistentId => RandomGuid;

        internal static DateTime TimeStamp { get; } = RandomDateTimeWithMinValue(new DateTime(1960, 1, 1));

        private static int RandomInt => RandomGuid.GetHashCode();

        private static Guid RandomGuid => Guid.NewGuid();

        private static DateTime RandomDateTimeWithMinValue(DateTime minValue)
        {
            var randomValue = RandomInt;
            return minValue.AddSeconds(randomValue < 0 ? -randomValue : randomValue);
        }
    }
}