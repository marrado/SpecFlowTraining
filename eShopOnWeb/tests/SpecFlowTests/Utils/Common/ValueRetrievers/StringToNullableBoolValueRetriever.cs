using System;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class StringToNullableBoolValueRetriever : IValueRetriever
    {
        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return MapDriver.IsNotSet(keyValuePair.Value) ? null : (bool?)MapDriver.MapToBool(keyValuePair.Value);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(bool?);
        }
    }
}
