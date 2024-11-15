using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace HowToSerializeInterfaces.OdinInterfaceReferencePractice.Scripts.Editor
{
    public class OdinRequiredInterfaceAttributeDrawer : OdinAttributeDrawer<OdinRequiredInterfaceAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var interfaceType = Attribute.InterfaceType;
            // // Call the next drawer, which will draw the float field.
            // CallNextDrawer(label);
            // // Get a rect to draw the health-bar on.
            // var rect = EditorGUILayout.GetControlRect();
            // SirenixEditorGUI.BeginBoxHeader();
            // SirenixEditorGUI.Title("Odin", null, TextAlignment.Left, false);
            // SirenixEditorGUI.EndBoxHeader();
        }
    }
}