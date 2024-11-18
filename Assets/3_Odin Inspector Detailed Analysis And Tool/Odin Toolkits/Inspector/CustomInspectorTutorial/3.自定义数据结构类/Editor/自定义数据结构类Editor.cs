using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Inspector.CustomInspectorTutorial._3.自定义数据结构类.Editor
{
    [CustomEditor(typeof(自定义数据结构类))]
    public class 自定义数据结构类Editor : Sirenix.OdinInspector.Editor.OdinEditor
    {
        SerializedProperty _customData;
        SerializedProperty _customDataIntValue;
        SerializedProperty _customDataStringValue;

        protected override void OnEnable()
        {
            base.OnEnable();
            // 此处初始化序列化属性 
            _customData = serializedObject.FindProperty("customData");
            // 通过 SerializedProperty 的 FindPropertyRelative 寻找子属性
            _customDataIntValue = _customData.FindPropertyRelative("intValue");
            // 通过 SerializedObject 的 FindProperty 寻找子属性
            _customDataStringValue = serializedObject.FindProperty("customData.stringValue");
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            serializedObject.Update(); // 在绘制之前，更新序列化对象，以确保最新的数据
            // 此处绘制自定义 Inspector
            GUILayout.Label("Unity 原生自定义数据结构如下: ", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_customData,
                new GUIContent("自定义数据结构", Sirenix.Utilities.Editor.OdinEditorResources.OdinLogo), true);
            // 自定义
            GUILayout.Label("自定义数据结构样式如下: ", EditorStyles.boldLabel);
            _customDataIntValue.intValue = EditorGUILayout.IntField("整形值", _customDataIntValue.intValue);
            _customDataStringValue.stringValue = EditorGUILayout.TextField("字符串值", _customDataStringValue.stringValue);
            serializedObject.ApplyModifiedProperties(); // 在绘制结束后，应用序列化对象，以确保修改生效
        }
    }
}