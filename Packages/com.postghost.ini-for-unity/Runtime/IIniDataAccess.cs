using IniParser.Model;

namespace PostGhost.IniForUnity
{
    public interface IIniDataAccess
    {
        KeyDataCollection this[string sectionName] { get; }

        bool TryGetValue<T>(string keyName, out T value);

        bool TryGetValue<T>(string sectionName, string keyName, out T value);

        bool TryConvertFromString<T>(string valueString, out T value);
    }
}