using IniParser.Model;
using IniParser.Parser;
using UnityEngine;

namespace PostGhost.IniForUnity
{
    public class IniAsset : ScriptableObject, ISerializationCallbackReceiver, IIniDataAccess
    {
        private IIniDataAccess _iniDataAccess = NullIniDataAccess.Instance;

        public IniData RawData { get; private set; }

        [field: SerializeField]
        private string FileContents { get; set; }

        public KeyDataCollection this[string sectionName] => _iniDataAccess[sectionName];

        public static IniAsset Create(string fileContents)
        {
            var iniAsset = CreateInstance<IniAsset>();
            iniAsset.FileContents = fileContents;
            return iniAsset;
        }

        public bool TryGetValue<T>(string keyName, out T value)
        {
            return _iniDataAccess.TryGetValue(keyName, out value);
        }

        public bool TryGetValue<T>(string sectionName, string keyName, out T value)
        {
            return _iniDataAccess.TryGetValue(sectionName, keyName, out value);
        }

        public bool TryConvertFromString<T>(string valueString, out T value)
        {
            return _iniDataAccess.TryConvertFromString(valueString, out value);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (string.IsNullOrWhiteSpace(FileContents))
            {
                return;
            }

            var parser = new IniDataParser();
            RawData = parser.Parse(FileContents);
            _iniDataAccess = new IniDataAccess(RawData);
        }
    }
}