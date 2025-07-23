using Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Assist.ValueRetrievers;
using static Lopcommerce.Regles.WebAPI.Tests.Common.Constantes.Constantes;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.Hooks
{
    [Binding]
    public static class HooksTestRun
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Service.Instance.ValueRetrievers.Register(new EnumMemberValueRetriever());
            Service.Instance.ValueRetrievers.Register(new BooleanValueRetriever(true, "vrai"));
            Service.Instance.ValueRetrievers.Register(new BooleanValueRetriever(false, "faux"));
            Service.Instance.ValueRetrievers.Register(new ValueRetrievers.GuidValueRetriever("guid", new TestGuidProvider()));
            Service.Instance.ValueRetrievers.Register(new CommentaireAgeApprentiRetriever(CommentairesMotif.AGE_APPRENTI_COMMENTAIRE, new TestCommentaireProvider(CommentairesMotif.AGE_APPRENTI)));
            Service.Instance.ValueRetrievers.Register(new CommentaireAgeApprentiRetriever(CommentairesMotif.DATE_FORMATION_COMMENTAIRE, new TestCommentaireProvider(CommentairesMotif.DATE_FORMATION)));
            Service.Instance.ValueRetrievers.Register(new TelephoneRegexValueRetriever("regexTelephone", new TestRegexTelephoneProvider()));
            Service.Instance.ValueRetrievers.Register(new SirenRegexValueRetriever("regexSiren", new TestRegexSirenProvider()));
            Service.Instance.ValueRetrievers.Register(new NomRegexValueRetriever("regexNom", new TestRegexNomProvider()));
            Service.Instance.ValueRetrievers.Register(new NullValueRetriever("null"));
            Service.Instance.ValueRetrievers.Register(new EmptyValueRetriever("vide"));
            Service.Instance.ValueRetrievers.Register(new FixtureValueRetriever("aléatoire"));
            Service.Instance.ValueRetrievers.Register(new NowValueRetriever("maintenant", new TestDateTimeProvider()));
            Service.Instance.ValueRetrievers.Register(new ToDayValueRetriever("date du jour", new TestDateTimeProvider()));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
                $@"il y a (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) secondes?",
                TimesOffsetType.SECOND,
                new TestDateTimeProvider()));
            Service.Instance.ValueRetrievers.Register(new LengthStringValueRetriever(
                $@"(?<{LengthStringValueRetriever.OccurenceGroupName}>\d+) lettres?",
                new TestStringProvider()));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
                $@"il y a (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) mois?",
                TimesOffsetType.MONTH,
                new TestDateTimeProvider()));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
                $@"dans (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) mois?",
                TimesOffsetType.MONTH,
                new TestDateTimeProvider(),
                isFutureOffset: true));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
                $@"il y a (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) jours?",
                TimesOffsetType.DAY,
                new TestDateTimeProvider()));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
                $@"dans (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) jours?",
                TimesOffsetType.DAY,
                new TestDateTimeProvider(),
                isFutureOffset: true));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
                $@"dans (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) secondes?",
                TimesOffsetType.SECOND,
                new TestDateTimeProvider(),
                isFutureOffset: true));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
                $@"il y a (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) ans?",
                TimesOffsetType.YEAR,
                new TestDateTimeProvider()));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
              $@"dans (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) an?",
              TimesOffsetType.YEAR,
              new TestDateTimeProvider(),
                isFutureOffset: true));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueRetriever(
                $@"dans (?<{OffsetDateValueRetriever.TimesOffsetGroupName}>\d+) ans?",
                TimesOffsetType.YEAR,
                new TestDateTimeProvider(),
                isFutureOffset: true));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueTwoCombinedRetriever(
                $@"dans (?<{OffsetDateValueTwoCombinedRetriever.TimesOffsetGroupName1}>\d+) mois et (?<{OffsetDateValueTwoCombinedRetriever.TimesOffsetGroupName2}>\d+) jours?",
                [TimesOffsetType.MONTH, TimesOffsetType.DAY],
                new TestDateTimeProvider(),
                isFutureOffset: true));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueTwoCombinedRetriever(
                $@"il y a (?<{OffsetDateValueTwoCombinedRetriever.TimesOffsetGroupName1}>\d+) mois et (?<{OffsetDateValueTwoCombinedRetriever.TimesOffsetGroupName2}>\d+) jours?",
                [TimesOffsetType.MONTH, TimesOffsetType.DAY],
                new TestDateTimeProvider(),
                isFutureOffset: false));

            Service.Instance.ValueRetrievers.Register(new OffsetDateValueThreeCombinedRetriever(
                $@"dans (?<{OffsetDateValueThreeCombinedRetriever.TimesOffsetGroupName1}>\d+) ans et (?<{OffsetDateValueThreeCombinedRetriever.TimesOffsetGroupName2}>\d+) mois et (?<{OffsetDateValueThreeCombinedRetriever.TimesOffsetGroupName3}>\d+) jours?",
                [TimesOffsetType.YEAR, TimesOffsetType.MONTH, TimesOffsetType.DAY],
                new TestDateTimeProvider(),
                isFutureOffset: true));
            Service.Instance.ValueRetrievers.Register(new OffsetDateValueThreeCombinedRetriever(
                $@"il y a (?<{OffsetDateValueThreeCombinedRetriever.TimesOffsetGroupName1}>\d+) ans et (?<{OffsetDateValueThreeCombinedRetriever.TimesOffsetGroupName2}>\d+) mois et (?<{OffsetDateValueThreeCombinedRetriever.TimesOffsetGroupName3}>\d+) jours?",
                [TimesOffsetType.YEAR, TimesOffsetType.MONTH, TimesOffsetType.DAY],
                new TestDateTimeProvider(),
                isFutureOffset: false));
        }
    }
}
