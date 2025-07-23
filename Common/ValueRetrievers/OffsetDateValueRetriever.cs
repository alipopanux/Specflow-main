using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class OffsetDateValueRetriever : IValueRetriever
    {
        public static string TimesOffsetGroupName = "timesOffset";
        private readonly TimesOffsetType _timesOffsetType;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly bool _futureOffset;
        private readonly Regex _offsetDateRegex;

        /// <summary>
        /// Permet de traduire et de récupérer les chaines de caractères décrivant une date passée ou future. Ex: il y a 30 minutes 
        /// </summary>
        /// <param name="offsetDateRegex">La chaine qui contient la Regex qui décrit l'expression de la date passée ou future. Ex: dans 2 ans</param>
        /// <param name="timesOffsetType">Type de décalage horaire: jour, année, mois, ....etc.</param>
        /// <param name="dateTimeProvider">Fournisseur de DateTime.</param>
        /// <example>
        /// - il y a 25 heures => new OffsetDateValueRetriever($@"il y a (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) heures?", TimesOffsetType.HOUR, new TestDateTimeProvider())
        /// - il y a 60 minutes => new OffsetDateValueRetriever($@"il y a (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) minutes?", TimesOffsetType.MINUTE, new TestDateTimeProvider())
        /// - dans 30 minutes => new OffsetDateValueRetriever($@"dans (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) minutes?", TimesOffsetType.MINUTE, new TestDateTimeProvider(), true)
        /// </example>
        public OffsetDateValueRetriever(
            string offsetDateRegex,
            TimesOffsetType timesOffsetType,
            IDateTimeProvider dateTimeProvider,
            bool isFutureOffset = false)
        {
            _timesOffsetType = timesOffsetType;
            _dateTimeProvider = dateTimeProvider;
            _futureOffset = isFutureOffset;
            _offsetDateRegex = new Regex(offsetDateRegex, RegexOptions.Compiled);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (keyValuePair.Value == null)
                return false;

            var match = _offsetDateRegex.Match(keyValuePair.Value);
            if (!match.Success
                || !match.Groups[TimesOffsetGroupName].Success
                || !int.TryParse(match.Groups[TimesOffsetGroupName].Value, out _))
                return false;

            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return type == typeof(DateTimeOffset) || type == typeof(DateTime) || type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var match = _offsetDateRegex.Match(keyValuePair.Value);
            var timesOffset = int.Parse(match.Groups[TimesOffsetGroupName].Value);
            timesOffset = _futureOffset ? timesOffset : -timesOffset;
            var dateTime = _timesOffsetType switch
            {
                TimesOffsetType.YEAR => _dateTimeProvider.NowOffset.Date.AddYears(timesOffset),
                TimesOffsetType.MONTH => _dateTimeProvider.NowOffset.Date.AddMonths(timesOffset),
                TimesOffsetType.DAY => _dateTimeProvider.NowOffset.Date.AddDays(timesOffset),
                TimesOffsetType.HOUR => _dateTimeProvider.NowOffset.AddHours(timesOffset),
                TimesOffsetType.MINUTE => _dateTimeProvider.NowOffset.AddMinutes(timesOffset),
                TimesOffsetType.SECOND => _dateTimeProvider.NowOffset.AddSeconds(timesOffset),
                _ => throw new ArgumentOutOfRangeException(),
            };

            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            if (type == typeof(DateTime))
                return dateTime.DateTime;

            if (type == typeof(string))
            {
                return dateTime.DateTime.ToString("yyyy/MM/dd");
            }

            return dateTime;
        }
    }

    public enum TimesOffsetType
    {
        YEAR,
        MONTH,
        DAY,
        HOUR,
        MINUTE,
        SECOND
    }
}
