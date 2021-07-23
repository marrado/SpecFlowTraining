using System;
using System.ComponentModel.DataAnnotations;
using TechTalk.SpecFlow.Bindings;

namespace SpecFlowTests.Utils.Common.Converter
{
    internal static class EnumConverter
    {
        internal static bool IsEnum(Type type)
        {
            return IsNullableEnum(type) ? typeof(Enum).IsAssignableFrom(type.GetGenericArguments()[0]) : type.IsEnum;
        }

        internal static Enum ConvertToEnum(string value, Type enumType)
        {
            var enumTypeInternal = GetEnumTypeFromNullableEnum(enumType);

            switch (enumTypeInternal)
            {
                case var type when TryGetValueFromDisplayNameAttribute(value, type, out var result): return result;
                default:
                    return (Enum)StepArgumentTypeConverter.ConvertToAnEnum(enumTypeInternal, value);
            }
        }

        private static Type GetEnumTypeFromNullableEnum(Type enumType)
        {
            return !IsNotNullableEnum(enumType) ? enumType.GetGenericArguments()[0] : enumType;
        }


        private static bool IsNullableEnum(Type type)
        {
            return type.IsGenericType
                   && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                   && typeof(Enum).IsAssignableFrom(type.GetGenericArguments()[0]);
        }

        internal static bool IsNotNullableEnum(Type type)
        {
            return !type.IsGenericType;
        }

        private static bool TryGetValueFromDisplayNameAttribute(string name, Type type, out Enum value)
        {
            if (!type.IsEnum)
            {
                throw new InvalidOperationException("Provided type is not of type 'Enum'");
            }

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute && string.Equals(attribute.Name, name, StringComparison.InvariantCultureIgnoreCase))
                {
                    value = (Enum)field.GetValue(null);
                    return true;
                }

                if (string.Equals(field.Name, name, StringComparison.InvariantCultureIgnoreCase))
                {
                    value = (Enum)field.GetValue(null);
                    return true;
                }
            }

            value = null;
            return false;
        }
    }
}
