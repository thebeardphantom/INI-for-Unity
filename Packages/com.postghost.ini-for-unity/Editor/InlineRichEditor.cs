using IniParser.Model.Formatting;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace PostGhost.IniForUnity.Editor
{
    public class InlineRichEditor : IInlineEditor
    {
        private readonly IniAsset _iniAsset;

        private readonly List<TreeViewItemData<SectionKeyData>> _itemSource = new();

        private MultiColumnTreeView _treeView;

        public bool IsDirty { get; }

        public InlineRichEditor(IniAsset iniAsset)
        {
            _iniAsset = iniAsset;
        }

        private static VisualElement MakeTextField()
        {
            return new TextField();
        }

        public VisualElement CreateEditorGUI()
        {
            SetContents(null);
            _treeView = new MultiColumnTreeView
            {
                reorderable = false,
            };
            var sectionColumn = new Column
            {
                title = "Section",
                name = "section",
                stretchable = true,
                makeCell = MakeTextField,
                bindCell = BindSectionCell,
                sortable = true,
            };
            _treeView.columns.Add(sectionColumn);
            var keyColumn = new Column
            {
                title = "Key",
                name = "key",
                stretchable = true,
                makeCell = MakeTextField,
                bindCell = BindKeyCell,
                sortable = true,
            };
            _treeView.columns.Add(keyColumn);
            var valueColumn = new Column
            {
                title = "Value",
                name = "value",
                stretchable = true,
                makeCell = MakeTextField,
                bindCell = BindValueCell,
                sortable = true,
            };
            _treeView.columns.Add(valueColumn);
            _treeView.SetRootItems(_itemSource);
            return _treeView;
        }

        public string GetContents()
        {
            var iniDataFormatter = new DefaultIniDataFormatter();
            return _iniAsset.IniData.ToString(iniDataFormatter);
        }

        public void SetContents(string contents)
        {
            _itemSource.Clear();
            IEnumerable<SectionKeyData> keyDatas = _iniAsset.IniData.Global.Select(kd => new SectionKeyData(null, kd.KeyName, kd.Value));
            keyDatas = keyDatas.Concat(
                _iniAsset
                    .IniData
                    .Sections
                    .SelectMany(s => s.Keys.Select(kd => new SectionKeyData(s.SectionName, kd.KeyName, kd.Value))));
            IEnumerable<TreeViewItemData<SectionKeyData>> treeViewItems = keyDatas
                .Select((data, index) => new TreeViewItemData<SectionKeyData>(index, data));
            _itemSource.AddRange(treeViewItems);
        }

        private void BindSectionCell(VisualElement arg1, int index)
        {
            var textField = (TextField)arg1;
            textField.SetValueWithoutNotify(_itemSource[index].data.Section);
        }

        private void BindKeyCell(VisualElement arg1, int index)
        {
            var textField = (TextField)arg1;
            textField.SetValueWithoutNotify(_itemSource[index].data.Key);
        }

        private void BindValueCell(VisualElement arg1, int index)
        {
            var textField = (TextField)arg1;
            textField.SetValueWithoutNotify(_itemSource[index].data.Value);
        }

        private class SectionKeyData
        {
            public readonly string Section;
            public readonly string Key;
            public readonly string Value;

            public SectionKeyData(string section, string key, string value)
            {
                Section = section;
                Key = key;
                Value = value;
            }
        }
    }
}