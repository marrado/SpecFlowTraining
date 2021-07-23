using System;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class StringToBoolValueRetriever : IValueRetriever
    {
        public bool GetValue(KeyValuePair<string, string> keyValuePair)
        {
            if (string.IsNullOrWhiteSpace(keyValuePair.Value))
                throw new NotSupportedException($"Table row column '{keyValuePair.Key}' does not support empty entry.");

            return MapDriver.MapToBool(keyValuePair.Value);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetValue(keyValuePair);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(bool);
        }
    }
}
