using System.Collections.Generic;
using System.Linq;
using Odin_Toolkits.Common_Utilities;
using Odin_Toolkits.Custom_Utilities.Attributes;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Development_Tools._1_Unity_Editor_API_Overview.Scripts.Editor
{
    public class UnityEditorAPIOverviewWindows : OdinMenuEditorWindow
    {
        [ForStringDisplayAsStringAlignLeftRichText(16)]
        public string unityVersionMajor = "当前 Unity 版本为: " + UnityVersion.Major;

        static UnityEditorAPIOverviewDatabase OverviewDatabase => UnityEditorAPIOverviewUtil.OverviewDatabase;

        protected override void OnEnable()
        {
            base.OnEnable();
            MenuWidth = 220;
            ResizableMenuWidth = true;
            WindowPadding = new Vector4(10, 10, 10, 10);
            DrawUnityEditorPreview = true;
            DefaultEditorPreviewHeight = 20;
            UseScrollView = true;
        }

        [Button("生成 Data 文件", ButtonSizes.Large)]
        void FindAllEditorClassAPIData()
        {
            foreach (var type in OverviewDatabase.SirenixEditorClassRecommendedToUse)
                UnityEditorAPIOverviewUtil.CreateEditorClassAPIData(type,
                    OdinWithProjectUtil.GetOdinToolkitsFolderPath() +
                    "/Development Tools/1_Unity Editor API Overview/ClassAPIDataAssets");

            foreach (var type in OverviewDatabase.UnityEditorClassRecommendedToUse)
                UnityEditorAPIOverviewUtil.CreateEditorClassAPIData(type,
                    OdinWithProjectUtil.GetOdinToolkitsFolderPath() +
                    "/Development Tools/1_Unity Editor API Overview/ClassAPIDataAssets");
        }

        [MenuItem(OdinToolkitsConfig.OdinDevelopmentToolMenuItemPath + "/" + "Unity Editor API Overview")]
        static void OpenWindow()
        {
            var win = GetWindow<UnityEditorAPIOverviewWindows>();
            win.titleContent = new GUIContent("Unity 编辑器相关 API 查询器",
                EditorIcons.OdinInspectorLogo);
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 600);
            win.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false)
            {
                // {
                //     "主界面", this
                // },
                {
                    "Sirenix", null, EditorIcons.OdinInspectorLogo
                },
                {
                    "Unity Build In", null, EditorGUIUtility.IconContent("UnityLogo").image
                }
            };


            foreach (var apiData in OverviewDatabase.allEditorClassAPIData)
            {
                var path = apiData.belongToCollection + "/" + apiData.className;
                switch (apiData.className)
                {
                    case "SirenixEditorGUI":
                    case "SirenixEditorFields":
                    case "SirenixGUIStyles":
                    case "EditorIcons":
                    case "GUILayout":
                    case "GUILayoutUtility":
                    case "GUI":
                    case "EditorGUILayout":
                    case "EditorGUI":
                    case "EditorApplication":
                    case "EditorUtility":
                        tree.Add(path, apiData, SdfIconType.LightbulbFill);
                        break;
                    default:
                        tree.Add(path, apiData);
                        break;
                }
            }

            tree.Config.DrawSearchToolbar = true;
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}