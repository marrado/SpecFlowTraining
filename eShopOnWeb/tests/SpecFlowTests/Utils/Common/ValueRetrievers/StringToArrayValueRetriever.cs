using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    public class StringToEnumerableEnumValueRetriever : IValueRetriever
    {
        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetValue(keyValuePair.Value, propertyType);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetEnumerableTypes(propertyType).Any(t => t.IsEnum);
        }

        private static object GetValue(string value, Type enumType)
        {
            var values = value.Split(';');
            var enumerable = values.Where(v => !v.Trim().Equals("-")).Select(v =>
            {
                CheckIfValueIsOfEnum(v, enumType);
                return ConvertToEnum(v, enumType);
            }).ToList();
            return ConvertList(enumerable, enumType);
        }

        private static object ConvertList(List<object> items, Type type, bool performConversion = false)
        {
            var containedType = type.GenericTypeArguments.First();
            var enumerableType = typeof(Enumerable);
            var castMethod = enumerableType.GetMethod(nameof(Enumerable.Cast)).MakeGenericMethod(containedType);
            var toListMethod = enumerableType.GetMethod(nameof(Enumerable.ToList)).MakeGenericMethod(containedType);

            IEnumerable<object> itemsToCast;

            if (performConversion)
            {
                itemsToCast = items.Select(item => Convert.ChangeType(item, containedType));
            }
            else
            {
                itemsToCast = items;
            }

            var castedItems = castMethod.Invoke(null, new[] { itemsToCast });

            return toListMethod.Invoke(null, new[] { castedItems });
        }

        private static object ConvertToEnum(string value, Type enumType)
        {
            foreach (var field in enumType.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.Name == value)
                    {
                        return field.GetValue(null);
                    }
                }
            }

            return Enum.Parse(GetTheEnumType(enumType), ParseTheValue(value), true);
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

        private static Type GetTheEnumType(Type enumType)
        {
            return !IsEnumNullable(enumType) ? enumType : enumType.GetGenericArguments()[0];
        }

        private static void CheckIfValueIsOfEnum(string value, Type enumType)
        {
            if (!IsEnumNullable(enumType) && string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException("Null or empty values cannot be parsed to an enum");
            }

            try
            {
                ConvertToEnum(value, enumType);
            }
            catch
            {
                throw new InvalidOperationException($"No enum with value {value} found");
            }
        }

        private static bool IsEnumNullable(Type enumType)
        {
            return enumType.IsGenericType;
        }

        private static string ParseTheValue(string value)
        {
            value = value.Replace(" ", "");
            value = value.Replace("ä", "ae");
            value = value.Replace("ö", "oe");
            value = value.Replace("ü", "ue");
            value = value.Replace("Ä", "Ae");
            value = value.Replace("Ö", "Oe");
            value = value.Replace("Ü", "Ue");
            value = value.Replace("ß", "ss");
            return value;
        }
    }
}
