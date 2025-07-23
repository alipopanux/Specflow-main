using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class OffsetDateValueThreeCombinedRetriever : IValueRetriever
    {
        public static string TimesOffsetGroupName1 = "timesOffset1";
        public static string TimesOffsetGroupName2 = "timesOffset2";
        public static string TimesOffsetGroupName3 = "timesOffset3";
        private readonly List<TimesOffsetType> _timesOffsetType;
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
        public OffsetDateValueThreeCombinedRetriever(
            string offsetDateRegex,
            List<TimesOffsetType> timesOffsetTypes,
            IDateTimeProvider dateTimeProvider,
            bool isFutureOffset = false)
        {
            _timesOffsetType = timesOffsetTypes;
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
                || !match.Groups[TimesOffsetGroupName1].Success
                || !int.TryParse(match.Groups[TimesOffsetGroupName1].Value, out _))
                return false;

            if (!match.Success
                || !match.Groups[TimesOffsetGroupName2].Success
                || !int.TryParse(match.Groups[TimesOffsetGroupName2].Value, out _))
                return false;

            if (!match.Success
                || !match.Groups[TimesOffsetGroupName3].Success
                || !int.TryParse(match.Groups[TimesOffsetGroupName3].Value, out _))
                return false;

            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return type == typeof(DateTimeOffset) || type == typeof(DateTime) || type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var match = _offsetDateRegex.Match(keyValuePair.Value);
            var timesOffset1 = int.Parse(match.Groups[TimesOffsetGroupName1].Value);
            timesOffset1 = _futureOffset ? timesOffset1 : -timesOffset1;
            var timesOffset2 = int.Parse(match.Groups[TimesOffsetGroupName2].Value);
            timesOffset2 = _futureOffset ? timesOffset2 : -timesOffset2;
            var timesOffset3 = int.Parse(match.Groups[TimesOffsetGroupName3].Value);
            timesOffset3 = _futureOffset ? timesOffset3 : -timesOffset3;

            var dateTime = _dateTimeProvider.NowOffset;
            var offsetDateBuilder1 = new OffsetDateBuilder
            {
                Type = _timesOffsetType[0],
                TimesOffset = timesOffset1
            };

            var offsetDateBuilder2 = new OffsetDateBuilder
            {
                Type = _timesOffsetType[1],
                TimesOffset = timesOffset2
            };

            var offsetDateBuilder3 = new OffsetDateBuilder
            {
                Type = _timesOffsetType[2],
                TimesOffset = timesOffset3
            };

            var offsetDateBuilderList = new List<OffsetDateBuilder>
            { offsetDateBuilder1, offsetDateBuilder2, offsetDateBuilder3 };

            foreach (var offset in offsetDateBuilderList)
            {
                dateTime = offset.Type switch
                {
                    TimesOffsetType.YEAR => dateTime.Date.AddYears(offset.TimesOffset),
                    TimesOffsetType.MONTH => dateTime.Date.AddMonths(offset.TimesOffset),
                    TimesOffsetType.DAY => dateTime.Date.AddDays(offset.TimesOffset),
                    TimesOffsetType.HOUR => dateTime.AddHours(offset.TimesOffset),
                    TimesOffsetType.MINUTE => dateTime.AddMinutes(offset.TimesOffset),
                    TimesOffsetType.SECOND => dateTime.AddSeconds(offset.TimesOffset),
                    _ => throw new ArgumentOutOfRangeException(),
                };

            }

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
}
