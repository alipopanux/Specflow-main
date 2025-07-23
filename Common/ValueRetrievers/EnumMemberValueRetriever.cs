using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class EnumMemberValueRetriever : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType.IsEnum || propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && propertyType.GetGenericArguments()[0].IsEnum;
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var value = keyValuePair.Value;

            if (propertyType.IsEnum)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("No enum with value {null} found");
                }

                if (value.Length == 0)
                {
                    throw new InvalidOperationException("No enum with value {empty} found");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }

                propertyType = propertyType.GetGenericArguments()[0];
            }

            try
            {
                return ConvertToAnEnum(propertyType, value);
            }
            catch
            {
                throw new InvalidOperationException($"No enum with value {value} found");
            }
        }

        private static object ConvertToAnEnum(Type enumType, string value)
        {
            return JsonConvert.DeserializeObject($"\"{value.Trim()}\"", enumType, new StringEnumConverter());
        }
    }
}
