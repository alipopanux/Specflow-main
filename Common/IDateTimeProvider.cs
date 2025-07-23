namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class IDateTimeProvider
    {
        public DateTime Now => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        public DateTimeOffset NowOffset => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
    }
}
