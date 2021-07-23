using System;
using System.Collections.Generic;
using System.Linq;
using SpecFlowTests.Utils.Common.Converter;
using EnumConverter = SpecFlowTests.Utils.Common.Converter.EnumConverter;

namespace SpecFlowTests.Utils.Common
{
    internal static class MapDriver
    {
        internal static bool IsNotSet(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                   || value.Trim() == MagicStrings.KeineAngabeSymbol
                   || value.Trim().ToLower() == MagicStrings.KeineAngabe
                   || value.Trim() == MagicStrings.ExplicitNull;
        }

        internal static bool IsSet(string value)
        {
            return !IsNotSet(value);
        }

        internal static bool MapToBool(string sourceValue)
        {
            return BoolConverter.MapToBool(sourceValue);
        }

        public static Enum MapToEnum(string value, Type enumType)
        {
            return IsNotSet(value)
                ? null
                : EnumConverter.ConvertToEnum(value, enumType);
        }

        internal static IEnumerable<string> MapSingleRowCellEntryToMultiple(string sourceValue)
        {
            IEnumerable<string> ParseValues(string source)
            {
                return source.Split(new[] { MagicStrings.MultiValueSeperator }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()).Where(i => !IsNotSet(i));
            }

            return IsNotSet(sourceValue) ? Array.Empty<string>() : ParseValues(sourceValue);
        }
    }
}
