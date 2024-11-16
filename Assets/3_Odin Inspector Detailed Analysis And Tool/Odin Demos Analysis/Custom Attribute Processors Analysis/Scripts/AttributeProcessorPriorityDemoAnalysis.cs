using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

#if UNITY_EDITOR
namespace _3_Odin_Inspector_Detailed_Analysis_And_Tool.Odin_Demos_Analysis.Custom_Attribute_Processors_Analysis.Scripts
{
    [TypeInfoBox("This example demonstrates how AttributeProcessors are ordered by priority.")]
    // 这个例子演示了AttributeProcessors是如何按优先级排序的。
    public class AttributeProcessorPriorityDemoAnalysis : MonoBehaviour
    {
        public PrioritizedProcessed processed;
    }

    [Serializable]
    public class PrioritizedProcessed
    {
        public int a;
    }

    // This processor has the highest priority and is therefore executed first.
    // It adds a Range attribute the child members of the PrioritizedResolved class.
    // The range attribute will be removed by the SecondAttributeProcessor.
    // 该处理器具有最高优先级，因此首先执行。
    // 它为 PrioritizedResolved 的子成员添加一个 Range 属性。
    // range 属性将被 SecondAttributeProcessor 删除。
    [ResolverPriority(100)]
    public class FirstAttributeProcessor : OdinAttributeProcessor<PrioritizedProcessed>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            attributes.Add(new BoxGroupAttribute("First"));
            attributes.Add(new RangeAttribute(0, 10));
        }
    }

    // This processor has a default priority of 0, and is therefore executed second.
    // It clears the attributes list and therefore removes all attributes from the members of the PrioritizedResolved class.
    // 该处理器具有默认优先级为 0，因此第二个执行
    // 它将清空属性列表，因此从 PrioritizedResolved 类的成员中删除所有属性。
    public class SecondAttributeProcessor : OdinAttributeProcessor<PrioritizedProcessed>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            attributes.RemoveAttributeOfType<RangeAttribute>();
            var boxGroup = attributes.OfType<BoxGroupAttribute>().FirstOrDefault();
            if (boxGroup != null) boxGroup.GroupName += " - Second";
        }
    }

    // This processor has the lowest priority and is therefore executed last.
    // It adds a BoxGroup to the child members of the PrioritizedResolved class.
    // Since this is executed after the SecondAttributeProcessor, the BoxGroup attribute is not removed.
    // 该处理器具有最低优先级，因此最后执行。
    // 它向 PrioritizedResolved 的子成员添加一个 BoxGroup 属性。
    // 由于它在 SecondAttributeProcessor 之后执行，因此 BoxGroup 属性不会被删除。
    [ResolverPriority(-100)]
    public class ThirdAttributeProcessor : OdinAttributeProcessor<PrioritizedProcessed>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            var boxGroup = attributes.OfType<BoxGroupAttribute>().FirstOrDefault();
            if (boxGroup != null) boxGroup.GroupName += " - Third";
        }
    }
}
#endif