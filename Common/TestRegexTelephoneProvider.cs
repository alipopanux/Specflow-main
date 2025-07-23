namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class TestRegexTelephoneProvider : ITelephoneRegexProvider
    {
        public string GetRegex()
        {
            return @"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$";
        }
    }

    public interface ITelephoneRegexProvider
    {
        string GetRegex();
    }
}
