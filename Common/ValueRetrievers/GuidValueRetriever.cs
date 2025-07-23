using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class GuidValueRetriever : IValueRetriever
    {
        private readonly string _stringkeyWord;
        private readonly IGuidProvider _guidProvider;

        public GuidValueRetriever(string emptykeyWord, IGuidProvider guidProvider)
        {
            _stringkeyWord = emptykeyWord;
            _guidProvider = guidProvider;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _stringkeyWord && type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (propertyType == typeof(string))
                return _guidProvider.NewGuid();

            return Activator.CreateInstance(propertyType);
        }
    }
}
