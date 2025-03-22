using IniParser.Model;
using IniParser.Model.Configuration;
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
            iniAsset.Repopulate();
            return iniAsset;
        }

        public bool TryGetGlobalValue<T>(string keyName, out T value)
        {
            return _iniDataAccess.TryGetGlobalValue(keyName, out value);
        }

        public bool TryGetValue<T>(string sectionName, string keyName, out T value)
        {
            return _iniDataAccess.TryGetValue(sectionName, keyName, out value);
        }

        private void Repopulate()
        {
            if (string.IsNullOrWhiteSpace(FileContents))
            {
                return;
            }

            IniParserConfiguration iniParserConfiguration = IniDataUtility.ParserConfigurationFactory.Invoke();
            var parser = new IniDataParser(iniParserConfiguration);
            RawData = parser.Parse(FileContents);
            _iniDataAccess = new IniDataAccess(RawData);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Repopulate();
        }
    }
}