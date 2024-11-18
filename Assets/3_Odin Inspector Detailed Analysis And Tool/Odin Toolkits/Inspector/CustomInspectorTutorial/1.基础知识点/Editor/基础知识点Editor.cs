using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Inspector.CustomInspectorTutorial._1.基础知识点.Editor
{
    [CustomEditor(typeof(基础知识点))]
    public class 基础知识点Editor : Sirenix.OdinInspector.Editor.OdinEditor
    {
        SerializedProperty _atk;
        SerializedProperty _def;
        SerializedProperty _obj;

        bool _foldout;

        protected override void OnEnable()
        {
            base.OnEnable();
            _atk = serializedObject.FindProperty("atk");
            _def = serializedObject.FindProperty("def");
            _obj = serializedObject.FindProperty("obj");
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            serializedObject.Update();
            _foldout = EditorGUILayout.BeginFoldoutHeaderGroup(_foldout,
                new GUIContent("基础属性", OdinEditorResources.OdinLogo));
            if (_foldout)
            {
                if (GUILayout.Button("绘制按钮"))
                {
                    Debug.Log(target.GetType());
                }

                EditorGUILayout.IntSlider(_atk, 0, 100, new GUIContent("攻击力"));
                EditorGUILayout.PropertyField(_atk);
                EditorGUILayout.PropertyField(_def);
                EditorGUILayout.ObjectField(_obj, typeof(GameObject), new GUIContent("物体"));
                EditorGUILayout.EndFoldoutHeaderGroup();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}