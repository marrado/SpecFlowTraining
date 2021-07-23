using System;
using System.Collections.Generic;
using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class StringToEnumValueRetriever : IValueRetriever
    {
        public object GetValue(KeyValuePair<string, string> keyValuePair, Type enumType)
        {
            if (EnumConverter.IsNotNullableEnum(enumType) && string.IsNullOrWhiteSpace(keyValuePair.Value))
                throw new NotSupportedException($"Table row column '{keyValuePair.Key}' does not support empty entries.");

            try
            {
                return MapDriver.MapToEnum(keyValuePair.Value, enumType);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"No enum with value '{keyValuePair.Value}' found for Table row column '{keyValuePair.Key}'.", exception);
            }
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetValue(keyValuePair, propertyType);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return EnumConverter.IsEnum(propertyType);
        }
    }
}
