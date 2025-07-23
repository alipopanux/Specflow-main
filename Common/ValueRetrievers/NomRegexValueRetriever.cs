using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{ 
    public class NomRegexValueRetriever : IValueRetriever
    {
        private readonly string _stringkeyWord;
        private readonly INomRegexProvider _nomRegleProvider;

        public NomRegexValueRetriever(string emptykeyWord, INomRegexProvider nomRegleProvider)
        {
            _stringkeyWord = emptykeyWord;
            _nomRegleProvider = nomRegleProvider;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _stringkeyWord && type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (propertyType == typeof(string))
                return _nomRegleProvider.GetRegex();

            return Activator.CreateInstance(propertyType);
        }
    }
}
