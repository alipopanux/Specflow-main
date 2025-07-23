namespace Lopcommerce.Regles.WebAPI.Tests.Common.ValueRetrievers
{
    using System;
    using System.Collections.Generic;
    using TechTalk.SpecFlow.Assist;

    public class BooleanValueRetriever : IValueRetriever
    {
        private readonly string _vraiKeyWord;
        private readonly bool _value;

        public BooleanValueRetriever(bool value, string vraiKeyWord = "vrai")
        {
            _vraiKeyWord = vraiKeyWord;
            _value = value;
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            return keyValuePair.Value == _vraiKeyWord && (type == typeof(bool?) || type == typeof(bool));
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (keyValuePair.Value.Equals(_vraiKeyWord, StringComparison.OrdinalIgnoreCase))
                return _value;
            else
                throw new ArgumentException($"Impossible de récupérer une valeur booléenne à partir de : {keyValuePair.Value}");
        }
    }
}