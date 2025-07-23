namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class TestRegexSirenProvider : ISirenRegexProvider
    {
        public string GetRegex()
        {
            return @"format : ^(?!1|2).*$";
        }
    }

    public interface ISirenRegexProvider
    {
        string GetRegex();
    }
}
