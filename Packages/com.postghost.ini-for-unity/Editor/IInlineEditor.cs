using UnityEngine.UIElements;

namespace PostGhost.IniForUnity.Editor
{
    public interface IInlineEditor
    {
        bool IsDirty { get; }
        VisualElement CreateEditorGUI();
        string GetContents();
        void SetContents(string contents);
    }
}