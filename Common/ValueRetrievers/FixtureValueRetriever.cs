using AutoFixture;
using Lopcommerce.Regles.WebAPI.Tests.Helpers;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class FixtureValueRetriever : IValueRetriever
    {
        private readonly Regex _regex;
        private readonly Fixture _fixture = new Fixture();

        public FixtureValueRetriever(string valueForFixture)
        {
            _regex = new Regex(valueForFixture + @"(?<array>\[(?<index>\d+)\])?", RegexOptions.Compiled);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (keyValuePair.Value == null)
                return false;

            var match = _regex.Match(keyValuePair.Value);
            if (match.Success && !match.Groups["index"].Success)
                return true;

            if (match.Success && match.Groups["index"].Success)
                return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

            return false;
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var match = _regex.Match(keyValuePair.Value);

            if (!match.Groups["index"].Success)
                return _fixture.Create(propertyType);

            var arrayLength = int.Parse(match.Groups["index"].Value);
            var arrayFixture = new Fixture() { RepeatCount = arrayLength };
            var genericType = propertyType.GetGenericArguments().First();

            return arrayFixture.CreateMany(genericType);
        }
    }
}
