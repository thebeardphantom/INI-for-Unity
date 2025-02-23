using System.IO;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine.UIElements;

namespace PostGhost.IniForUnity.Editor
{
    [CustomEditor(typeof(IniAssetImporter))]
    public class IniAssetImporterEditor : ScriptedImporterEditor
    {
        private IInlineEditor _inlineEditor;

        public override bool showImportedObject => false;

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            _inlineEditor = new InlineTextEditor();
            VisualElement inlineEditorGui = _inlineEditor.CreateEditorGUI();
            container.Add(inlineEditorGui);
            ReloadInlineEditorContentsFromDisk();
            container.Add(new IMGUIContainer(ApplyRevertGUI));
            return container;
        }

        public override bool HasModified()
        {
            return _inlineEditor is { IsDirty: true, } || base.HasModified();
        }

        public override void DiscardChanges()
        {
            ReloadInlineEditorContentsFromDisk();
            base.DiscardChanges();
        }

        protected override void Apply()
        {
            string contents = _inlineEditor.GetContents();
            string path = AssetDatabase.GetAssetPath(assetTarget);
            File.WriteAllText(path, contents);
            AssetDatabase.ImportAsset(path);
            base.Apply();
        }

        private void ReloadInlineEditorContentsFromDisk()
        {
            string path = AssetDatabase.GetAssetPath(assetTarget);
            string contents = File.ReadAllText(path);
            _inlineEditor.SetContents(contents);
        }
    }
}