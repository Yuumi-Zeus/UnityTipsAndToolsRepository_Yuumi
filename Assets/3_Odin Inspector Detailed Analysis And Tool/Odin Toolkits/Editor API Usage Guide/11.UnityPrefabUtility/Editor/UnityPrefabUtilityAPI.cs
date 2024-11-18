using Odin_Toolkits.Editor_API_Usage_Guide.General.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._11.UnityPrefabUtility.Editor
{
    public class UnityPrefabUtilityAPI : AbstractEditorTutorialWindow<UnityPrefabUtilityAPI>
    {
        protected override string SetUsageTip()
        {
            return "案例介绍: Unity 内置 PrefabUtility API 使用示例";
        }

        [PropertyOrder(0)]
        [Title("PrefabUtility API 按钮测试")]
        [Button("动态创建并保存预制体", ButtonSizes.Medium)]
        void Execute()
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;
            gameObject.name = "ExampleCube";
            // 创建预制体
            PrefabUtility.SaveAsPrefabAsset(gameObject,
                "Assets/OdinExtension/EditorAPITutorial/11.UnityPrefabUtility/TestPrefabs/ExampleCube.prefab");
            // 销毁游戏对象
            DestroyImmediate(gameObject);
        }

        [PropertyOrder(0)]
        [InfoBox("这一步的加载实际是加载到了隔离场景中，然后需要使用 SaveAsPrefabAsset 作为一个新的预制体进行覆盖保存，需要成对出现，否则会出现内存泄漏问题")]
        [InfoBox("这种方法可以移除脚本，实际已经加载到了一个场景中，可以进行操作")]
        [Button("加载预制体对象到隔离场景,然后释放，需要成对出现", ButtonSizes.Medium)]
        void Execute2()
        {
            var prefab = PrefabUtility.LoadPrefabContents(
                "Assets/OdinExtension/EditorAPITutorial/11.UnityPrefabUtility/TestPrefabs/ExampleCube.prefab");
            if (!prefab.GetComponent<BoxCollider>())
            {
                prefab.AddComponent<BoxCollider>();
            }

            PrefabUtility.SaveAsPrefabAsset(prefab,
                "Assets/OdinExtension/EditorAPITutorial/11.UnityPrefabUtility/TestPrefabs/ExampleCube.prefab");
            PrefabUtility.UnloadPrefabContents(prefab);
        }

        [PropertyOrder(0)]
        [InfoBox("这一步的加载是使用 AssetDatabase ，然后可以使用 SavePrefabAsset 直接修改保存")]
        [InfoBox("这种方法不可以移除脚本，因为它属于内存中的资产")]
        [Button("修改已有预制体", ButtonSizes.Medium)]
        void Execute3()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/OdinExtension/EditorAPITutorial/11.UnityPrefabUtility/TestPrefabs/ExampleCube.prefab");
            if (!prefab.GetComponent<BoxCollider>())
            {
                prefab.AddComponent<BoxCollider>();
            }

            // if (prefab.GetComponent<MeshRenderer>())
            // {
            //     DestroyImmediate(prefab.GetComponent<MeshRenderer>());
            // }

            PrefabUtility.SavePrefabAsset(prefab, out bool success);
            Debug.Log(success);
        }

        [PropertyOrder(0)]
        [Button("实例化预制体", ButtonSizes.Medium)]
        void Execute4()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/OdinExtension/EditorAPITutorial/11.UnityPrefabUtility/TestPrefabs/ExampleCube.prefab");
            var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            if (instance != null) instance.transform.position = Vector3.one;
        }
    }
}