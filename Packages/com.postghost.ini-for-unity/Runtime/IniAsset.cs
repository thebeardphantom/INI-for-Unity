using IniParser.Model;
using IniParser.Parser;
using UnityEngine;

namespace PostGhost.IniForUnity
{
    public class IniAsset : ScriptableObject, ISerializationCallbackReceiver
    {
        public IniData IniData { get; private set; }

        [field: SerializeField]
        private string FileContents { get; set; }

        public static IniAsset Create(string fileContents)
        {
            var iniAsset = CreateInstance<IniAsset>();
            iniAsset.FileContents = fileContents;
            return iniAsset;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (string.IsNullOrWhiteSpace(FileContents))
            {
                return;
            }

            var parser = new IniDataParser();
            IniData = parser.Parse(FileContents);
        }
    }
}