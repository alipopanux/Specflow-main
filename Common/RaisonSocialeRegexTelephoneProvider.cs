namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class RaisonSocialeRegexTelephoneProvider : IRaisonSocialeRegleProvider
    {
        public string GetRegex()
        {
            return @"format : ^(?![\s\S]*[\n\r\t])(?!.*^\s|.*\s$)(?!.*\s{2})[\wàâäãáåÀÁÂÃÄÅçÇéèêëÉÈÊËîïìíÌÍÎÏñÑôöðòóõÒÓÔÕÖùúûüÙÚÛÜýÿÝ'\-\.,%()@& ]{1,200}$";
        }
    }

    public interface IRaisonSocialeRegleProvider
    {
        string GetRegex();
    }
}
