using Odin_Toolkits.Editor_API_Usage_Guide.General.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._10.UnityAssetDatabase.Editor
{
    public class UnityAssetDatabaseAPI : AbstractEditorTutorialWindow<UnityAssetDatabaseAPI>
    {
        protected override string SetUsageTip()
        {
            return "案例介绍: Unity 内置 AssetDatabase API 使用示例";
        }

        [PropertyOrder(0)]
        [Title("AssetDatabase API 按钮测试")]
        [OnInspectorGUI]
        void Title() { }
        
        protected override void DrawEditors()
        {
            // base.DrawEditors();
            // ---
            if (GUILayout.Button("创建资源"))
            {
                var material = new Material(Shader.Find("Standard"));
                AssetDatabase.CreateAsset(material,
                    "Assets/OdinExtension/EditorAPITutorial/10.UnityAssetDatabase/Test/ExampleMaterial.mat");
            }

            // ---
            if (GUILayout.Button("加载资源"))
            {
                var material = AssetDatabase.LoadAssetAtPath<Material>(
                    "Assets/OdinExtension/EditorAPITutorial/10.UnityAssetDatabase/Test/ExampleMaterial.mat");
                Debug.Log(material);
            }

            // ---
            if (GUILayout.Button("移动资源"))
            {
                AssetDatabase.MoveAsset(
                    "Assets/OdinExtension/EditorAPITutorial/10.UnityAssetDatabase/Test/ExampleMaterial.mat",
                    "Assets/OdinExtension/EditorAPITutorial/10.UnityAssetDatabase/TestSecond/ExampleMaterial2.mat");
            }

            // ---
            if (GUILayout.Button("复制资源"))
            {
                AssetDatabase.CopyAsset(
                    "Assets/OdinExtension/EditorAPITutorial/10.UnityAssetDatabase/TestSecond/ExampleMaterial2.mat",
                    "Assets/OdinExtension/EditorAPITutorial/10.UnityAssetDatabase/Test/ExampleMaterialCopy.mat");
            }

            // ---
            if (GUILayout.Button("删除资源"))
            {
                AssetDatabase.DeleteAsset(
                    "Assets/OdinExtension/EditorAPITutorial/10.UnityAssetDatabase/Test/ExampleMaterialCopy.mat");
            }

            // ---
            if (GUILayout.Button("获取当前选中的资源路径"))
            {
                var obj = Selection.activeObject;
                string path = AssetDatabase.GetAssetPath(obj);
                Debug.Log(path);
            }

            // ---
            if (GUILayout.Button("获取资源的依赖关系"))
            {
                var obj = Selection.activeObject;
                string[] dependencies = AssetDatabase.GetDependencies(AssetDatabase.GetAssetPath(obj));
                foreach (string dependency in dependencies)
                {
                    Debug.Log(dependency);
                }
            }

            // ---
            if (GUILayout.Button("加载所有资源 AssetDatabase.LoadAllAssetsAtPath 会包含自身，图集比较常用"))
            {
                var assets =
                    AssetDatabase.LoadAllAssetsAtPath(
                        "Assets/OdinExtension/EditorAPITutorial/10.UnityAssetDatabase/TestSecond");
                foreach (var asset in assets)
                {
                    Debug.Log(asset);
                }
            }

            // ---
            if (GUILayout.Button("返回资源所属于的AB包 AssetDatabase.GetImplicitAssetBundleName "))
            {
                var obj = Selection.activeObject;
                string path = AssetDatabase.GetAssetPath(obj);
                string assetBundleName = AssetDatabase.GetImplicitAssetBundleName(path);
                Debug.Log(assetBundleName);
            }
        }
    }
}