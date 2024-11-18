using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Inspector.CustomInspectorTutorial._2.数组和列表.Editor
{
    [CustomEditor(typeof(数组和列表))]
    public class 数组和列表Editor : Sirenix.OdinInspector.Editor.OdinEditor
    {
        SerializedProperty _ints;
        SerializedProperty _strings;
        SerializedProperty _gameObjects;

        int _intsArraySize;

        protected override void OnEnable()
        {
            base.OnEnable();
            // 此处初始化序列化属性
            _ints = serializedObject.FindProperty("ints");
            _strings = serializedObject.FindProperty("strings");
            _gameObjects = serializedObject.FindProperty("gameObjects");
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            serializedObject.Update(); // 在绘制之前，更新序列化对象，以确保最新的数据
            // 此处绘制自定义 Inspector
            GUILayout.Label("Unity 原生数组和列表如下: ", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_ints, new GUIContent("整形数组", OdinEditorResources.OdinLogo), true);
            EditorGUILayout.PropertyField(_strings, new GUIContent("字符串数组", OdinEditorResources.OdinInspectorLogo),
                true);
            EditorGUILayout.PropertyField(_gameObjects,
                new GUIContent("物体对象列表", OdinEditorResources.OdinSerializerLogo), true);
            // 自定义
            GUILayout.Label("自定义数组样式如下: ", EditorStyles.boldLabel);
            _intsArraySize = _ints.arraySize;
            _intsArraySize =
                EditorGUILayout.IntSlider(new GUIContent("整形数组长度: ", OdinEditorResources.OdinValidatorLogo),
                    _intsArraySize, 0, 10);
            if (_intsArraySize != _ints.arraySize)
            {
                try
                {
                    // 添加边界检查和异常处理
                    if (_intsArraySize >= 0 && _intsArraySize <= 10)
                    {
                        _ints.arraySize = _intsArraySize;
                    }
                    else
                    {
                        Debug.LogWarning("整形数组长度超出范围，将不进行修改。");
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("修改整形数组长度时发生异常: " + ex.Message);
                }
            }

            for (var i = 0; i < _ints.arraySize; i++)
            {
                EditorGUILayout.PropertyField(_ints.GetArrayElementAtIndex(i), new GUIContent("整形数组元素: " + i));
            }

            serializedObject.ApplyModifiedProperties(); // 在绘制结束后，应用序列化对象，以确保修改生效
        }
    }
}