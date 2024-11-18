using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Odin_Toolkits.Common_Utilities
{
    /// <summary>
    /// ZeusFramework 框架工具集
    /// </summary>
    public static partial class ProjectUtils
    {
        /// <summary>
        /// 有关脚本资源(MonoScript)和类(Type)的操作集合，部分仅在编辑器状态可用<br />
        /// 例如：查找脚本、查找脚本所在路径，获取子类脚本等
        /// </summary>
        public static class Scripts
        {
            #region 脚本文件

            /// <summary>
            /// 通过脚本名字找到脚本路径，同名脚本可能会找错
            /// </summary>
            /// <param name="scriptName"> </param>
            /// <returns> </returns>
            public static string FindScriptPath(string scriptName)
            {
#if UNITY_EDITOR
                var scriptAssetPath = AssetDatabase.FindAssets("t:MonoScript " + scriptName)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .FirstOrDefault();
                return !string.IsNullOrEmpty(scriptAssetPath) ? scriptAssetPath : null;
#else
            LogModule.ZeusError("Script.FindScriptPath() 仅在编辑器下可用");
            return string.Empty;
#endif
            }
#if UNITY_EDITOR
            /// <summary>
            /// 查找脚本，并选择到这个脚本文件
            /// 注意：查找的是 MonoScript，而不是 ScriptableObject，加载的也是 MonoScript
            /// </summary>
            /// <param name="scriptName"> </param>
            /// <returns> </returns>
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
                else { }

                return foundMonoScript;
            }
#endif

            #endregion

            #region Type 类型封装

            // public static List<Type> FindIsSubClassOf(Type abstractType)
            // {
            //     var assembly = Assembly.GetExecutingAssembly();
            //     var subclasses = assembly.GetTypes().Where(t => t.IsSubclassOf(abstractType)).ToList();
            //     foreach (var subClass in subclasses)
            //         return subclasses;
            //
            //     return subclasses;
            // }

            /// <summary>
            /// 查找提供的文件夹中是否存在继承了抽象类的子类，非泛型
            /// </summary>
            /// <param name="abstractType"> 抽象基类 </param>
            /// <param name="folderPath"> 返回带后缀名的脚本路径 </param>
            public static string FindIsSubClassOfInFolder(Type abstractType, string folderPath)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var subclasses = assembly.GetTypes().Where(t => t.IsSubclassOf(abstractType)).ToList();
                foreach (var subClass in subclasses)
                {
                    var scriptPath = Path.Combine(folderPath, subClass.Name + ".cs");
                    if (!File.Exists(scriptPath)) continue;

                    return Path.Combine(folderPath, subClass.Name + ".cs");
                }

                return null;
            }

            /// <summary>
            /// 查找提供的文件夹中是否存在继承了抽象类的子类，是抽象泛型类，泛型只有一个 T
            /// </summary>
            /// <param name="abstractType"> 泛型抽象基类，一个泛型参数 </param>
            /// <param name="folderPath"> 文件夹路径 </param>
            /// <returns> 返回带后缀名的脚本路径 </returns>
            public static string FindIsGenericSubClassOfInFolderReturnPath(Type abstractType, string folderPath)
            {
                var subTypes = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true }
                                && t.BaseType.GetGenericTypeDefinition() == abstractType &&
                                t.BaseType.GetGenericArguments()[0] == t)
                    .ToList();

                foreach (var subType in subTypes)
                {
                    // Debug.Log(subType.FullName);
                    var scriptPath = Path.Combine(folderPath, subType.Name + ".cs");
                    // Debug.Log(scriptPath);
                    if (!File.Exists(scriptPath)) continue;
                    return Path.Combine(folderPath, subType.Name + ".cs");
                }

                return null;
            }

            /// <summary>
            /// 查找提供的文件夹中是否存在继承了抽象类的子类，是抽象泛型类，泛型只有一个 T
            /// </summary>
            /// <param name="abstractType"> </param>
            /// <param name="folderPath"> </param>
            /// <returns> 返回类名 </returns>
            public static string FindIsGenericSubClassOfInFolderReturnName(Type abstractType, string folderPath)
            {
                var subTypes = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true }
                                && t.BaseType.GetGenericTypeDefinition() == abstractType &&
                                t.BaseType.GetGenericArguments()[0] == t)
                    .ToList();
                foreach (var subType in subTypes)
                {
                    var scriptPath = Path.Combine(folderPath, subType.Name + ".cs");
                    if (!File.Exists(scriptPath)) continue;

                    return subType.Name;
                }

                return null;
            }

            /// <summary>
            /// 返回当前项目中继承了该类的子类，是抽象泛型类，泛型只有一个 T
            /// </summary>
            public static string[] GetIsGenericSubClassOfInProjectReturnFullName(Type absType)
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => assembly.FullName.Contains("Assembly-CSharp"))
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true }
                                && t.BaseType.GetGenericTypeDefinition() == absType &&
                                t.BaseType.GetGenericArguments()[0] == t)
                    .Select(type => type.FullName)
                    .ToArray();
            }

            public static Type GetScriptFullType(string nameSpace, string typeName)
            {
                try
                {
                    return Type.GetType(nameSpace + "." + typeName);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            #endregion
        }
    }
}