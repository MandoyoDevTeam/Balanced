using System;
using System.Linq;
using System.Reflection;

namespace Balanced.Helpers
{
    public class BalancedAttributeHelper
    {

        public static string GetPropertyAttributes(PropertyInfo prop)
        {
            try
            {
                // look for an attribute that takes one constructor argument
                return (from attribData in prop.GetCustomAttributesData() let typeName = attribData.Constructor.DeclaringType.Name where attribData.ConstructorArguments.Count == 1 && (typeName == "JsonProperty" || typeName == "JsonPropertyAttribute") select attribData.ConstructorArguments[0].Value).FirstOrDefault().ToString();
            }
            catch (Exception)
            {
                return prop == null ? String.Empty : prop.Name;
            }

        }

        public static string GetEnumAttributes(MemberInfo[] prop)
        {
            try
            {
                // look for an attribute that takes one constructor argument
                return (from attribData in prop[0].GetCustomAttributesData() let typeName = attribData.Constructor.DeclaringType.Name where attribData.ConstructorArguments.Count == 1 && (typeName == "JsonProperty" || typeName == "JsonPropertyAttribute") select attribData.ConstructorArguments[0].Value).FirstOrDefault().ToString();
            }
            catch (Exception)
            {
                return prop == null ? String.Empty : prop[0].Name;
            }

        }

    }
}
