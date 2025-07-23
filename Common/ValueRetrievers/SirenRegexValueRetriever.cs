using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class SirenRegexValueRetriever : IValueRetriever
    {
        private readonly string _stringkeyWord;
        private readonly ISirenRegexProvider _sirenRegleProvider;

        public SirenRegexValueRetriever(string emptykeyWord, ISirenRegexProvider sirenRegleProvider)
        {
            _stringkeyWord = emptykeyWord;
            _sirenRegleProvider = sirenRegleProvider;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _stringkeyWord && type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (propertyType == typeof(string))
                return _sirenRegleProvider.GetRegex();

            return Activator.CreateInstance(propertyType);
        }
    }
}
