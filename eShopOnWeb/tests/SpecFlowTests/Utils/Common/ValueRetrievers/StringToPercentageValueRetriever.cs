using System;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class StringToPercentageValueRetriever : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(decimal);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (string.IsNullOrWhiteSpace(keyValuePair.Value)) throw new NotSupportedException($"Table row column '{keyValuePair.Key}' does not support empty entry.");
            if (MapDriver.IsNotSet(keyValuePair.Value)) return 0M;

            var isPercentageRepresentation = keyValuePair.Value.Contains("%");
            var stringValue = keyValuePair.Value;

            if (isPercentageRepresentation)
            {
                stringValue = stringValue.Replace("%", "").Trim();
            }

            var value = decimal.Parse(stringValue);

            if (isPercentageRepresentation)
            {
                value *= 0.01M;
            }

            return value;
        }
    }
}