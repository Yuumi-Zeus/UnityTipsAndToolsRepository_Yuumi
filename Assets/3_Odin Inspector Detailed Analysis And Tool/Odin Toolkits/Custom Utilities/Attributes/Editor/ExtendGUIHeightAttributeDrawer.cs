using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Custom_Utilities.Attributes.Editor
{
    [DrawerPriority(0, 9)]
    public class ExtendGUIHeightAttributeDrawer : OdinAttributeDrawer<ExtendGUIHeightAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            EditorGUILayout.Space(Attribute.Height);
            CallNextDrawer(label);
        }
    }
}