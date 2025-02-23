using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace PostGhost.IniForUnity.Editor
{
    [InitializeOnLoad]
    public static class IniEditorEntryPoint
    {
        static IniEditorEntryPoint()
        {
            HashSet<string> extensions = EditorSettings.projectGenerationUserExtensions.ToHashSet();
            extensions.Add("ini");
            EditorSettings.projectGenerationUserExtensions = extensions.ToArray();
        }
    }
}