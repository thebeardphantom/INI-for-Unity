using IniParser.Model;

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

        public bool TryGetGlobalValue<T>(string keyName, out T value)
        {
            return _rawData.Global.TryGetValue(keyName, out value);
        }

        public bool TryGetValue<T>(string sectionName, string keyName, out T value)
        {
            return _rawData.TryGetValue(sectionName, keyName, out value);
        }
    }
}