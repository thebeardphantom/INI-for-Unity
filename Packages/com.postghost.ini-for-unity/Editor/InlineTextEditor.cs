using UnityEditor;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace PostGhost.IniForUnity.Editor
{
    public class InlineTextEditor : IInlineEditor
    {
        private const string InlineEditorFontPath = "RobotoMono-Regular SDF";

        private TextField _textField;
        public bool IsDirty { get; private set; }

        public VisualElement CreateEditorGUI()
        {
            var font = (FontAsset)EditorGUIUtility.Load(InlineEditorFontPath);

            _textField = new TextField
            {
                multiline = true,
                tripleClickSelectsLine = true,
                doubleClickSelectsWord = true,
                selectAllOnFocus = false,
                selectAllOnMouseUp = false,
                style =
                {
                    unityFontDefinition = new StyleFontDefinition(font),
                    whiteSpace = WhiteSpace.Normal,
                    overflow = Overflow.Visible,
                    flexWrap = Wrap.Wrap,
                },
            };
            _textField.RegisterValueChangedCallback(OnTextFieldValueChanged);
            return _textField;
        }

        public string GetContents()
        {
            IsDirty = false;
            return _textField.text;
        }

        public void SetContents(string contents)
        {
            IsDirty = false;
            _textField.SetValueWithoutNotify(contents);
        }

        private void OnTextFieldValueChanged(ChangeEvent<string> evt)
        {
            IsDirty = true;
        }
    }
}