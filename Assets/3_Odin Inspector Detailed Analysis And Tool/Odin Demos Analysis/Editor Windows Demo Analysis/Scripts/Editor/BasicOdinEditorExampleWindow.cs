using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

#if UNITY_EDITOR
namespace Odin_Demos_Analysis.Editor_Windows_Demo_Analysis.Scripts.Editor
{
    public class BasicOdinEditorExampleWindow : OdinEditorWindow
    {
        [EnumToggleButtons]
        [InfoBox(
            "Inherit from OdinEditorWindow instead of EditorWindow " +
            "in order to create editor windows like you would inspectors - by exposing members and using attributes.")]
        // 最好从OdinEditorWindow继承，而不是EditorWindow，这样就可以创建类似于检查器的编辑器窗口，通过暴露成员和使用属性来实现。
        public ViewTool someField;

        [MenuItem("Tools/Odin/Demos/Odin Editor Window Demos/Basic Odin Editor Window")]
        static void OpenWindow()
        {
            var window = GetWindow<BasicOdinEditorExampleWindow>();

            // Nifty little trick to quickly position the window in the middle of the editor.
            // 快速定位窗口在编辑器中间的小技巧。
            // Sirenix 提供
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
        }
    }
}
#endif