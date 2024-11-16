using System;
using UnityEngine;

// 定义一个特性用于指示字段需要实现特定的接口
// 此特性仅适用于字段
namespace _2_Unity_Tips.How_To_Serialize_Interfaces.UnityOriginal
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RequireInterfaceAttribute : PropertyAttribute
    {
        // 接口类型
        public readonly Type InterfaceType;

        // 构造函数，接收一个接口类型参数
        public RequireInterfaceAttribute(Type interfaceType)
        {
            // 断言传递的类型必须是接口
            // 断言式，如果为 True，则不会抛出异常，否则程序会在调试模式下抛出一个断言失败的错误并显示
            Debug.Assert(interfaceType.IsInterface, $"{nameof(interfaceType)} must be an interface");
            InterfaceType = interfaceType;
        }
    }
}