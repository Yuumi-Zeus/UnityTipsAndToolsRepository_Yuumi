using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Common_Utilities
{
    /// <summary>
    /// ZeusFramework Project Util 关于工程文件的操作类 <br />
    /// 比如文件夹，资源
    /// </summary>
    public static partial class ProjectUtils
    {
        #region 文件及文件夹，路径

        /// <summary>
        /// 如果不存在这个文件夹，就创建它，创建成功返回 true
        /// </summary>
        /// <param name="folderPath"> 文件夹路径 </param>
        public static bool TryCreateFolder(string folderPath)
        {
            if (Directory.Exists(folderPath)) return false;
            Directory.CreateDirectory(folderPath);
            return true;
        }

        /// <summary>
        /// 尝试删除指定的原始文件夹并在相同位置创建新的文件夹。
        /// </summary>
        /// <param name="folderPath">需要被处理的文件夹路径。</param>
        /// <returns>如果文件夹被成功删除并重新创建，则返回true；如果文件夹不存在且被成功创建，则返回false。</returns>
        public static bool TryDeleteOriginalFolderAndCreateNew(string folderPath)
        {
            // 检查文件夹是否存在，如果存在，则删除并重新创建
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true); // 删除文件夹及其内容
                Directory.CreateDirectory(folderPath); // 重新创建文件夹
                return true;
            }

            // 如果文件夹不存在，直接创建新的文件夹
            Directory.CreateDirectory(folderPath);
            return false;
        }

        /// <summary>
        /// 尝试删除原有的文件
        /// </summary>
        /// <param name="filePath"> 文件路径 </param>
        public static bool TryDeleteOriginalFile(string filePath)
        {
            if (!File.Exists(filePath)) return false;

            File.Delete(filePath);
            File.Delete(filePath + ".meta");
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
            return true;
        }

        /// <summary>
        /// 写入文件，通过字节数组
        /// </summary>
        /// <param name="filePath"> </param>
        /// <param name="data"> </param>
        public static void WriteFileFromByte(string filePath, byte[] data)
        {
            if (File.Exists(filePath)) File.Delete(filePath);

            var stream = File.Create(filePath);
            stream.Write(data, 0, data.Length);
            stream.Dispose();
            stream.Close();
        }

        /// <summary>
        /// AppDataPath + 相对路径，默认 Windows 系统="/"
        /// </summary>
        /// <param name="relativePath">推荐 "Assets/xxx/xxx"</param>
        /// <returns></returns>
        public static string GetAbsolutePath(string relativePath)
        {
            return relativePath.StartsWith("Assets")
                ? CombinePath(Application.dataPath, relativePath[6..])
                : CombinePath(Application.dataPath, relativePath);
        }

        /// <summary>
        /// Combines two paths 替换所有反斜杠为正斜杠，默认Windows系统="/"
        /// </summary>
        public static string CombinePath(string a, string b)
        {
            a = a.Replace("\\", "/").TrimEnd('/');
            b = b.Replace("\\", "/").TrimStart('/');
            return a + "/" + b;
        }

        #endregion

        #region 资源相关，SO 资源

        /// <summary>
        /// 查找资源路径，根据资源名称和扩展名
        /// </summary>
        /// <param name="nameNoExtension"></param>
        /// <returns></returns>
        public static string FindAssetPath(string nameNoExtension)
        {
#if UNITY_EDITOR
            string guid = AssetDatabase.FindAssets(nameNoExtension).FirstOrDefault();
            return string.IsNullOrEmpty(guid) ? string.Empty : AssetDatabase.GUIDToAssetPath(guid);
#else
return string.Empty;
#endif
        }

        /// <summary>
        /// 根据类型获取单个资源文件，更加通用，默认查找.asset
        /// </summary>
        /// <typeparam name="T"> 资源类型 </typeparam>
        /// <returns> Asset 资源文件 </returns>
        public static T FindAndLoadAsset<T>() where T : Object
        {
#if UNITY_EDITOR
            var assetPath = FindAssetPath<T>();
            return !string.IsNullOrEmpty(assetPath) ? AssetDatabase.LoadAssetAtPath<T>(assetPath) : null;
#else
LogModule.ZeusError("查找资源并加载，仅在编辑器下有效，请修改冗余代码");
return default;
#endif
        }

        /// <summary>
        /// 根据类型获取单个资源的路径，更加通用，非脚本类型
        /// </summary>
        /// <typeparam name="T"> 资源类型 </typeparam>
        /// <returns> 字符串路径 </returns>
        public static string FindAssetPath<T>() where T : Object
        {
#if UNITY_EDITOR
            var assetPath = AssetDatabase.FindAssets("t:" + typeof(T))
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault();
            return !string.IsNullOrEmpty(assetPath) ? assetPath : default;
#else
LogModule.ZeusError("查找资源路径，仅在编辑器下有效，请修改冗余代码");
return default;
#endif
        }

        /// <summary>
        /// 获取或创建一个单例 SO 资源，如果资源不存在则创建，如果有多个 SO 资源，则只返回第一个，并删除其他资源
        /// </summary>
        /// <remarks> 使用 AssetDatabase.LoadAssetAtPath 加载，此加载方式仅适用于编辑器状态 </remarks>
        /// <param name="path"> 新创建的 SO 资源路径，如果不存在资源 </param>
        /// <typeparam name="T"> SO 文件类型 </typeparam>
        /// <returns> 加载一个具体类的 SO 资源 </returns>
        public static T GetSingleSoAndDeleteExtra<T>(string path) where T : ScriptableObject
        {
#if UNITY_EDITOR
            T wantToAsset;
            var guids = AssetDatabase.FindAssets("t:" + typeof(T));
            var allPaths = new string[guids.Length];
            if (guids.Length > 0)
            {
                allPaths[0] = AssetDatabase.GUIDToAssetPath(guids[0]);

                // 只获取一个资源 0 号
                wantToAsset = AssetDatabase.LoadAssetAtPath<T>(allPaths[0]);
                if (wantToAsset == null) Debug.LogWarning("GetSingleSOAndDeleteExtra 中加载资源失败");

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
            // 清除空格
            string newPath = path.Trim();
            if (!path.EndsWith(".asset")) newPath = Path.Combine(newPath, ".asset");
            AssetDatabase.CreateAsset(wantToAsset, newPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return wantToAsset;
#else
#endif
        }

        #endregion
    }
}