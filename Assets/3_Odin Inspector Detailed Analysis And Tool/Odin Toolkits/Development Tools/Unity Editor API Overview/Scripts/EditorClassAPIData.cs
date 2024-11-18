using System;
using System.Collections.Generic;
using System.Reflection;
using Odin_Toolkits.Custom_Utilities.Attributes;
using Odin_Toolkits.Custom_Utilities.Attributes.Composite;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Odin_Toolkits.Development_Tools._1_Unity_Editor_API_Overview.Scripts
{
    [CreateAssetMenu(menuName = "Odin Toolkits/EditorClassAPIData", fileName = "ClassAPIDate", order = 10)]
    public class EditorClassAPIData : ScriptableObject
    {
        [HideInInspector] public string belongToCollection;
        [HideInInspector] public string assemblyQualifiedName;

        [PropertyOrder(-1), HorizontalGroup("Header", 0.88f), VerticalGroup("Header/Left"),
         ForStringDisplayAsStringAlignLeftRichText(36)]
        public string className;

        [HorizontalGroup("Header", 0.12f), VerticalGroup("Header/Right"), BoxGroup("Header/Right/右", ShowLabel = false),
         HideLabel, ForStringDisplayAsStringAlignLeftRichText(13)]
        public string unityClassAPIVersion;

        [ListDrawerSettings, TableList(ShowIndexLabels = true), LabelText("方法数据列表"), Searchable]
        public List<MethodData> backendMethodMemberDataList;

        [ListDrawerSettings, TableList(ShowIndexLabels = true), LabelText("属性数据列表"), Searchable]
        public List<MemberData> backendPropertyDataList;

        [ListDrawerSettings, TableList(ShowIndexLabels = true), LabelText("字段数据列表"), Searchable]
        public List<MemberData> backendFieldDataList;

        [ListDrawerSettings, TableList(ShowIndexLabels = true), LabelText("事件数据列表"), Searchable]
        public List<MethodData> backendEventDataList;

        [HorizontalGroup("Header", 0.12f), VerticalGroup("Header/Right"), Button("重新读取信息", ButtonSizes.Large)]
        public void ResetAnalyse()
        {
            backendFieldDataList = new List<MemberData>();
            backendPropertyDataList = new List<MemberData>();
            backendMethodMemberDataList = new List<MethodData>();
            backendEventDataList = new List<MethodData>();
            ReadClassInfo();
        }

        public void Init()
        {
            backendFieldDataList = new List<MemberData>();
            backendPropertyDataList = new List<MemberData>();
            backendMethodMemberDataList = new List<MethodData>();
            backendEventDataList = new List<MethodData>();
        }

        public void ReadClassInfo()
        {
            var type = Type.GetType(assemblyQualifiedName);
            if (type == null)
            {
                Debug.LogWarning("type 获取失败，assemblyQualifiedName = " + assemblyQualifiedName);
                return;
            }

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var events = type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (fields.Length > 0)
                foreach (var field in fields)
                    backendFieldDataList.Add(new MemberData()
                    {
                        memberName = field.GetNiceName(),
                        isStatic = field.IsStatic()
                    });

            if (properties.Length > 0)
                foreach (var property in properties)
                    backendPropertyDataList.Add(new MemberData()
                    {
                        memberName = property.GetNiceName(),
                        isStatic = property.IsStatic()
                    });

            if (methods.Length > 0)
                foreach (var method in methods)
                {
                    // 舍弃属性的 getter 方法
                    if (method.GetNiceName().StartsWith("get")) continue;

                    var methodMemberData = new MethodData()
                    {
                        memberName = method.GetNiceName(),
                        isStatic = method.IsStatic(),
                        returnType = method.ReturnType.GetNiceName()
                    };

                    backendMethodMemberDataList.Add(methodMemberData);
                }

            if (events.Length <= 0) return;
            foreach (var eventInfo in events)
            {
                backendEventDataList.Add(new MethodData()
                {
                    memberName = eventInfo.GetNiceName()
                });
            }
        }
    }


    [Serializable]
    public class MemberData
    {
        [PropertyOrder(0), TableColumnWidth(60, Resizable = false), DisplayAsString]
        public bool isStatic;

        [PropertyOrder(10)] public string memberName;
    }

    [Serializable]
    public class MethodData : MemberData
    {
        [PropertyOrder(5), TableColumnWidth(80, Resizable = false), DisplayAsString]
        public string returnType;
    }
}