using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Deza.Stone.Utilities
{
    public static class EnumUtils
    {
        public static int GetEnumNumber<T>(T enumValue)
        {
            return Convert.ToInt32(Convert.ChangeType(enumValue, ((Enum)(object)enumValue).GetTypeCode()));
        }

        public static T GetEnum<T>(int enumNumber)
        {
            Type enumType = typeof(T);

            Enum value = (Enum)Enum.ToObject(enumType, enumNumber);
            if (Enum.IsDefined(enumType, value) == false)
            {
                throw new NotSupportedException("Unable to convert value from database to the type: " + enumType.ToString());
            }

            return (T)(object)value;
        }

        public static T GetEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetDescription<T>(T value)
        {
            var enumType = typeof(T);

            var memberInfo = enumType.GetMember(value.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attribute = memberInfo[0].GetCustomAttribute(typeof(DescriptionAttribute), false);

                if (attribute != null)
                {
                    return ((DescriptionAttribute)attribute).Description;
                }
            }

            return null;
        }

        public static string GetDescriptionById<T>(int enumNumber)
        {
            var enumEntity = GetEnum<T>(enumNumber);

            return GetDescription<T>(enumEntity);
        }

    }
}
