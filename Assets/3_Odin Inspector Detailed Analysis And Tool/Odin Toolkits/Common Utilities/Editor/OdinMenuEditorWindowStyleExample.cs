using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Odin_Toolkits.General.Editor
{
    public class OdinMenuEditorWindowStyleExample : OdinMenuEditorWindow
    {
        static void ShowWindow()
        {
            var window = GetWindow<OdinMenuEditorWindowStyleExample>();
            window.titleContent = new GUIContent("#CLASSNAME#");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.Show();
        }

        #region 自定义样式一览

        protected override void OnEnable()
        {
            base.OnEnable();
            // 可以自定义相关属性
            // OdinMenuEditorWindow Default Properties
            MenuBackgroundColor = Color.clear;
            MenuWidth = 200;
            ResizableMenuWidth = true;
            DrawMenuSearchBar = true;
            // OdinEditorWindow Default Properties
            DefaultLabelWidth = 0.33f;
            WindowPadding = new Vector4(4f, 4f, 4f, 4f);
            UseScrollView = true;
            DrawUnityEditorPreview = false;
            DefaultEditorPreviewHeight = 170f;
        }

        /// <summary>
        /// 可以自定义菜单样式
        /// </summary>
        /// <returns></returns>
        static OdinMenuStyle CustomMenuStyle()
        {
            // OdinMenuStyle Default Properties
            var customMenuStyle = new OdinMenuStyle
            {
                // General
                Height = 30,
                Offset = 16f,
                LabelVerticalOffset = 0f,
                IndentAmount = 15f,
                // Icons
                IconSize = 16f,
                IconOffset = 0f,
                NotSelectedIconAlpha = 0.85f,
                IconPadding = 3f,
                // Triangle
                DrawFoldoutTriangle = true,
                TriangleSize = 17f,
                TrianglePadding = 8f,
                AlignTriangleLeft = false,
                // Borders
                Borders = true,
                BorderPadding = 13f,
                BorderAlpha = 0.5f,
                // Colors
                SelectedColorDarkSkin = SirenixGUIStyles.DefaultSelectedMenuTreeColorDarkSkin,
                SelectedInactiveColorDarkSkin = SirenixGUIStyles.DefaultSelectedInactiveMenuTreeColorDarkSkin,
                SelectedColorLightSkin = SirenixGUIStyles.DefaultSelectedMenuTreeColorLightSkin,
                SelectedInactiveColorLightSkin = SirenixGUIStyles.DefaultSelectedInactiveMenuTreeColorLightSkin,
                // GUIStyles
                DefaultLabelStyle = SirenixGUIStyles.Label,
                SelectedLabelStyle = SirenixGUIStyles.WhiteLabel
            };
            return customMenuStyle;
        }

        /// <summary>
        /// 和 UnityProjectWindow 比较相似的 OdinMenuStyle 样式
        /// </summary>
        static OdinMenuStyle UnityTreeViewStyle()
        {
            var customMenuStyle = new OdinMenuStyle
            {
                BorderPadding = 0.0f,
                AlignTriangleLeft = true,
                TriangleSize = 16f,
                TrianglePadding = 0.0f,
                Offset = 20f,
                Height = 23,
                IconPadding = 0.0f,
                BorderAlpha = 0.323f
            };
            return customMenuStyle;
        }

        #endregion

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true) { };
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}