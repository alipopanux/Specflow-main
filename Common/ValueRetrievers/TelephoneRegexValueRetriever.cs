using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class TelephoneRegexValueRetriever : IValueRetriever
    {
        private readonly string _stringkeyWord;
        private readonly ITelephoneRegexProvider _telephoneRegexValueRetriever;

        public TelephoneRegexValueRetriever(string emptykeyWord, ITelephoneRegexProvider telephoneRegleProvider)
        {
            _stringkeyWord = emptykeyWord;
            _telephoneRegexValueRetriever = telephoneRegleProvider;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _stringkeyWord && type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (propertyType == typeof(string))
                return _telephoneRegexValueRetriever.GetRegex();

            return Activator.CreateInstance(propertyType);
        }
    }
}
