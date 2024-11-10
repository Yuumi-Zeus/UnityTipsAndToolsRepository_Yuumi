using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HowToSerializeInterfaces.UnityOriginal.Editor
{
    [CustomPropertyDrawer(typeof(InterfaceReference<>))]
    [CustomPropertyDrawer(typeof(InterfaceReference<,>))]
    public class InterfaceReferenceDrawer : PropertyDrawer
    {
        /// <summary>
        /// 定义一个字段名称
        /// </summary>
        const string SourceFieldName = "sourceValue";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sourceProperty = property.FindPropertyRelative(SourceFieldName);
            // 这里的 fieldInfo 是从 PropertyDrawer 基类继承的字段信息
            // 这个抽屉的作用域的字段
            // 即 InterfaceReference<T> 或者 InterfaceReference<T, TSource> 这个字段的 FieldInfo
            var args = GetArguments(fieldInfo);
            EditorGUI.BeginProperty(position, label, property);
            {
                var assignedObject = EditorGUI.ObjectField(position, label, sourceProperty.objectReferenceValue,
                    typeof(Object), true);

                if (assignedObject != null)
                {
                    if (assignedObject is GameObject gameObject)
                    {
                        ValidateAndAssignObject(sourceProperty, gameObject.GetComponent(args.InterfaceType),
                            gameObject.name, args.InterfaceType.Name);
                    }
                    else
                    {
                        ValidateAndAssignObject(sourceProperty, assignedObject, assignedObject.name);
                    }
                }
                else
                {
                    sourceProperty.objectReferenceValue = null;
                }
            }
            EditorGUI.EndProperty();
            InterfaceReferenceUtil.OnGUI(position, sourceProperty, label, args);
        }

        /// <summary>
        /// 从字段信息中获取接口参数。
        /// </summary>
        static InterfaceArgs GetArguments(FieldInfo fieldInfo)
        {
            Type fieldType = fieldInfo.FieldType;

            if (!TryGetTypesFromInterfaceReference(fieldType, out var interfaceTypeValue, out var objectTypeValue))
            {
                GetTypesFromList(fieldType, out interfaceTypeValue, out objectTypeValue);
            }

            return new InterfaceArgs(interfaceTypeValue, objectTypeValue);

            bool TryGetTypesFromInterfaceReference(Type type, out Type interfaceType, out Type objectType)
            {
                objectType = interfaceType = null;
                if (type?.IsGenericType != true)
                {
                    return false;
                }

                // 目的是获取最基础的类型，InterfaceReference<TInterface,TObject>
                Type genericType = type.GetGenericTypeDefinition();
                // 如果是 InterfaceReference<>，则获取其基类，也就是 InterfaceReference<,>
                if (genericType == typeof(InterfaceReference<>))
                {
                    type = type.BaseType;
                }

                if (type?.GetGenericTypeDefinition() == typeof(InterfaceReference<,>))
                {
                    var types = type.GetGenericArguments();
                    interfaceType = types[0];
                    objectType = types[1];
                    return true;
                }

                return false;
            }

            void GetTypesFromList(Type type, out Type interfaceType, out Type objectType)
            {
                interfaceType = objectType = null;
                // 数组和列表都实现了 IList 接口
                var listInterface = type.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IList<>));

                if (listInterface != null)
                {
                    var elementType = listInterface.GetGenericArguments()[0];
                    TryGetTypesFromInterfaceReference(elementType, out interfaceType, out objectType);
                }
            }
        }

        /// <summary>
        /// 验证对象是否符合指定的组件名称或类型，并将其分配给提供的属性。
        /// 如果对象不符合要求且未分配，则输出警告信息。
        /// </summary>
        /// <param name="property">要分配对象的属性。</param>
        /// <param name="targetObject">目标对象，如果为null，则不会分配。</param>
        /// <param name="componentNameOrType">组件名称或类型，用于验证。</param>
        /// <param name="interfaceName">可选参数，指定接口名称，用于更具体的错误信息。</param>
        static void ValidateAndAssignObject(SerializedProperty property, Object targetObject,
            string componentNameOrType, string interfaceName = null)
        {
            // 如果目标对象不为空，则直接分配给属性
            if (targetObject != null)
            {
                property.objectReferenceValue = targetObject;
            }
            else
            {
                // 根据是否提供了接口名称，构建不同的警告信息
                var message = interfaceName != null
                    ? $"GameObject '{componentNameOrType}'"
                    : "assigned object";
                // 输出警告信息，并将属性值设置为 null
                Debug.LogWarning($"The {message} does not have a component that implements  '{interfaceName}'.");
                property.objectReferenceValue = null;
            }
        }
    }

    public struct InterfaceArgs
    {
        public Type InterfaceType;
        public Type ObjectType;

        public InterfaceArgs(Type interfaceType, Type objectType)
        {
            // IsAssignableFrom 是指实参的类型是否为实例的类型的派生类型，实参是否能够赋值给实例类型
            // Assignable 可分配的，即可分配给实例类型，也就是实参是否为实例类型或者及其派生类型
            Debug.Assert(typeof(Object).IsAssignableFrom(objectType),
                $"{nameof(objectType)} needs to be a subclass of UnityEngine.Object");
            Debug.Assert(interfaceType.IsInterface, $"{nameof(interfaceType)} needs to be an interface");
            // 构造函数赋值
            InterfaceType = interfaceType;
            ObjectType = objectType;
        }
    }
}