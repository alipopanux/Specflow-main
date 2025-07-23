using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class EmptyValueRetriever : IValueRetriever
    {
        private readonly string _emptykeyWord;

        public EmptyValueRetriever(string emptykeyWord)
        {
            _emptykeyWord = emptykeyWord;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return keyValuePair.Value == _emptykeyWord;
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (propertyType == typeof(string))
                return string.Empty;

            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var genericType = propertyType.GetGenericArguments().First();
                var instance = Array.CreateInstance(genericType, 0);
                return instance;
            }

            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                var type = typeof(Dictionary<,>).MakeGenericType(propertyType.GetGenericArguments());
                return Activator.CreateInstance(type);
            }

            return Activator.CreateInstance(propertyType);
        }
    }
}
