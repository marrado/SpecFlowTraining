using System;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class NullableIntValueRetriever : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(int?);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return MapDriver.IsNotSet(keyValuePair.Value)
                       ? (int?)null
                       : int.Parse(keyValuePair.Value.Replace(".", "").Replace(" ", ""));
        }
    }
}
