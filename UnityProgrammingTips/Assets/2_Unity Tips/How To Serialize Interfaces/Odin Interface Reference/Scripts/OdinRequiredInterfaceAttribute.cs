using System;
using HowToSerializeInterfaces.Example;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HowToSerializeInterfaces.OdinInterfaceReferencePractice.Scripts
{
    [AttributeUsage(AttributeTargets.Field)]
    [IncludeMyAttributes]
    [ValidateInput("@HowToSerializeInterfaces.OdinInterfaceReferencePractice.Scripts.JudgeInterface.IsIDamageable")]
    public class OdinRequiredInterfaceAttribute : Attribute
    {
        // 接口类型
        public readonly Type InterfaceType;

        // 构造函数，接收一个接口类型参数
        public OdinRequiredInterfaceAttribute(Type interfaceType)
        {
            // 断言传递的类型必须是接口
            // 断言式，如果为 True，则不会抛出异常，否则程序会在调试模式下抛出一个断言失败的错误并显示
            Debug.Assert(interfaceType.IsInterface, $"{nameof(interfaceType)} must be an interface");
            InterfaceType = interfaceType;
        }
    }

    public class JudgeInterface
    {
        public static bool IsIDamageable(UnityEngine.Object obj) => typeof(IDamageable).IsAssignableFrom(obj.GetType());
    }
}