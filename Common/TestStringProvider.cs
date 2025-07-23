namespace Lopcommerce.Regles.WebAPI.Tests.Common
{
    public class TestStringProvider : IStringProvider
    {
        private readonly Random _rng = new();
        public string GetRandomString(int length)
        {
            const string allowedChars = "A";

            char[] buffer = new char[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = allowedChars[_rng.Next(allowedChars.Length)];
            }

            return new string(buffer);
        }
    }

    public interface IStringProvider
    {
        string GetRandomString(int length);
    }
}
