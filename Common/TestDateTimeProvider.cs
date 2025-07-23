namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class TestDateTimeProvider : IDateTimeProvider
    {
        public static DateTime NowDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        public static DateTimeOffset NowDateTimeOffset = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
    }
}
