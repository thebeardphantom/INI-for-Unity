using IniParser.Model;

namespace PostGhost.IniForUnity
{
    public class NullIniDataAccess : IIniDataAccess
    {
        public static readonly NullIniDataAccess Instance = new();

        public KeyDataCollection this[string sectionName] => null;

        public bool TryGetValue<T>(string keyName, out T value)
        {
            value = default;
            return false;
        }

        public bool TryGetValue<T>(string sectionName, string keyName, out T value)
        {
            value = default;
            return false;
        }

        public bool TryConvertFromString<T>(string valueString, out T value)
        {
            value = default;
            return false;
        }
    }
}