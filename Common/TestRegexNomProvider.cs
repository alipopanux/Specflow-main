namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class TestRegexNomProvider : INomRegexProvider
    {
        public string GetRegex()
        {
            return @"format : (^[a-zA-ZàâäãáåÀÁÂÃÄÅçÇéèêëÉÈÊËîïìíÌÍÎÏñÑôöðòóõÒÓÔÕÖùúûüÙÚÛÜýÿÝ'’-]+$)|(^[A-Za-z]+(?:[-'][A-Za-z]+)*(?: [A-Za-z]+(?:[-'][A-Za-z]+)*)?$)";
        }
    }

    public interface INomRegexProvider
    {
        string GetRegex();
    }
}
