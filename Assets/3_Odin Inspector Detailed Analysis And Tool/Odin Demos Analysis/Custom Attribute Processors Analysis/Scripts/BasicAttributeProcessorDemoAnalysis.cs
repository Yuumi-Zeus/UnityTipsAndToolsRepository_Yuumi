using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

#if UNITY_EDITOR
namespace _3_Odin_Inspector_Detailed_Analysis_And_Tool.Odin_Demos_Analysis.Custom_Attribute_Processors_Analysis.Scripts
{
    public class BasicAttributeProcessorDemoAnalysis : MonoBehaviour
    {
        // 等价于
        // [InfoBox("Dynamically added attributes.")]
        // [InlineProperty]
        public MyCustomClass processed = new();
    }

    [Serializable]
    public class MyCustomClass
    {
        // 等价于
        // [HideLabel]
        // [BoxGroup("Box")]
        // [EnumToggleButtons]
        public ScaleMode mode;

        // [HideLabel]
        // [BoxGroup("Box")]
        // [Range(0, 5)]
        public float size;
    }

    // This AttributeProcessor will be found and used to processor attributes for the MyCustomClass class.
    // 这个 AttributeProcessor 将被找到并用于 MyCustomClass 类的处理器属性。
    // ---
    // 用来自定义对某些类上增加 Attribute 的逻辑。
    // 这样对于某一类，就不用在每一个字段上添加 Attribute，它会直接对这个类的字段上进行属性处理。
    public class MyResolvedClassAttributeProcessor : OdinAttributeProcessor<MyCustomClass>
    {
        // This method will be called for any field or property of the type MyCustomClass.
        // In this example, this will be run for the BasicAttributeProcessorExample.Processed field.
        // 对于 MyCustomClass 类型的任何字段或属性都将调用此方法。
        // 在本例中，这将为 BasicAttributeProcessorDemoAnalysis 运行。
        // processed 字段
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new InfoBoxAttribute("Dynamically added attributes."));
            attributes.Add(new InlinePropertyAttribute());
            // 以上操作等价于
            // [InfoBox("Dynamically added attributes.")]
            // [InlineProperty]
            // public MyCustomClass processed;
        }

        // This method will be called for any members of the type MyCustomClass.
        // In this example, this will be run for the fields MyCustomClass.Mode and MyCustomClass.Size.
        // 这个方法是对类型 MyCustomClass 的所有成员进行处理。
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new BoxGroupAttribute("Box", false));

            switch (member.Name)
            {
                case "mode":
                    attributes.Add(new EnumToggleButtonsAttribute());
                    break;
                case "size":
                    attributes.Add(new RangeAttribute(0, 5));
                    break;
            }
            // 以上操作等价于
            // [HideLabel]
            // [BoxGroup("Box")]
            // [EnumToggleButtons]
            // public ScaleMode mode;
            // [HideLabel]
            // [BoxGroup("Box")]
            // [Range(0, 5)]
            // public float size;
        }
    }
}
#endif