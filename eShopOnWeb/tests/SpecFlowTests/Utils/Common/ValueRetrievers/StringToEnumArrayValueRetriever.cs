using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class StringToEnumerableValueRetriever<T> : IValueRetriever
    {
        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetValue(keyValuePair.Value);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetEnumerableTypes(propertyType).Any(t => t == typeof(T));
        }

        private static IEnumerable<T> GetValue(string value)
        {
            var values = value.Split(';');
            var enumerable = values.Where(v => MapDriver.IsSet(v.Trim())).Select(ChangeType).ToList();
            return enumerable.Any() ? enumerable : null;
        }

        private static T ChangeType(string valueToBeParsed)
        {
            return (T)Convert.ChangeType(valueToBeParsed, typeof(T));
        }

        private static IEnumerable<Type> GetEnumerableTypes(Type type)
        {
            if (type.IsInterface)
            {
                if (type.IsGenericType
                    && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    yield return type.GetGenericArguments()[0];
                }
            }
            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    yield return intType.GetGenericArguments()[0];
                }
            }
        }
    }
}
