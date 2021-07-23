using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecFlowTests.Utils.Common.Converter
{
    public static class BoolConverter
    {
        internal static bool MapToBool(string sourceValue)
        {
            bool AnyMatch(IEnumerable<string> values, string valueToMatch)
            {
                return values.Any(v => valueToMatch.Equals(v, StringComparison.InvariantCultureIgnoreCase));
            }

            switch (sourceValue)
            {
                case var value when AnyMatch(MagicStrings.TrueValues, value):
                    return true;
                case var value when AnyMatch(MagicStrings.FalseValues, value):
                    return false;
                default:
                    throw new NotSupportedException($"Value '{sourceValue}' is not supported to be convertable to bool.");
            }
        }
    }
}
