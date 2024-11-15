#if UNITY_EDITOR

using System;
using System.Globalization;
using Odin_Inspector_Detailed_Analysis_And_Tool.Common_Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Inspector_Detailed_Analysis_And_Tool.Odin_Demos_Analysis.Custom_Drawers_Analysis.Scripts
{
    // Custom data struct, for demonstration.
    [Serializable]
    public struct MyStruct
    {
        public float x;
        public float y;
    }

    [TypeInfoBox("This example demonstrates how a custom drawer can be implemented for a custom struct or class.\n" +
                 "这个案例展示怎么实现一个自定义 struct 或 class 的 字段抽屉绘制")]
    public class CustomDrawerDemoAnalysis : MonoBehaviour
    {
        public MyStruct myStruct;
    }
#if UNITY_EDITOR

    public class CustomStructDrawer : OdinValueDrawer<MyStruct>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var rect = EditorGUILayout.GetControlRect();
            var thisLineRectWidth = rect.width;
            OdinEditorDrawUtil.OdinDebug.DrawRectOutlineWithBorder(rect, Color.red, 5);
            // In Odin, labels are optional and can be null, so we have to account for that.
            if (label != null)
            {
                var prefixLabelRect = new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, rect.height);
                OdinEditorDrawUtil.OdinDebug.DrawRectOutlineWithBorder(prefixLabelRect, Color.yellow);
                // Unity 中对于可以修改的部分叫做 Control 控件
                // 参数中的 Rect 表示 前面的标签 + 后面的用于修改的控件  
                // 返回值的 Rect 只表示 控件部分
                // PrefixLabel 所用的宽度就是默认的 EditorGUIUtility.labelWidth
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            var value = this.ValueEntry.SmartValue;
            // EditorGUIUtility.labelWidth 是 Unity 内置的一个全局变量，用来控制标签的宽度，
            // 对于某些 控件，比如 Slider，无法设置这一个 Slider 的 Label 的宽度，（可能可以），那么就会采用全局的变量绘制 这个 Label
            // 如果标签太宽，可能会把控件给挤到下一行，此时需要设置一个更小的标签宽度，
            // 所以要临时修改，把我需要特制的 Slider 绘制完成后，再还原回来
            // --- 
            // var prev = EditorGUIUtility.labelWidth;
            // EditorGUIUtility.labelWidth = 20;
            // --- Push 操作等于上面
            GUIHelper.PushLabelWidth(20);
            OdinEditorDrawUtil.OdinDebug.DrawRectOutlineWithBorder(rect, Color.green, 3);
            value.x = EditorGUI.Slider(rect.AlignLeft(rect.width * 0.5f), " X ", value.x, 0, 1);
            OdinEditorDrawUtil.OdinDebug.DrawRectOutlineWithBorder(rect.AlignLeft(rect.width * 0.5f), Color.magenta, 0);
            value.y = EditorGUI.Slider(rect.AlignRight(rect.width * 0.5f), " Y ", value.y, 0, 1);
            OdinEditorDrawUtil.OdinDebug.DrawRectOutlineWithBorder(rect.AlignRight(rect.width * 0.5f), Color.cyan, 0);
            // EditorGUIUtility.labelWidth = prev;
            // --- Pop 操作等于上面
            GUIHelper.PopLabelWidth();
            // --- 错误示范
            // 不要把 GUILayout 和 EditorGUILayout 混合使用，可能导致 GUI 错乱
            // 编辑器绘制要以 EditorGUILayout 优先使用，除非实在没有办法达成目的，再去尝试 GUILayout
            // 把以下 GUILayout 显示后，会发现，当 Inspector 宽度缩小到一定程度后，
            // 右侧的 Slider 的 Label 会被挤出面板，导致显示不全
            // ---
            // 错误原因分析得出，因为 GUILayout 的自动布局会影响一行中的宽度
            // 具体原因就是 GUILayout.Label("XXX"); 中 XXX text 的值，所需要的宽度超过了 EditorGUILayout 的最小宽度，会强行拉高
            // 就会导致 Inspector 的显示宽度扩大，但是编辑器中我们实际给它的宽度没有那么多，我们看到的是一个假宽度
            // 例如，目前只能显示 100 宽度，但是 text 为 120，就会让面板宽度扩大到 120，但是实际给它的宽度只有 100，导致右侧的 Slider 的 Label 被挤出面板
            // 如果只使用 EditorGUILayout，就不会有这个问题，它的宽度会和我们能够看到的宽度相匹配
            // ---
            // 只要 GUILayout.Label("SmartValue"); 这个 XXX 的宽度足够小，则不会影响
            // GUILayout.Label("青色 cyan 表示 Y 的值的 Slider 的全部空间，内容如上。。。。。。。。");
            // 可以使用这行代码测试，修改 text 的长度
            // ---
            // EditorGUILayout.LabelField 只写一个参数是对应 GUILayout.Label ，而且不会影响一行的宽度
            EditorGUILayout.Space();
            var wordWrapLabelStyle = new GUIStyle(GUI.skin.label)
            {
                wordWrap = true
            };
            EditorGUI.PrefixLabel(EditorGUILayout.GetControlRect(),
                new GUIContent("Rect 指示框（会略大于实际的 Rect）"), SirenixGUIStyles.BoldTitle);
            // EditorGUILayout.LabelField($"左侧 Slider 的 Rect 为 {rect.AlignLeft(rect.width * 0.5f)}", wordWrapLabelStyle);
            // EditorGUILayout.LabelField("右侧 Slider 的 Rect 为 " + rect.AlignRight(rect.width * 0.5f), wordWrapLabelStyle);
            EditorGUILayout.LabelField("红色 表示 EditorGUILayout.GetControlRect() 一行的空间，一行通常由标签 Label 和控件 Control 两部分组成",
                wordWrapLabelStyle);
            EditorGUILayout.LabelField("黄色 表示 EditorGUI.PrefixLabel 的空间，宽度使用默认的 EditorGUIUtility.labelWidth",
                wordWrapLabelStyle);
            EditorGUILayout.LabelField("绿色 表示一行中减去 PrefixLabel 标签，剩下的 Control 的空间", wordWrapLabelStyle);
            // 创建一个带有自动换行的 GUIStyle
            EditorGUILayout.LabelField(
                "洋红色 表示 X 的值的 Slider 的全部空间，包括 Label 和后面的滑动条，Label 宽度使用 EditorGUIUtility.labelWidth，此处进行了临时修改",
                wordWrapLabelStyle
            );
            EditorGUILayout.LabelField(
                "青色 cyan 表示 Y 的值的 Slider 的全部空间，内容如上",
                wordWrapLabelStyle
            );
            EditorGUILayout.Space();
            EditorGUI.PrefixLabel(EditorGUILayout.GetControlRect(),
                new GUIContent("SmartValue"), SirenixGUIStyles.BoldTitle);
            ValueEntry.SmartValue = value;
            EditorGUILayout.LabelField(
                $"ValueEntry.SmartValue 对应的对象的 X 值为 {ValueEntry.SmartValue.x.ToString(CultureInfo.InvariantCulture)}",
                wordWrapLabelStyle);
            EditorGUILayout.LabelField(
                $"ValueEntry.SmartValue 对应的对象的 Y 值为 {ValueEntry.SmartValue.y.ToString(CultureInfo.InvariantCulture)}",
                wordWrapLabelStyle);
        }
    }

#endif
#endif
}