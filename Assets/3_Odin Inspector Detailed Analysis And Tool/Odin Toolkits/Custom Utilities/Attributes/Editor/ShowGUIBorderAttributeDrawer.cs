using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Odin_Toolkits.Custom_Utilities.Attributes.Editor
{
    [DrawerPriority(0, 10)]
    public class ShowGUIBorderAttributeDrawer : OdinAttributeDrawer<ShowGUIBorderAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
            OdinEditorGUIUtil.DrawRectOutlineWithBorder(GUILayoutUtility.GetLastRect(), Color.green);
        }
    }
}