using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HowToSerializeInterfaces.OdinInterfaceReferencePractice.Scripts.Editor
{
    public class OdinInterfaceReferenceDrawer<TReference, TInterface, TObject>
        : OdinValueDrawer<TReference>
        where TReference : OdinInterfaceReference<TInterface, TObject>
        where TInterface : class
        where TObject : UnityEngine.Object
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            TReference interfaceReference = this.ValueEntry.SmartValue;
            Type interfaceType = typeof(TInterface);
            var rect = EditorGUILayout.GetControlRect();
            // DrawRectWithBorder(new Rect(rect.xMax - rect.width * 0.6f, rect.y, rect.width * 0.6f, rect.height),
            //     Color.cyan, 1);
            // 一行分成四段，字段标签，接口类型提示标签，引用选择框，约束提示
            var labelWidth = SirenixGUIStyles.Label.CalcWidth(label ?? new GUIContent());
            var firstRect = new Rect(rect.x, rect.y, labelWidth, rect.height);
            string requireText = $" [{typeof(TInterface).Name}] ";
            GUIContent requireContent = new GUIContent($"{requireText}");
            var secondRect = new Rect(firstRect.xMax, rect.y,
                SirenixGUIStyles.Label.CalcWidth(requireContent),
                rect.height);
            var rightRect = new Rect(rect.xMax - rect.height, rect.y, rect.height, rect.height);
            var leftRectWidth = (rect.xMax - secondRect.xMax - rightRect.width - 30f) > rect.width * 0.6f
                ? rect.width * 0.6f
                : rect.xMax - secondRect.xMax - rightRect.width - 30f;
            var leftRect = new Rect(rect.xMax - rightRect.width - 5f - leftRectWidth, rect.y, leftRectWidth,
                rect.height);
            // DrawRectWithBorder(leftRect, Color.yellow, 1);
            // 开始绘制
            GUI.Label(firstRect, label ?? new GUIContent(), SirenixGUIStyles.Label);
            GUI.Label(secondRect, requireContent, SirenixGUIStyles.HighlightedLabel);
            if (interfaceReference.UnderlyingObject)
            {
                Type targetType = interfaceReference.UnderlyingObject.GetType();
                if (!interfaceType.IsAssignableFrom(targetType))
                {
                    SirenixEditorGUI.DrawSolidRect(rightRect, Color.red);
                    interfaceReference.UnderlyingObject = null;
                }
                else
                {
                    SirenixEditorGUI.DrawSolidRect(rightRect, Color.green);
                }
            }
            else
            {
                SirenixEditorGUI.ErrorMessageBox(
                    $"OdinInterfaceReference 提示: 刚刚选择的实例对象没有实现接口 {interfaceType.Name}，选择 Project 中的脚本文件是 MonoScript 类型,不是实例对象",
                    false);
                SirenixEditorGUI.DrawSolidRect(rightRect, Color.red);
            }

            interfaceReference.UnderlyingObject =
                SirenixEditorFields.UnityObjectField(leftRect,
                    new GUIContent(),
                    interfaceReference.UnderlyingObject, typeof(TObject),
                    true) as TObject;
        }

        private static void DrawRectWithBorder(Rect rect, Color borderColor, float borderWidth)
        {
            // 绘制边框
            Handles.DrawSolidRectangleWithOutline(
                new Rect(rect.x - borderWidth, rect.y - borderWidth, rect.width + borderWidth * 2,
                    rect.height + borderWidth * 2), Color.clear, borderColor);

            // 绘制填充（可选）
            // GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.StretchToFill, false, 0, Color.clear, 0, 0);
        }
    }
}