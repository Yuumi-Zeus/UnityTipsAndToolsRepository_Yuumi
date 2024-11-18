using System;
using System.Reflection;
using Odin_Toolkits.Custom_Utilities.Attributes;
using Odin_Toolkits.Custom_Utilities.Attributes.Composite;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Odin_Toolkits.Development_Tools._1_Unity_Editor_API_Overview.ProceduralTest
{
    [CreateAssetMenu(menuName = "Odin Toolkits/Test/TestUnityEditorAPIOverviewAsset",
        fileName = "TestUnityEditorAPIOverview",
        order = 10)]
    public class TestUnityEditorAPIOverview : ScriptableObject
    {
        [Title("测试信息值")]
        [ShowInInspector]
        [InfoBox("Type 类型和 Assembly 类型无法直接序列化，因此值不会存储到文件中，重新编译值为空，[ShowInInspector] 不序列化值，仅仅是显示到面板上")]
        public Type Type;

        [ShowInInspector] public Assembly Assembly;
        public string fullName;
        public string namespaceName;
        public string typeName;
        public string assemblyQualifiedName;
        public string assemblyName;

        [Title("方法")]
        [Button("测试获取 Type 相关值")]
        public void TestType()
        {
            Type = typeof(DragAndDropUtilities);
            fullName = Type.FullName;
            namespaceName = Type.Namespace;
            typeName = Type.Name;
            Assembly = Type.Assembly;
            assemblyQualifiedName = Type.AssemblyQualifiedName;
            assemblyName = Type.Assembly.GetName().Name;
        }

        [Button("测试 Type.GetType() 方法")]
        public void GetUniqueType()
        {
            Type uniqueType;
            uniqueType = Type.GetType(typeName);
            if (uniqueType != null)
            {
                Debug.Log($"Type.GetType(typeName) 有效，返回的 Type 类型 值为 {uniqueType}");
            }
            else
            {
                Debug.Log($"Type.GetType(typeName) 无效");
            }

            uniqueType = Type.GetType(fullName);
            if (uniqueType != null)
            {
                Debug.Log($"Type.GetType(fullName) 有效，返回的 Type 类型 值为 {uniqueType}");
            }
            else
            {
                Debug.Log($"Type.GetType(fullName) 无效");
            }

            uniqueType = Type.GetType(assemblyQualifiedName);
            if (uniqueType != null)
            {
                Debug.Log($"Type.GetType(assemblyQualifiedName) 有效，返回的 Type 类型 值为 {uniqueType}");
            }
            else
            {
                Debug.Log($"Type.GetType(assemblyQualifiedName) 无效");
            }

            uniqueType = Type.GetType(assemblyName);
            if (uniqueType != null)
            {
                Debug.Log($"Type.GetType(assemblyName) 有效，返回的 Type 类型 值为 {uniqueType}");
            }
            else
            {
                Debug.Log($"Type.GetType(assemblyName) 无效");
            }

            uniqueType = Type.GetType(namespaceName + "." + typeName);
            if (uniqueType != null)
            {
                Debug.Log($"Type.GetType(namespaceName + \"." + typeName + "\") 有效，返回的 Type 类型 值为 {uniqueType}");
            }
            else
            {
                Debug.Log($"Type.GetType(namespaceName + \"." + typeName + "\") 无效");
            }

            uniqueType = Type.GetType(typeName + ", " + assemblyName);
            if (uniqueType != null)
            {
                Debug.Log($"Type.GetType(typeName + \", \" + assemblyName) 有效，返回的 Type 类型 值为 {uniqueType}");
            }
            else
            {
                Debug.Log($"Type.GetType(typeName + \", \" + assemblyName) 无效");
            }

            uniqueType = Type.GetType(fullName + ", " + assemblyName);
            if (uniqueType != null)
            {
                Debug.Log($"Type.GetType(fullName + \", \" + assemblyName) 有效，返回的 Type 类型 值为 {uniqueType}");
            }
            else
            {
                Debug.Log($"Type.GetType(fullName + \", \" + assemblyName) 无效");
            }
        }

        [PropertyOrder(5), Title("结论"), ForStringDisplayAsStringAlignLeftRichText(13), ShowDrawerChain]
        public string conclusion =
            "1.Type 类型和 Assembly 类型无法直接序列化，因此值不会存储到文件中，重新编译值为空，[ShowInInspector] 不序列化值，仅仅是显示到面板上 \n" +
            "2.选择使用 Type 的 AssemblyQualifiedName，可以顺利搜索到类型，不用人为考虑程序集，命名空间，使用这个最完整信息即可";
    }
}