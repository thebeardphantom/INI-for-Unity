using IniParser.Model;
using System.ComponentModel;

namespace PostGhost.IniForUnity
{
    public class IniDataAccess : IIniDataAccess
    {
        private readonly IniData _rawData;

        public IniDataAccess(IniData rawData)
        {
            _rawData = rawData;
        }

        public KeyDataCollection this[string sectionName] => _rawData[sectionName];

        public bool TryGetValue<T>(string keyName, out T value)
        {
            return _rawData.Global.TryGetValue(keyName, out value);
        }

        public bool TryGetValue<T>(string sectionName, string keyName, out T value)
        {
            return _rawData.TryGetValue(sectionName, keyName, out value);
        }

        public bool TryConvertFromString<T>(string valueString, out T value)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
            object valueObject = typeConverter.ConvertFromString(valueString);

            if (valueObject == null)
            {
                value = default;
                return false;
            }

            value = (T)valueObject;
            return true;
        }
    }
}