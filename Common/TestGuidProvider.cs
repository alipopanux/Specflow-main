namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class TestGuidProvider : IGuidProvider
    {
        public string NewGuid()
        {
            return new string("dddddddd-dddd-dddd-dddd-dddddddddddd");
        }
    }

    public interface IGuidProvider
    {
        string NewGuid();
    }
}
