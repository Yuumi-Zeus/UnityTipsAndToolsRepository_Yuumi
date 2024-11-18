using System;
using System.Collections.Generic;
using System.Linq;
using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._5.UnityEditorIconsView.Editor
{
    public class OdinUnityEditorIcons : OdinEditorWindow
    {
        public static void OpenWindow()
        {
            var win = GetWindow<OdinUnityEditorIcons>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        [PropertyOrder(0)] [Title("UnityEditor Icons")]
        public string search = "";

        bool DoSearch => !string.IsNullOrWhiteSpace(search) && search != "";

        static string[] IconList = UnityEditorIconsView.AllUnityEditorIconNames;

        protected override void OnEnable()
        {
            base.OnEnable();
            // Unity 版本可能不同，更新图标列表
            IconList = CollectionUtils.MergeAndRemoveDuplicates(IconList,
                (from x in Resources.FindObjectsOfTypeAll<Texture2D>()
                    let icoContent = GetIcon(x.name)
                    where icoContent != null
                    where !IconList.Contains(x.name)
                    select x.name).ToArray());
            // 再过滤一次，确保所有图标都是有效的
            IconList = IconList.Where(x => GetIcon(x) != null).ToArray();
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        static GUIContent iconSelected;

        static GUIStyle iconButtonStyle;
        static GUIStyle iconPreviewBlack;
        static GUIStyle iconPreviewWhite;
        static bool viewBigIcons = true;
        static bool darkPreview = true;
        Vector2 scroll;
        int buttonSize = 70;

        static List<GUIContent> iconContentListAll;
        static List<GUIContent> iconContentListSmall;
        static List<GUIContent> iconContentListBig;
        static List<string> iconMissingNames;

        [PropertyOrder(30)]
        [TitleGroup("UnityEditor Icons Preview")]
        [Button("小于 36*36 的图标", ButtonSizes.Large)]
        void SmallIcon() { }

        [PropertyOrder(30)]
        [TitleGroup("UnityEditor Icons Preview")]
        [Button("大于 36*36 图标", ButtonSizes.Large)]
        void BigIcon() { }

        [PropertyOrder(30)]
        [TitleGroup("UnityEditor Icons Preview")]
        [OnInspectorGUI]
        void Icons()
        {
            float ppp = EditorGUIUtility.pixelsPerPoint;
            InitCategoryIcons();
            GUILayout.Space(10);
            buttonSize = viewBigIcons ? 70 : 40;
            // scrollbar_width = ~ 12.5
            float render_width = Screen.width / ppp - 13f;
            int gridW = Mathf.FloorToInt(render_width / buttonSize);
            float margin_left = (render_width - buttonSize * gridW) / 2;

            int row = 0, index = 0;

            List<GUIContent> iconList;

            if (DoSearch)
                iconList = iconContentListAll.Where(x => x.tooltip.ToLower()
                    .Contains(search.ToLower())).ToList();
            else iconList = viewBigIcons ? iconContentListBig : iconContentListSmall;

            while (index < iconList.Count)
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Space(margin_left);

                    for (var i = 0; i < gridW; ++i)
                    {
                        int k = i + row * gridW;

                        var icon = iconList[k];

                        if (GUILayout.Button(icon,
                                iconButtonStyle,
                                GUILayout.Width(buttonSize),
                                GUILayout.Height(buttonSize)))
                        {
                            EditorGUI.FocusTextInControl("");
                            iconSelected = icon;
                        }

                        index++;

                        if (index == iconList.Count) break;
                    }
                }

                row++;
            }

            GUILayout.Space(10);

            if (iconSelected == null) return;

            GUILayout.FlexibleSpace();

            using (new GUILayout.HorizontalScope(EditorStyles.helpBox, GUILayout.MaxHeight(viewBigIcons ? 140 : 120)))
            {
                using (new GUILayout.VerticalScope(GUILayout.Width(130)))
                {
                    GUILayout.Space(2);

                    GUILayout.Button(iconSelected,
                        darkPreview ? iconPreviewBlack : iconPreviewWhite,
                        GUILayout.Width(128), GUILayout.Height(viewBigIcons ? 128 : 40));

                    GUILayout.Space(5);

                    darkPreview = GUILayout.SelectionGrid(
                        darkPreview ? 1 : 0, new[] { "Light", "Dark" },
                        2, EditorStyles.miniButton) == 1;

                    GUILayout.FlexibleSpace();
                }

                GUILayout.Space(10);

                using (new GUILayout.VerticalScope())
                {
                    var s = $"Size: {iconSelected.image.width}x{iconSelected.image.height}";
                    s += "\nIs Pro Skin Icon: " +
                         (iconSelected.tooltip.IndexOf("d_", StringComparison.Ordinal) == 0 ? "Yes" : "No");
                    s += $"\nTotal {iconContentListAll.Count} icons";
                    GUILayout.Space(5);
                    EditorGUILayout.HelpBox(s, MessageType.None);
                    GUILayout.Space(5);
                    EditorGUILayout.TextField("EditorGUIUtility.IconContent(\"" + iconSelected.tooltip + "\")");
                    GUILayout.Space(5);
                    // if (GUILayout.Button("Copy to clipboard", EditorStyles.miniButton))
                    //     EditorGUIUtility.systemCopyBuffer = iconSelected.tooltip;
                    // if (GUILayout.Button("Save icon to file ...", EditorStyles.miniButton))
                    //     SaveIcon(iconSelected.tooltip);
                }

                GUILayout.Space(10);

                if (GUILayout.Button("X", GUILayout.ExpandHeight(true))) iconSelected = null;
            }

            void InitCategoryIcons()
            {
                if (iconContentListSmall != null) return;

                iconButtonStyle = new GUIStyle(EditorStyles.miniButton)
                {
                    margin = new RectOffset(0, 0, 0, 0),
                    fixedHeight = 0
                };

                iconPreviewBlack = new GUIStyle(iconButtonStyle);
                AllTheTextures(ref iconPreviewBlack, Texture2DPixel(new Color(0.15f, 0.15f, 0.15f)));

                iconPreviewWhite = new GUIStyle(iconButtonStyle);
                AllTheTextures(ref iconPreviewWhite, Texture2DPixel(new Color(0.85f, 0.85f, 0.85f)));

                iconMissingNames = new List<string>();
                iconContentListSmall = new List<GUIContent>();
                iconContentListBig = new List<GUIContent>();
                iconContentListAll = new List<GUIContent>();

                for (var i = 0; i < IconList.Length; ++i)
                {
                    var ico = GetIcon(IconList[i]);

                    if (ico == null)
                    {
                        iconMissingNames.Add(IconList[i]);
                        continue;
                    }

                    ico.tooltip = IconList[i];

                    iconContentListAll.Add(ico);

                    if (!(ico.image.width <= 36 || ico.image.height <= 36))
                        iconContentListBig.Add(ico);
                    else iconContentListSmall.Add(ico);
                }
            }
        }

        /// <summary>
        /// 使用给定的纹理对一个 GUIStyle 进行设置，
        /// </summary>
        /// <param name="s">要修改的 GUI样式</param>
        /// <param name="t">要应用的纹理</param>
        static void AllTheTextures(ref GUIStyle s, Texture2D t)
        {
            // 将给定的纹理应用到GUI样式的所有状态的背景属性上
            s.hover.background = s.onHover.background = s.focused.background = s.onFocused.background =
                s.active.background = s.onActive.background = s.normal.background = s.onNormal.background = t;

            // 将给定的纹理数组应用到 GUI 样式的所有状态的缩放背景属性上
            s.hover.scaledBackgrounds = s.onHover.scaledBackgrounds = s.focused.scaledBackgrounds =
                s.onFocused.scaledBackgrounds = s.active.scaledBackgrounds = s.onActive.scaledBackgrounds =
                    s.normal.scaledBackgrounds = s.onNormal.scaledBackgrounds = new[] { t };
        }

        static Texture2D Texture2DPixel(Color c)
        {
            var t = new Texture2D(1, 1);
            t.SetPixel(0, 0, c);
            t.Apply();
            return t;
        }

        GUIContent GetIcon(string iconName)
        {
            GUIContent valid = null;
            Debug.unityLogger.logEnabled = false;
            if (!string.IsNullOrEmpty(iconName)) valid = EditorGUIUtility.IconContent(iconName);
            Debug.unityLogger.logEnabled = true;
            return valid?.image == null ? null : valid;
        }
    }
}