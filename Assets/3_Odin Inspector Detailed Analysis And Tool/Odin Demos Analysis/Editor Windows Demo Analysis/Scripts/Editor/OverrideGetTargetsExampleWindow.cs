using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Odin_Demos_Analysis.Editor_Windows_Demo_Analysis.Scripts.Editor
{
    public class OverrideGetTargetsExampleWindow : OdinEditorWindow
    {
        [HideLabel] [Multiline(6)] [SuffixLabel("This is drawn", true)]
        public string Test;

        [MenuItem("Tools/Odin/Demos/Odin Editor Window Demos/Draw Any Target")]
        static void OpenWindow()
        {
            GetWindow<OverrideGetTargetsExampleWindow>()
                .position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        // In the default implemenentation, it simply yield returns it self.
        // But you can also override this behaviour and have your window render any
        // object you like - Unity and non-Unity objects a like.
        protected override IEnumerable<object> GetTargets()
        {
            // Draws this instance using Odin
            yield return this;

            // Draw non-unity objects.
            yield return GUI.skin.settings; // GUISettings is a regular class.

            // Or Unity objects.
            yield return GUI.skin; // GUI.Skin is a ScriptableObject
        }

        // You can also override the method that draws each editor.
        // This come in handy if you want to add titles, boxes, or draw them in a GUI.Window etc...
        protected override void DrawEditor(int index)
        {
            var currentDrawingEditor = CurrentDrawingTargets[index];

            SirenixEditorGUI.Title(
                currentDrawingEditor.ToString(),
                currentDrawingEditor.GetType().GetNiceFullName(),
                TextAlignment.Left,
                true
            );

            base.DrawEditor(index);

            if (index != CurrentDrawingTargets.Count - 1) SirenixEditorGUI.DrawThickHorizontalSeparator(15, 15);
        }
    }
}
#endif