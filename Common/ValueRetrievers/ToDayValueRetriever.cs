using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class ToDayValueRetriever : IValueRetriever
    {
        private readonly string _nowKeyWord;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ToDayValueRetriever(string nowKeyWord, IDateTimeProvider dateTimeProvider)
        {
            _nowKeyWord = nowKeyWord;
            _dateTimeProvider = dateTimeProvider;
        }
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _nowKeyWord && (type == typeof(DateTimeOffset) || type == typeof(DateTime));
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            if (type == typeof(DateTime))
                return _dateTimeProvider.Now.Date;

            return new DateTimeOffset(_dateTimeProvider.NowOffset.Date);
        }
    }
}
