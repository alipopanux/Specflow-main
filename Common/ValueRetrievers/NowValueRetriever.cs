using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class NowValueRetriever : IValueRetriever
    {
        private readonly string _nowKeyWord;
        private readonly IDateTimeProvider _dateTimeProvider;

        public NowValueRetriever(string nowKeyWord, IDateTimeProvider dateTimeProvider)
        {
            _nowKeyWord = nowKeyWord;
            _dateTimeProvider = dateTimeProvider;
        }
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _nowKeyWord && (type == typeof(DateTimeOffset) || type == typeof(DateTime) || type == typeof(string));
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            if (type == typeof(DateTime))
                return _dateTimeProvider.Now;

            if (type == typeof(string))
                return _dateTimeProvider.Now.ToString("yyyy/MM/dd");

            return _dateTimeProvider.NowOffset;
        }
    }
}
