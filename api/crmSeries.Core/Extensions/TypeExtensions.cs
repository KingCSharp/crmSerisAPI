using System;
using System.Linq;
using System.Reflection;

namespace crmSeries.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool ContainsAttribute(this Type type, Type attributeType)
        {
            var customAttribute = type.GetTypeInfo().GetCustomAttribute(attributeType);

            return customAttribute != null;
        }

        public static T GetPropertyAttribute<T>(this Type type, string propertyName)
            where T : Attribute
        {
            var property = type.GetProperty(propertyName);

            return property == null
                ? null
                : (T) property.GetCustomAttribute(typeof(T), false);
        }

        public static string GetPrettyName(this Type type)
        {
            var prettyName = type.Name;
            if (type.GetTypeInfo().IsGenericType)
            {
                var backtick = prettyName.IndexOf('`');
                if (backtick > 0)
                {
                    prettyName = prettyName.Remove(backtick);
                }

                prettyName += "<";
                var typeParameters = type.GetGenericArguments();
                for (var i = 0; i < typeParameters.Length; i++)
                {
                    var typeParamName = GetPrettyName(typeParameters[i]);
                    prettyName += (i == 0 ? typeParamName : "," + typeParamName);
                }

                prettyName += ">";
            }
            else
            {
                prettyName = type.FullName.Split('.').Reverse().First();
            }

            return prettyName.Replace('+', '.');
        }
    }
}