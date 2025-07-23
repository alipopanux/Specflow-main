using TechTalk.SpecFlow;

namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    [Binding]
    public class Transformations
    {
        private static readonly string DateFormat = "yyyy-MM-dd";
        private readonly DateTimeOffset _today = TestDateTimeProvider.NowDateTime.Date;
        private readonly string _guid = new TestGuidProvider().NewGuid();

        [StepArgumentTransformation("null")]
        public string NullString() => null;

        [StepArgumentTransformation(@"guid")]
        public string GuidString() => _guid;

        [StepArgumentTransformation("null")]
        public DateTime? NullDateTime() => null;

        [StepArgumentTransformation("null")]
        public DateTimeOffset? NullDateTimeOffSet() => null;

        [StepArgumentTransformation("vide")]
        public string EmptyString() => string.Empty;

        [StepArgumentTransformation(@"date du jour")]
        public string DateDuJourTransformationStr() => DateDuJourTransformation().ToString(DateFormat);

        [StepArgumentTransformation(@"date du jour")]
        public DateTimeOffset DateDuJourTransformation() => _today;

        [StepArgumentTransformation(@"date du jour")]
        public DateTimeOffset? DateDuJourNullableTransformation() => _today;

        [StepArgumentTransformation(@"date du jour")]
        public DateTime DateDuJourSansOffsetTransformation() => _today.Date;

        [StepArgumentTransformation(@"date du jour")]
        public DateTime? DateDuJourSansOffsetNullableTransformation() => _today.Date;

        [StepArgumentTransformation(@"maintenant")]
        public DateTimeOffset DateDuMomentTransformation() => TestDateTimeProvider.NowDateTimeOffset;

        [StepArgumentTransformation(@"maintenant")]
        public DateTimeOffset? DateDuMomentNullableTransformation() => TestDateTimeProvider.NowDateTimeOffset;

        [StepArgumentTransformation(@"maintenant")]
        public DateTime DateDuMomentSansOffsetTransformation() => TestDateTimeProvider.NowDateTime;

        [StepArgumentTransformation(@"maintenant")]
        public DateTime? DateDuMomentSansOffsetNullableTransformation() => TestDateTimeProvider.NowDateTime;
    }
}
