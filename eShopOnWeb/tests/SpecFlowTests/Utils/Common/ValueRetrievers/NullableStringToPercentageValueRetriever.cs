using System;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class NullableStringToPercentageValueRetriever : IValueRetriever
    {
        private readonly StringToPercentageValueRetriever _retriever = new StringToPercentageValueRetriever();

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(decimal?);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return MapDriver.IsNotSet(keyValuePair.Value) 
                ? null 
                : _retriever.Retrieve(keyValuePair, targetType, propertyType);
        }
    }
}