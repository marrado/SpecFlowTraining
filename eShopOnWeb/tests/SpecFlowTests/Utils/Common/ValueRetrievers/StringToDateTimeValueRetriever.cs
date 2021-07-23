using System;
using System.Collections.Generic;
using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class StringToDateTimeValueRetriever : IValueRetriever
    {
        private static DateTime GetValue(KeyValuePair<string, string> keyValuePair)
        {
            return DateConverter.GetDateFrom(keyValuePair.Value) ?? default;
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetValue(keyValuePair);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(DateTime);
        }
    }
}
