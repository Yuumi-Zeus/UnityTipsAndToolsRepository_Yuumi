using Odin_Inspector_Detailed_Analysis_And_Tool.Common_Utilities;
using Sirenix.OdinInspector.Editor;
using Sirenix.Reflection.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using GUIContent = UnityEngine.GUIContent;

namespace HowToSerializeInterfaces.OdinInterfaceReferencePractice.Scripts.Editor
{
    /// <summary>
    /// 按 Odin 的绘制流程绘制 OdinInterfaceReference&lt;TInterface, TObject&gt; 类型 <br/>
    /// 继承 OdinValueDrawer&lt;TReference&gt; 使用泛型约束，可以对其子类实行同样的绘制 <br/>
    /// 以 public OdinInterfaceReference&lt;IDamageable&gt; reference; 为例
    /// </summary>
    /// <typeparam name="TReference">要进行绘制的基础类型，使其子类也按这个方法绘制</typeparam>
    /// <typeparam name="TInterface">接口约束</typeparam>
    /// <typeparam name="TObject">具体实例对象类型</typeparam>
    public class OdinInterfaceReferenceDrawer<TReference, TInterface, TObject>
        : OdinValueDrawer<TReference>
        where TReference : OdinInterfaceReference<TInterface, TObject>
        where TInterface : class
        where TObject : UnityEngine.Object
    {
        /// <summary>
        /// 重写该方法，替代原生的绘制逻辑，同时不调用 CallNextDrawer()，不进入绘制链，覆盖其他绘制逻辑
        /// </summary>
        /// <param name="label">这个标签的 text 值为字段名，例如: public OdinInterfaceReference&lt;IDamageable&gt; reference; <br/>
        /// Label 绘制时首字母大写，即 Reference </param>
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // --- 绘制时推荐使用自动布局 + 部分调整
            // 首先通过 Odin 的 SmartValue 获取当前值，也就是这个字段的值，即 reference; 
            var interfaceReference = ValueEntry.SmartValue;
            // 获取接口类型，此时可以直接从泛型约束中获取（使用 Unity 原生方式会比较麻烦）
            var interfaceType = typeof(TInterface);
            // 此时绘制一个 ErrorMessageBox，进行提示
            if (interfaceReference.UnderlyingObject == null)
            {
                SirenixEditorGUI.ErrorMessageBox(
                    $"请选择实现了 {interfaceType.Name} 接口的实例对象或 ScriptableObject 资产");
            }

            // 布局的关键 API
            // EditorGUILayout.GetControlRect(); 没有参数的情况下，默认会贴着上一个 Rect，直接开辟一行的空间。
            // GUILayoutUtility.GetLastRect(); 在自动布局的情况下，获取上一个的控件空间。
            // 使用 using 的写法等同于 EditorGUILayout.BeginHorizontal()
            // 开启一行布局，可以返回一个 Rect（通常不需要使用），直接开始接下来的绘制。
            // 内部封装了一个 GUILayout.BeginHorizontal();
            SirenixEditorGUI.BeginHorizontalPropertyLayout(new GUIContent($"{label} [{typeof(TInterface).Name}] "));
            {
                // Rect rect = GUILayoutUtility.GetLastRect();
                // OdinEditorDrawUtil.Debug.DrawRectWithBorder(rect, Color.yellow);
                // --- 设计目标是将一行分成四个部分
                // 字段名标签 Label，即 Reference
                // 接口类型提示标签 Label
                // 引用选择框，Odin 的引用选择框基于 Unity 原生，有新增部分，更好用，在 Odin 的绘制过程中，优先使用 Odin 封装的功能
                // 提示标签（图标）
                // ---
                // if (label != null)
                //     // 绘制字段名标签，在 Odin 绘制中，label 可能会为 null
                //     // 此说法源自官方案例 CustomStructDrawer 中
                //     // 原文 In Odin, labels are optional and can be null, so we have to account for that.
                //     // 如果不判断，在绘制数组或者列表时，会直接报错，因为 Odin 对于集合类型的绘制是默认舍弃字段名这个 Label 的
                //     // GUILayout 自动布局
                //     // 可以使用 SirenixGUIStyles 来获取一些成熟的样式（经过 Odin 封装的），例如 Label，和 Unity 内置差异不大，
                //     // 但是在 Odin 的绘制过程中，使用 Odin 封装功能优先
                //     // GUILayoutOptions 内部封装了一个 GUILayoutOption 数组，可以使用链式调用，简化代码
                //     GUILayout.Label(label, SirenixGUIStyles.Label, GUILayoutOptions.Height(22f));
                // Rect rect = GUILayoutUtility.GetLastRect();
                // OdinEditorDrawUtil.Debug.DrawRectWithBorder(rect, Color.yellow);
                // 接口类型提示标签
                // Odin 的绘制中，最好注意在字符串的两侧保留空格，有可能会裁切，这样可以保证全部显示
                // var requireText = $" [{typeof(TInterface).Name}] ";
                // GUILayout.Label(requireText, SirenixGUIStyles.HighlightedLabel, GUILayoutOptions.Height(22f));
                // rect = GUILayoutUtility.GetLastRect();
                // OdinEditorDrawUtil.Debug.DrawRectWithBorder(rect, Color.yellow);
                // 引用选择框
                // SirenixEditorFields.UnityObjectField
                interfaceReference.UnderlyingObject = (TObject)SirenixEditorFields.UnityObjectField(
                    new GUIContent(),
                    interfaceReference.UnderlyingObject,
                    typeof(TObject),
                    true);
                // 提示标签(图标)
                // 目标是绘制一个圆点
                // 此时的思路是使用自动布局的 Box 开辟一个 Rect
                // 然后获取这个 Rect，再绘制纯色圆点，覆盖这个 Box
                GUILayout.Box(new GUIContent(), SirenixGUIStyles.Label, GUILayoutOptions
                    .Height(22F).MaxWidth(25F));
                var lastRect = GUILayoutUtility.GetLastRect();
                const float squareSize = 13f;
                var innerRect = OdinEditorDrawUtil.Calculate.GetInnerRectFromRect(lastRect, InnerRectType.Center,
                    squareSize, squareSize);
                // OdinEditorDrawUtil.Debug.DrawRectWithBorder(lastRect, Color.cyan);
                // OdinEditorDrawUtil.Debug.DrawRectWithBorder(innerRect, Color.yellow);
                if (interfaceReference.UnderlyingObject != null)
                {
                    var targetType = interfaceReference.UnderlyingObject.GetType();
                    if (!interfaceType.IsAssignableFrom(targetType))
                    {
                        SirenixEditorGUI.DrawRoundRect(innerRect, SirenixGUIStyles.RedErrorColor, squareSize / 2);
                        interfaceReference.UnderlyingObject = null;
                    }
                    else
                    {
                        SirenixEditorGUI.DrawRoundRect(innerRect, SirenixGUIStyles.GreenValidColor, squareSize / 2);
                    }
                }
                else
                {
                    SirenixEditorGUI.DrawRoundRect(innerRect, SirenixGUIStyles.RedErrorColor, squareSize / 2);
                }
            }
            // EditorGUILayout.EndHorizontal();
            SirenixEditorGUI.EndHorizontalPropertyLayout();
            Rect outRect = GUILayoutUtility.GetLastRect();
            OdinEditorDrawUtil.OdinDebug.DrawRectOutlineWithBorder(outRect, Color.green);
            // 结束后更新 smartValue
            ValueEntry.SmartValue = interfaceReference;
        }
    }
}