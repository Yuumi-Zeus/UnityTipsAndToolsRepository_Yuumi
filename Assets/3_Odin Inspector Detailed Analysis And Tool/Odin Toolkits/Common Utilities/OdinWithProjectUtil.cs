using System;
using System.IO;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.Utilities;
using UnityEditor;
#endif

namespace Odin_Toolkits.Common_Utilities
{
    public static class OdinWithProjectUtil
    {
        /// <summary>
        /// 根据类型获取单个资源的路径，更加通用，非脚本类型
        /// </summary>
        /// <typeparam name="T"> 资源类型 </typeparam>
        /// <returns> 字符串路径 </returns>
        public static string FindScriptableObjectAssetPath<T>() where T : ScriptableObject
        {
#if UNITY_EDITOR
            var assetPath = AssetDatabase.FindAssets("t:" + typeof(T))
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault();
            return !string.IsNullOrEmpty(assetPath) ? assetPath : default;
#endif
        }

        public static bool CheckScriptableObjectExist<T>(string filePath = "") where T : ScriptableObject
        {
#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(filePath))
            {
                return AssetDatabase.AssetPathToGUID(filePath) != null;
            }

            return AssetDatabase.FindAssets("t:" + typeof(T)).Length > 0;
#endif
        }
#if UNITY_EDITOR
        /// <summary>
        /// 查找脚本，并选择到这个脚本文件
        /// 注意：查找的是 MonoScript，而不是 ScriptableObject，加载的也是 MonoScript
        /// </summary>
        public static MonoScript FindAndSelectedScript(string scriptName)
        {
            MonoScript foundMonoScript = null;
            var scriptAssetPath = AssetDatabase.FindAssets("t:MonoScript " + scriptName)
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(scriptAssetPath))
                foundMonoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptAssetPath);

            if (foundMonoScript != null)
            {
                Selection.activeObject = foundMonoScript;
            }

            return foundMonoScript;
        }

        /// <summary>
        /// 获取或创建一个单例 SO 资源，如果资源不存在则创建，如果有多个 SO 资源，则只返回第一个，并删除其他资源
        /// </summary>
        public static T GetScriptableObjectDeleteExtra<T>(string filePath) where T : ScriptableObject
        {
            T wantToAsset;
            var guids = AssetDatabase.FindAssets("t:" + typeof(T));
            var allPaths = new string[guids.Length];
            if (guids.Length > 0)
            {
                allPaths[0] = AssetDatabase.GUIDToAssetPath(guids[0]);

                // 只获取一个资源 0 号
                wantToAsset = AssetDatabase.LoadAssetAtPath<T>(allPaths[0]);
                if (wantToAsset == null) Debug.LogWarning("GetScriptableObjectDeleteExtra 中加载资源失败");

                // 删除从序号 1 开始的所有资源
                for (var i = 1; i < guids.Length; i++)
                {
                    // 能获得扩展名
                    allPaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
                    AssetDatabase.DeleteAsset(allPaths[i]);
                }

                AssetDatabase.Refresh();
                return wantToAsset;
            }

            wantToAsset = ScriptableObject.CreateInstance<T>();
            if (!filePath.EndsWith(".asset")) filePath = Path.Combine(filePath, ".asset");
            AssetDatabase.CreateAsset(wantToAsset, filePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return wantToAsset;
        }

        public static string GetUniqueFilePath(string partialFolderPath, string fileName, string extensionName)
        {
            var folderPath = GetInOdinToolkitsFolderPath(partialFolderPath);
            // Debug.Log("folderPath: " + folderPath);
            var filePathNoExtension = string.Join("/", folderPath, fileName);
            var filePath = string.Join(".", filePathNoExtension, extensionName);
            // Debug.Log("filePath: " + filePath);
            return filePath;
        }

        public static string GetOdinToolkitsFolderPath()
        {
            var assetsPath = Application.dataPath;
            // 查找所有匹配的路径
            var allFolders = Directory.EnumerateDirectories(assetsPath, "Odin*Toolkits",
                enumerationOptions: new EnumerationOptions()
                {
                    RecurseSubdirectories = true
                }).ToArray();
            foreach (var item in allFolders)
            {
                if (!item.Contains("Odin Toolkits")) continue;
                var path = item.Replace('\\', '/');
                path = path[(assetsPath.Length - "Asset/".Length)..];
                return path;
            }

            Debug.LogWarning("在项目中没有找到 Odin Toolkits 文件夹，请检查是否改名，注意中间为空格");
            return string.Empty;
        }

        static string GetInOdinToolkitsFolderPath(string partialPath)
        {
            var odinToolkitsFolderPath = GetOdinToolkitsFolderPath();
            return PathUtilities.Combine(odinToolkitsFolderPath, partialPath);
        }
#endif
    }
}