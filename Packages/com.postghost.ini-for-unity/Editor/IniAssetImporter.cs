using System.IO;
using UnityEditor.AssetImporters;

namespace PostGhost.IniForUnity.Editor
{
    [ScriptedImporter(0, "ini")]
    public class IniAssetImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            string contents = File.ReadAllText(ctx.assetPath);
            var dataAsset = IniAsset.Create(contents);
            ctx.AddObjectToAsset("MainObject", dataAsset);
            ctx.SetMainObject(dataAsset);
        }
    }
}