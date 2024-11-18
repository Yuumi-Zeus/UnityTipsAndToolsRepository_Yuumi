using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Object = UnityEngine.Object;

namespace Odin_Demos_Analysis.Custom_Attribute_Processors_Analysis.Scripts
{
#if UNITY_EDITOR
    [TypeInfoBox(
        "This example demonstrates how you could use AttributeProcessors to arrange properties " +
        "into different groups, based on where they were declared.")]
    // 这个例子演示了如何使用 AttributeProcessors 来排列属性，根据它们在哪里发布而分为不同的组
    public class TabGroupByDeclaringTypeDemoAnalysis : Bar // Bar inherits from Foo
    {
        // 等价于
        // [TabGroup("TabGroupByDeclaringTypeDemoAnalysis")]
        // [PropertyOrder(-3)]
        public string a;

        // [TabGroup("TabGroupByDeclaringTypeDemoAnalysis")]
        public string b;

        // [TabGroup("TabGroupByDeclaringTypeDemoAnalysis")]
        public string c;
    }

    public class TabifyFooResolver<T> : OdinAttributeProcessor<T> where T : Foo
    {
        // 子成员变量，只在当前类声明的变量才会被调用，也就是直接的子成员变量，从其他类继承来的，不算在此
        // 除非在接口约束中，选择的是最基础的类，才会层层传递下去
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            // 获取继承距离
            // var inheritanceDistance = member.DeclaringType.GetInheritanceDistance(typeof(Object));
            // TabGroupByDeclaringTypeDemoAnalysis 距离 Object 为 6 ,Object 为 0
            // TabGroupByDeclaringTypeDemoAnalysis 距离 Foo 为 2 ，Foo 为 0，Bar 为 1
            var inheritanceDistance = member.DeclaringType.GetInheritanceDistance(typeof(Object));

            // member.DeclaringType 获取声明此成员的类
            if (member.DeclaringType != null)
            {
                var tabName = member.DeclaringType.Name;
                attributes.Add(new TabGroupAttribute(tabName));
            }

            attributes.Add(new PropertyOrderAttribute(-inheritanceDistance));
            switch (member.Name)
            {
                case "a":
                    attributes.Add(new InfoBoxAttribute($"这个类的 inheritanceDistance 继承距离为 {inheritanceDistance}"));
                    break;
                case "d":
                    attributes.Add(new InfoBoxAttribute($"这个类的 inheritanceDistance 继承距离为 {inheritanceDistance}"));
                    break;
                case "g":
                    attributes.Add(new InfoBoxAttribute($"这个类的 inheritanceDistance 继承距离为 {inheritanceDistance}"));
                    break;
            }
        }
    }

#endif
}