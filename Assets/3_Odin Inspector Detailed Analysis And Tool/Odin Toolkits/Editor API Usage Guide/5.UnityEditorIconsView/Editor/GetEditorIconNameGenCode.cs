using System.Collections.Generic;
using System.IO;
using Odin_Toolkits.Common_Utilities;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._5.UnityEditorIconsView.Editor
{
    public static class GetEditorIconNameGenCode
    {
        const string ScriptPath = "Assets/Odin使用示例集成/OdinEditor/Editor/5_UnityEditorIconsView/EditorIconNames";
        static readonly Dictionary<string, Texture2D> Texture2Ds = new();

        // [MenuItem("Zeus/生成所有UnityEditor图标名称代码")]
        public static void GenerateEditorIconNameGenCode()
        {
            // 要获取的所有图标名称
            string[] allIconNames = GetAllNames().ToArray();
            allIconNames =
              CollectionUtils.MergeAndRemoveDuplicates(allIconNames, UnityBuildInEditorIcons.EssentialIcons);
            var number = 0;
            // 创建脚本文件
            var script = new FileInfo(ScriptPath + ".cs");
            // 使用using语句打开写入流
            using var writer = script.CreateText();
            // 写入原始脚本内容
            writer.WriteLine("using Sirenix.OdinInspector.Editor;");
            writer.WriteLine("using Sirenix.Utilities;");
            writer.WriteLine("using Sirenix.Utilities.Editor;");
            writer.WriteLine("using UnityEditor;");
            writer.WriteLine();
            writer.WriteLine("namespace Odin使用示例集成.OdinEditor.Editor._5_UnityEditorIconsView");
            writer.WriteLine("{");
            writer.WriteLine("    public static class EditorIconNames");
            writer.WriteLine("    {");
            writer.WriteLine("        public static string[] AllUnityEditorIconNames =");
            writer.WriteLine("        {");
            foreach (string iconName in allIconNames)
            {
                writer.Write($"\"{iconName}\",");
                number++;
                if (number >= 5)
                {
                    writer.WriteLine();
                    number = 0;
                }
            }

            writer.WriteLine("        };");
            writer.WriteLine("    }");
            writer.WriteLine("}");
            writer.Flush();
            AssetDatabase.Refresh();
        }

        static List<string> GetAllNames()
        {
            var list = Resources.FindObjectsOfTypeAll<Texture2D>();
            foreach (var texture2D in list)
            {
                Texture2Ds.TryAdd(texture2D.name, texture2D);
            }

            var strList = new List<string>();
            foreach (var texture2D in Texture2Ds)
            {
                if (TryGetIcon(texture2D.Key))
                {
                    strList.Add(texture2D.Key);
                }
            }

            return strList;
        }

        static bool TryGetIcon(string iconName)
        {
            GUIContent valid = null;
            Debug.unityLogger.logEnabled = false;
            if (!string.IsNullOrEmpty(iconName)) valid = EditorGUIUtility.IconContent(iconName);
            Debug.unityLogger.logEnabled = true;
            return valid != null && valid.image != null;
        }
    }
}