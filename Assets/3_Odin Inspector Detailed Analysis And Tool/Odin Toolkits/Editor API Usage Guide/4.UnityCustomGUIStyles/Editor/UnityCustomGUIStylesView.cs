using System.Collections.Generic;
using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._4.UnityCustomGUIStyles.Editor
{
    public class UnityCustomGUIStylesView : OdinEditorWindow
    {
        Vector2 _scrollPosition = new(0, 0);

        string _search = "";

        GUIStyle _textStyle;

        static UnityCustomGUIStylesView _window;

        [MenuItem(OdinToolkitsConfig.OdinEditorAPIUsageGuidePath + "/" + "UnityCustomGUIStyles效果一览(简易版)")]
        static void OpenWindow()
        {
            _window = GetWindow<UnityCustomGUIStylesView>();
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            _window.Show();
        }

        protected override IEnumerable<object> GetTargets()
        {
            // Draws this instance using Odin
            yield return this;

            // Draw non-unity objects.
            // yield return GUI.skin.settings; // GUISettings is a regular class.

            // Or Unity objects.
            yield return GUI.skin; // GUI.Skin is a ScriptableObject
        }

        protected override void DrawEditor(int index)
        {
            object currentDrawingEditor = this.CurrentDrawingTargets[index];

            SirenixEditorGUI.Title(
                title: currentDrawingEditor.ToString(),
                subtitle: currentDrawingEditor.GetType().GetNiceFullName(),
                textAlignment: TextAlignment.Left,
                horizontalLine: true
            );
            if (index == 0)
            {
                if (_textStyle == null)
                {
                    _textStyle = new GUIStyle("HeaderLabel");

                    _textStyle.fontSize = 25;
                }

                GUILayout.BeginHorizontal("HelpBox");
                GUILayout.Label("结果如下：", _textStyle);
                GUILayout.FlexibleSpace();
                GUILayout.Label("Search:");
                _search = EditorGUILayout.TextField(_search);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal("PopupCurveSwatchBackground");
                GUILayout.Label("样式展示", _textStyle, GUILayout.Width(300));
                GUILayout.Label("名字", _textStyle, GUILayout.Width(300));
                GUILayout.EndHorizontal();
                foreach (var style in GUI.skin.customStyles)
                    if (style.name.ToLower().Contains(_search.ToLower()))
                    {
                        GUILayout.Space(15);
                        GUILayout.BeginHorizontal("PopupCurveSwatchBackground");
                        if (GUILayout.Button(style.name, style, GUILayout.Width(300)))
                        {
                            EditorGUIUtility.systemCopyBuffer = style.name;

                            Debug.LogError(style.name);
                        }

                        EditorGUILayout.SelectableLabel(style.name, GUILayout.Width(300));

                        GUILayout.EndHorizontal();
                    }
            }
            else
            {
                base.DrawEditor(index);
            }

            if (index != this.CurrentDrawingTargets.Count - 1)
            {
                SirenixEditorGUI.DrawThickHorizontalSeparator(15, 15);
            }
        }

        // protected override void DrawEditors()
        // {
        //     base.DrawEditors();
        //     if (_textStyle == null)
        //     {
        //         _textStyle = new GUIStyle("HeaderLabel");
        //
        //         _textStyle.fontSize = 25;
        //     }
        //
        //     GUILayout.BeginHorizontal("HelpBox");
        //     GUILayout.Label("结果如下：", _textStyle);
        //     GUILayout.FlexibleSpace();
        //     GUILayout.Label("Search:");
        //     _search = EditorGUILayout.TextField(_search);
        //     GUILayout.EndHorizontal();
        //     GUILayout.BeginHorizontal("PopupCurveSwatchBackground");
        //     GUILayout.Label("样式展示", _textStyle, GUILayout.Width(300));
        //     GUILayout.Label("名字", _textStyle, GUILayout.Width(300));
        //     GUILayout.EndHorizontal();
        //     foreach (var style in GUI.skin.customStyles)
        //         if (style.name.ToLower().Contains(_search.ToLower()))
        //         {
        //             GUILayout.Space(15);
        //             GUILayout.BeginHorizontal("PopupCurveSwatchBackground");
        //             if (GUILayout.Button(style.name, style, GUILayout.Width(300)))
        //             {
        //                 EditorGUIUtility.systemCopyBuffer = style.name;
        //
        //                 Debug.LogError(style.name);
        //             }
        //
        //             EditorGUILayout.SelectableLabel(style.name, GUILayout.Width(300));
        //
        //             GUILayout.EndHorizontal();
        //         }
        // }


        // 此处直接当成 OnGUI,但是不要删除 base.DrawEditors(); 这一部分实现 Odin 特性的绘制
        // protected override void DrawEditors()
        // {
        //     base.DrawEditors();
        //     GUILayout.BeginHorizontal("HelpBox");
        //     GUILayout.Space(30);
        //     _search = EditorGUILayout.TextField("", _search, "SearchTextField", GUILayout.MaxWidth(position.x / 3));
        //     GUILayout.Label("", "SearchCancelButtonEmpty");
        //     GUILayout.EndHorizontal();
        //     _scrollVector2 = GUILayout.BeginScrollView(_scrollVector2);
        //     foreach (GUIStyle style in GUI.skin.customStyles)
        //     {
        //         if (style.name.ToLower().Contains(_search.ToLower()))
        //         {
        //             DrawStyleItem(style);
        //         }
        //     }
        //
        //     GUILayout.EndScrollView();
        // }
        // ------------------------------------------------
        // void DrawStyleItem(GUIStyle style)
        // {
        //     GUILayout.BeginHorizontal("box");
        //     GUILayout.Space(40);
        //     EditorGUILayout.SelectableLabel(style.name);
        //     GUILayout.FlexibleSpace();
        //     EditorGUILayout.SelectableLabel(style.name, style);
        //     GUILayout.Space(40);
        //     EditorGUILayout.SelectableLabel("", style, GUILayout.Height(40), GUILayout.Width(40));
        //     GUILayout.Space(50);
        //     if (GUILayout.Button("复制到剪贴板"))
        //     {
        //         TextEditor textEditor = new TextEditor();
        //         textEditor.text = style.name;
        //         textEditor.OnFocus();
        //         textEditor.Copy();
        //     }
        //
        //     GUILayout.EndHorizontal();
        //     GUILayout.Space(10);
        // }
    }
}