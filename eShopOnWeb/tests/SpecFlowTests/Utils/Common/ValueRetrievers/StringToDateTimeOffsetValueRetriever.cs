using System;
using System.Collections.Generic;
using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class StringToDateTimeOffsetValueRetriever : IValueRetriever
    {
        private static DateTimeOffset GetValue(KeyValuePair<string, string> keyValuePair)
        {
            return MapDriver.IsNotSet(keyValuePair.Value) ? default : DateConverter.GetDateOffset(keyValuePair.Value).Value;
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetValue(keyValuePair);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(DateTimeOffset);
        }
    }
}
