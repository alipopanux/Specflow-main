using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    public class CommentaireAgeApprentiRetriever : IValueRetriever
    {
        private readonly string _stringkeyWord;
        private readonly ICommentaireProvider _commentaireProvider;

        public CommentaireAgeApprentiRetriever(string emptykeyWord, ICommentaireProvider commentaireProvider)
        {
            _stringkeyWord = emptykeyWord;
            _commentaireProvider = commentaireProvider;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _stringkeyWord && type == typeof(string);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (propertyType == typeof(string))
                return _commentaireProvider.GetCommentaire();

            return Activator.CreateInstance(propertyType);
        }
    }
}
