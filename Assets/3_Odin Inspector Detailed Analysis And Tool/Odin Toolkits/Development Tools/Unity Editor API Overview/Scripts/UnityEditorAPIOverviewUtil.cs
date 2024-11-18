using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Odin_Toolkits.Common_Utilities;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Development_Tools._1_Unity_Editor_API_Overview.Scripts
{
    public static class UnityEditorAPIOverviewUtil
    {
        // 获取所有 Sirenix 相关的程序集一共有九个
        // 1. Sirenix.OdinInspector.Modules.UnityMathematics, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
        // 2. Sirenix.Reflection.Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
        // 3. Sirenix.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // 4. Sirenix.Serialization.Config, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // 5. Sirenix.OdinInspector.Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // 6. Sirenix.OdinInspector.Attributes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // 7. Sirenix.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // 8. Sirenix.Utilities.Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // 9. Sirenix.Serialization.RuntimeEmitted, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
        // --- 
        // 只需要和用户自定义编辑器最相关的，或者说用户有比较多机会使用的
        // 即使只选择 2 个 Utility，共有 245 个类，依旧非常多
        // 只能手动挑选常用的类型，不断补充
        public static List<string> SirenixEditorAssemblies => new List<string>()
        {
            "Sirenix.Utilities.Editor",
            "Sirenix.Utilities"
        };

        public static UnityEditorAPIOverviewDatabase OverviewDatabase
        {
            get
            {
                var database = OdinWithProjectUtil.GetScriptableObjectDeleteExtra<UnityEditorAPIOverviewDatabase>(
                    OdinWithProjectUtil.GetUniqueFilePath(
                        "Development Tools/1_Unity Editor API Overview/Database",
                        nameof(UnityEditorAPIOverviewDatabase), "asset"));
                return database;
            }
        }


        static string GetUnityVersionString() => "Unity_" + UnityVersion.Major;

        public static EditorClassAPIData CreateEditorClassAPIData(Type type, string folderPath)
        {
            if (type == null)
            {
                return default;
            }

            var fullName = type.FullName;
            // 查找是否存在同类型同版本的 Asset
            var hasSameAsset = OverviewDatabase.allEditorClassAPIData.FirstOrDefault(item =>
                item.className == type.Name && item.unityClassAPIVersion == GetUnityVersionString()) != default;
            if (hasSameAsset)
            {
                return OverviewDatabase.allEditorClassAPIData.FirstOrDefault(item =>
                    item.className == type.Name && item.unityClassAPIVersion == GetUnityVersionString());
            }

            var apiData = ScriptableObject.CreateInstance<EditorClassAPIData>();
            if (fullName != null && fullName.Contains("Sirenix"))
            {
                apiData.belongToCollection = "Sirenix";
            }
            else
            {
                apiData.belongToCollection = "Unity Build In";
            }

            apiData.assemblyQualifiedName = type.AssemblyQualifiedName;
            apiData.className = type.Name;
            apiData.unityClassAPIVersion = GetUnityVersionString();
            apiData.Init();
            // 对类型成员进行遍历分类
            apiData.ReadClassInfo();
            // ---
            // 生成对应 asset 文件
            var filePath = string.Join('/', folderPath, $"/{apiData.className}_{GetUnityVersionString()}.asset");
            AssetDatabase.CreateAsset(apiData, filePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Create EditorClassAPIData: " + apiData.className);
            return apiData;
        }

#if UNITY_EDITOR
        public static List<string> FindAllEditorClassAPIDataPath()
        {
            var assetPathList = AssetDatabase.FindAssets("t:" + typeof(EditorClassAPIData))
                .Select(AssetDatabase.GUIDToAssetPath)
                .ToList();
            // Debug.Log("查询到 assetPathList 的长度为：" + assetPathList.Count);
            return assetPathList.Count > 0 ? assetPathList : new List<string>();
        }
#endif
        static Assembly[] GetSirenixAllAssemblies()
        {
            const string sirenixAssemblyName = "Sirenix";
            return OdinWithReflectionUtil.GetAssembliesOfNameContainString(sirenixAssemblyName);
        }

        #region 过时

        // Select 是选择执行某一个方法的 Linq 扩展方法
        // Where 才是选择符合条件的元素
        public static Assembly[] GetSirenixEditorAssemblies() =>
            SirenixEditorAssemblies.Select(Assembly.Load).ToArray();

        public static Type[] GetSirenixEditorAssembliesAllTypes()
        {
            var assemblies = GetSirenixEditorAssemblies();
            return assemblies.SelectMany(assembly => assembly.GetTypes()).ToArray();
        }

        public static HashSet<string> GetTotalNamespaceCountInSirenixEditorAssemblies()
        {
            var assemblies = GetSirenixEditorAssemblies();
            var totalNamespaces = new HashSet<string>();

            foreach (var assembly in assemblies)
            {
                var namespaces = OdinWithReflectionUtil.GetNamespacesInAssembly(assembly);
                foreach (var ns in namespaces)
                {
                    totalNamespaces.Add(ns);
                }
            }

            return totalNamespaces;
        }

        #endregion
    }
}