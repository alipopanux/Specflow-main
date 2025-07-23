using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class RaisonSocialeRegexValueRetriever : IValueRetriever
    {
        private readonly string _stringkeyWord;
        private readonly IRaisonSocialeRegleProvider _raisonSocialeRegexValueRetriever;

        public RaisonSocialeRegexValueRetriever(string emptykeyWord, IRaisonSocialeRegleProvider raisonSocialeRegexValueRetriever)
        {
            _stringkeyWord = emptykeyWord;
            _raisonSocialeRegexValueRetriever = raisonSocialeRegexValueRetriever;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _stringkeyWord && type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (propertyType == typeof(string))
                return _raisonSocialeRegexValueRetriever.GetRegex();

            return Activator.CreateInstance(propertyType);
        }
    }
}
