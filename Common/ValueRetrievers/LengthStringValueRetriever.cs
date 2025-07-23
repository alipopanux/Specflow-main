using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class LengthStringValueRetriever : IValueRetriever
    {
        public static string OccurenceGroupName = "occurence";
        private readonly Regex _occurenceRegex;
        private readonly IStringProvider _stringProvider;

        public LengthStringValueRetriever(string occurenceRegex, IStringProvider stringProvider)
        {
            _occurenceRegex = new Regex(occurenceRegex, RegexOptions.Compiled);
            _stringProvider = stringProvider;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (keyValuePair.Value == null)
                return false;

            var match = _occurenceRegex.Match(keyValuePair.Value);
            if (!match.Success
                || !match.Groups[OccurenceGroupName].Success
                || !int.TryParse(match.Groups[OccurenceGroupName].Value, out _))
                return false;

            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var match = _occurenceRegex.Match(keyValuePair.Value);
            var occurence = int.Parse(match.Groups[OccurenceGroupName].Value);

            if (propertyType == typeof(string))
                return _stringProvider.GetRandomString(occurence);

            return Activator.CreateInstance(propertyType);
        }
    }
}
