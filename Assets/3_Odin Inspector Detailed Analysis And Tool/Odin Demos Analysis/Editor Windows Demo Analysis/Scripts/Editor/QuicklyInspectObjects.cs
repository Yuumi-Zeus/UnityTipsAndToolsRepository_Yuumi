using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

#if UNITY_EDITOR
namespace Odin_Demos_Analysis.Editor_Windows_Demo_Analysis.Scripts.Editor
{
    public class SomeClass2
    {
        [TextArea(10, 20)] public string Description = "Some description.";

        [HideLabel] [Title("Title", horizontalLine: false, bold: false)]
        public string Title = "Some Title";
    }

    public class QuicklyInspectObjects
    {
        SomeClass2 someObject = new();

        [Button(ButtonSizes.Large)]
        [Title("OdinEditorWindow.InspectObject examples", "Make sure to checkout QuicklyInspectObjects.cs")]
        void InspectObject()
        {
            OdinEditorWindow.InspectObject(someObject);
        }

        [Button(ButtonSizes.Large)]
        [HorizontalGroup("row1")]
        void InDropDownAutoHeight()
        {
            var btnRect = GUIHelper.GetCurrentLayoutRect();
            OdinEditorWindow.InspectObjectInDropDown(someObject, btnRect, btnRect.width);
        }

        [Button(ButtonSizes.Large)]
        [HorizontalGroup("row1")]
        void InDropDown()
        {
            var btnRect = GUIHelper.GetCurrentLayoutRect();
            OdinEditorWindow.InspectObjectInDropDown(someObject, btnRect, new Vector2(btnRect.width, 100));
        }

        [Button(ButtonSizes.Large)]
        [HorizontalGroup("row2")]
        void InCenter()
        {
            var window = OdinEditorWindow.InspectObject(someObject);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(270, 200);
        }

        [Button(ButtonSizes.Large)]
        [HorizontalGroup("row2")]
        void OtherStuffYouCanDo()
        {
            var window = OdinEditorWindow.InspectObject(someObject);

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(270, 200);
            window.titleContent = new GUIContent("Custom title", EditorIcons.RulerRect.Active);
            window.OnClose += () => Debug.Log("Window Closed");
            window.OnBeginGUI += () => GUILayout.Label("-----------");
            window.OnEndGUI += () => GUILayout.Label("-----------");
        }
    }
}
#endif