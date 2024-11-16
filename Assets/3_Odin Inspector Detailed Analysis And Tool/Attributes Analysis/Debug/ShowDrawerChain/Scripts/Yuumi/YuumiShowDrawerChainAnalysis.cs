using _3_Odin_Inspector_Detailed_Analysis_And_Tool.Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _3_Odin_Inspector_Detailed_Analysis_And_Tool.Attributes_Analysis.Debug.ShowDrawerChain.Scripts.Yuumi
{
    public class YuumiShowDrawerChainAnalysis : MonoBehaviour
    {
        [PropertyOrder(0)]
        [ShowDrawerChain]
        [DisplayAsString(TextAlignment.Left, EnableRichText = true, FontSize = 14)]
        [ShowInInspector]
        [EnableGUI]
        [HideLabel]
        public string Info
        {
            get
            {
                Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
                return "Display";
            }
        }

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public int intValue;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public float floatValue;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public bool boolValue;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public Vector2 vector2Value;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public string stringValue = "Unity Build-in";

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public LayerMask layerMask;

        [PropertyOrder(1)] [TitleGroup("Unity 内置类型开启 Odin 插件绘制的 DrawerChain")] [ShowDrawerChain]
        public Color color;

        [PropertyOrder(-2)]
        [OnInspectorGUI]
        void CustomGUI()
        {
            GUILayout.Box("由 GUILayout.Box(new GUIContent());绘制，此时顺序为 -2");
            var wordWrapLabelStyle = new GUIStyle(GUI.skin.label)
            {
                wordWrap = true
            };
            var rect = EditorGUILayout.GetControlRect();
            GUI.Label(rect,
                new GUIContent("由 GUILayout.Label 绘制，在 Odin 的 OnInspectorGUI 方法中，使用 GUILayout 优先，使用 EditorGUI 部分出错"),
                wordWrapLabelStyle);
            OdinEditorGUIUtil.DrawRectOutlineWithBorder(rect, Color.red);
            var secondRect = GUILayoutUtility.GetLastRect();
            OdinEditorGUIUtil.DrawRectOutlineWithBorder(secondRect, Color.cyan);
            GUILayout.Label($"此时获取的第一个 Rect 为: {rect}");
            GUILayout.Label($"此时获取的第二个 Rect 为: {secondRect}");
        }

        [OnInspectorGUI]
        void CustomGUI2()
        {
            GUILayout.Box("由 GUILayout.Box(new GUIContent());绘制，此时顺序为 默认 0");
            EditorGUILayout.HelpBox("由 EditorGUILayout.HelpBox 绘制，此时正常运行", MessageType.Info);
        }

        [PropertyOrder(5)]
        [OnInspectorGUI]
        void CustomGUI3()
        {
            GUILayout.Box("由 GUILayout.Box(new GUIContent());绘制，此时顺序为 5");
        }
    }
}