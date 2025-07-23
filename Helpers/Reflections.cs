using AutoFixture;
using Newtonsoft.Json;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Helpers
{
    public static class Reflections
    {
        public static readonly Regex TabIndexRegex = new Regex(@"(?<array>.*)\[(?<index>.+)\]", RegexOptions.Compiled);
        public static readonly Regex TypeRegex = new Regex(@"(?<property>.*)\<(?<type>.*)\>", RegexOptions.Compiled);
        private static readonly Fixture _fixture = new Fixture { OmitAutoProperties = true, RepeatCount = 1 };

        public static void SetPropValue(this object obj, string name, string value, Fixture fixture = null)
        {
            var valueSetter = GetValueSetter(obj, name.Split('.'), 0, fixture ?? _fixture);
            var propertyName = valueSetter.GetPropertyInfo().Name;
            var propertyType = valueSetter.GetValueType();
            var parentType = valueSetter.GetParentType();

            var valueRetriever = GetValueRetrieverFor(propertyName, value, parentType, propertyType);
            if (valueRetriever != null && value != null)
            {
                valueSetter.SetValue(valueRetriever.Retrieve(new KeyValuePair<string, string>(propertyName, value), parentType, propertyType));
            }
            else
            {
                object converted = value != null ? Convert.ChangeType(value, valueSetter.GetPropertyInfo().PropertyType) : null;
                valueSetter.SetValue(converted);
            }
        }

        public static object GetPropValue(this object obj, string name, Fixture fixture = null)
        {
            var valueSetter = GetValueSetter(obj, name.Split('.'), 0, fixture ?? _fixture);
            return valueSetter.GetPropertyInfo().GetValue(valueSetter.GetParent());
        }

        private static IValueRetriever GetValueRetrieverFor(string propertyName, string value, Type targetType, Type propertyType)
        {
            foreach (var valueRetriever in Service.Instance.ValueRetrievers)
            {
                if (valueRetriever.CanRetrieve(new KeyValuePair<string, string>(propertyName, value), targetType, propertyType))
                    return valueRetriever;
            }
            return null;
        }

        private static IValueSetter GetValueSetter(object obj, string[] parts, int index, Fixture fixture)
        {
            if (obj == null)
                throw new InvalidOperationException("obj is null");

            string part = parts[index];
            var match = TabIndexRegex.Match(part);
            object propValue;

            if (match.Success) //cas du tableau
            {
                var partTab = match.Groups["array"].Value;
                var matchType = TypeRegex.Match(partTab);
                partTab = matchType.Success ? matchType.Groups["property"].Value : partTab;

                var info = GetPropertyInfo(obj, partTab);

                if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                {
                    var key = JsonConvert.DeserializeObject(match.Groups["index"].Value, info.PropertyType.GetGenericArguments().First());
                    if (!(info.GetValue(obj) is IDictionary dico))
                    {
                        dico = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(info.PropertyType.GetGenericArguments())) as IDictionary;
                        info.SetValue(obj, dico);
                    }

                    // Dernier element
                    if (index == parts.Length - 1)
                    {
                        return new DictionaryValueSetter(dico, key, info);
                    }

                    if (!dico.Contains(key))
                    {
                        dico[key] = fixture.Create(info.PropertyType.GetGenericArguments()[1]);
                    }

                    propValue = dico.GetType().GetProperty("Item").GetValue(dico, new[] { key });
                }
                else
                {
                    var list = GetList(obj, info, fixture, matchType);
                    int listIndex = int.Parse(match.Groups["index"].Value);

                    if (list != info.GetValue(obj, null))
                    {
                        info.SetValue(obj, list);
                    }

                    // Dernier element
                    if (index == parts.Length - 1)
                    {
                        return new ListValueSetter(list, listIndex, info);
                    }

                    if (listIndex <= list.Count - 1)
                    {
                        propValue = list[listIndex];
                    }
                    else if (list.Count == listIndex)
                    {
                        if (list is Array arr)
                        {
                            // La méthode Add n'est pas implémentée pour le type array
                            var arrayElementType = info.PropertyType.GetElementType() ?? info.PropertyType.GetGenericArguments().First();
                            propValue = _fixture.Create(arrayElementType);
                            var newArray = Array.CreateInstance(arrayElementType, arr.Length + 1);
                            arr.CopyTo(newArray, 0);
                            newArray.SetValue(propValue, arr.Length);
                            info.SetValue(obj, newArray);
                            list = newArray;
                        }
                        else
                        {
                            if (matchType.Success)
                            {
                                var concreteType = obj.GetType().Assembly.GetTypes().First(t => t.Name == matchType.Groups["type"].Value);
                                propValue = fixture.Create(concreteType);
                            }
                            else
                            {
                                propValue = fixture.Create(info.PropertyType.GetGenericArguments().First());
                            }

                            list.Add(propValue);
                        }
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }
            else
            {
                var typeMatch = TypeRegex.Match(part);
                part = typeMatch.Success ? typeMatch.Groups["property"].Value : part;

                var info = GetPropertyInfo(obj, part);

                // Dernier element
                if (index == parts.Length - 1)
                {
                    return new ObjectValueSetter(obj, info);
                }

                // Sinon recupération de l'objet
                propValue = info.GetValue(obj, null);
                if (propValue == null)
                {
                    if (typeMatch.Success)
                    {
                        var concreteType = obj.GetType().Assembly.GetTypes().First(t => t.Name == typeMatch.Groups["type"].Value);
                        propValue = fixture.Create(concreteType);
                    }
                    else
                    {
                        propValue = fixture.Create(info.PropertyType);
                    }

                    info.SetValue(obj, propValue);
                }
            }

            return GetValueSetter(propValue, parts, index + 1, fixture);
        }

        private static PropertyInfo GetPropertyInfo(object obj, string name)
        {
            Type type = obj.GetType();
            var info = type.GetProperty(name);
            if (info == null)
                throw new InvalidOperationException($"Property not found for {name} on {type}");

            return info;
        }

        private static IList GetList(object obj, PropertyInfo info, Fixture fixture, Match typeMatch)
        {
            // Verification que le type est bien enumerable
            if (!typeof(IEnumerable).IsAssignableFrom(info.PropertyType) && info.PropertyType.IsGenericType)
            {
                throw new InvalidOperationException($"Property is not a generic Enumerable");
            }

            var type = info.PropertyType.IsGenericType ? info.PropertyType.GetGenericArguments().First() : info.PropertyType.GetElementType();

            if (typeMatch.Success)
            {
                type = obj.GetType().Assembly.GetTypes().First(t => t.Name == typeMatch.Groups["type"].Value);
            }

            dynamic propValue = info.GetValue(obj) ?? fixture.CreateMany(type);
            return propValue as IList ?? (info.PropertyType.IsArray ? Enumerable.ToArray(propValue) : Enumerable.ToList(propValue));
        }
    }

    public interface IValueSetter
    {
        void SetValue(object value);
        PropertyInfo GetPropertyInfo();
        object GetParent();
        Type GetValueType();
        Type GetParentType();
    }

    public class DictionaryValueSetter : IValueSetter
    {
        private readonly IDictionary _dico;
        private readonly object _key;
        private readonly PropertyInfo _info;

        public DictionaryValueSetter(IDictionary dico, object key, PropertyInfo info)
        {
            _dico = dico;
            _key = key;
            _info = info;
        }
        public object GetParent()
        {
            return _dico;
        }

        public Type GetParentType()
        {
            return _dico.GetType();
        }

        public PropertyInfo GetPropertyInfo()
        {
            return _info;
        }

        public Type GetValueType()
        {
            return _info.PropertyType.GetGenericArguments()[1];
        }

        public void SetValue(object value)
        {
            _dico[_key] = value;
        }
    }

    public class ObjectValueSetter : IValueSetter
    {
        private readonly object _parent;
        private readonly PropertyInfo _info;

        public ObjectValueSetter(object parent, PropertyInfo info)
        {
            _parent = parent;
            _info = info;
        }

        public object GetParent() => _parent;

        public Type GetParentType() => _parent.GetType();

        public PropertyInfo GetPropertyInfo() => _info;

        public Type GetValueType() => _info.PropertyType;

        public void SetValue(object value)
        {
            _info.SetValue(_parent, value);

            object settedValue = _info.GetValue(_parent);
            if (value == settedValue)
                return;

            if ((value == null || settedValue == null))
                throw new InvalidOperationException($"You want affect {value} to {_info.Name} of type {_info.PropertyType} but {_info.GetValue(_parent)} has been retrieved");

            if (!settedValue.Equals(value))
                throw new InvalidOperationException($"You want affect {value} to {_info.Name} of type {_info.PropertyType} but {_info.GetValue(_parent)} has been retrieved");
        }
    }

    public class ListValueSetter : IValueSetter
    {
        private readonly IList _listObject;
        private readonly int _index;
        private readonly PropertyInfo _info;

        public ListValueSetter(IList tabObject, int index, PropertyInfo info)
        {
            _listObject = tabObject;
            _index = index;
            _info = info;
        }
        public void SetValue(object value)
        {
            if (_listObject.Count > _index)
            {
                _listObject[_index] = value;
            }
            else if (_listObject.Count == _index)
            {
                _listObject.Add(value);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public PropertyInfo GetPropertyInfo() => _info;

        public object GetParent() => _listObject;

        public Type GetValueType() => _info.PropertyType.GetGenericArguments().FirstOrDefault() ?? _info.PropertyType.GetElementType();
        public Type GetParentType() => _listObject.GetType().GetGenericArguments().FirstOrDefault() ?? _listObject.GetType().GetElementType();
    }
}
