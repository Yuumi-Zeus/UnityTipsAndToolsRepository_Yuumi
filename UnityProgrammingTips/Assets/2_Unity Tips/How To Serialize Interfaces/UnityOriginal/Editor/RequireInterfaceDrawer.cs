using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HowToSerializeInterfaces.UnityOriginal.Editor
{
    // [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        RequireInterfaceAttribute RequireInterfaceAttribute => (RequireInterfaceAttribute)attribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Type requiredType = RequireInterfaceAttribute.InterfaceType;
            EditorGUI.BeginProperty(position, label, property);
            {
                if (property.isArray && property.propertyType == SerializedPropertyType.Generic)
                {
                    DrawArrayField(position, property, label, requiredType);
                }
                else
                {
                    DrawInterfaceObjectField(position, property, label, requiredType);
                }
            }
            EditorGUI.EndProperty();
            var args = new InterfaceArgs(requiredType, GetTypeOrElementType(fieldInfo.FieldType));
            InterfaceReferenceUtil.OnGUI(position, property, label, args);
        }

        void DrawArrayField(Rect position, SerializedProperty property, GUIContent label, Type interfaceType)
        {
            property.arraySize =
                EditorGUI.IntField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                    label.text + " Size", property.arraySize);
            float yOffset = EditorGUIUtility.singleLineHeight;
            for (int i = 0; i < property.arraySize; i++)
            {
                var element = property.GetArrayElementAtIndex(i);
                var elementRect = new Rect(position.x, position.y + yOffset, position.width,
                    EditorGUIUtility.singleLineHeight);
                DrawInterfaceObjectField(elementRect, element, new GUIContent($"Element {i}"), interfaceType);
                yOffset += EditorGUIUtility.singleLineHeight;
            }
        }

        void DrawInterfaceObjectField(Rect position, SerializedProperty property, GUIContent label, Type interfaceType)
        {
            var oldReference = property.objectReferenceValue;
            var newReference = EditorGUI.ObjectField(position, label, oldReference, typeof(Object), true);
            if (newReference != null && newReference != oldReference)
            {
                ValidateAndAssignedObject(property, newReference, interfaceType);
            }
            else if (newReference == null)
            {
                property.objectReferenceValue = null;
            }
        }

        void ValidateAndAssignedObject(SerializedProperty property, UnityEngine.Object newReference,
            Type interfaceType)
        {
            if (newReference is GameObject gameObject)
            {
                var component = gameObject.GetComponent(interfaceType);
                if (component == null) return;
                property.objectReferenceValue = component;
            }
            else if (interfaceType.IsAssignableFrom(newReference.GetType()))
            {
                property.objectReferenceValue = newReference;
            }
        }

        /// <summary>
        /// 获取类型或类型参数
        /// </summary>
        /// <param name="type">输入的类型</param>
        /// <returns>如果输入类型是数组，则返回数组元素类型；如果输入类型是泛型类型，则返回泛型参数类型；否则返回输入类型本身</returns>
        Type GetTypeOrElementType(Type type)
        {
            // 检查类型是否为数组
            if (type.IsArray)
            {
                // 返回数组的元素类型
                return type.GetElementType();
            }

            // 检查类型是否为泛型类型
            if (type.IsGenericType)
            {
                // 返回泛型类型的第一个参数类型
                return type.GetGenericArguments()[0];
            }

            // 如果类型既不是数组也不是泛型类型，直接返回该类型
            return type;
        }
    }
}