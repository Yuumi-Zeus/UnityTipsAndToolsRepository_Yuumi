using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HowToSerializeInterfaces.UnityOriginal
{
    [Serializable]
    public class InterfaceReference<TInterface, TObject> where TInterface : class where TObject : Object
    {
        [SerializeField] [HideInInspector] TObject sourceValue;

        // /// <summary>
        // /// 特殊情况用于外界设置值
        // /// </summary>
        // public TObject SourceValue
        // {
        //     get => sourceValue;
        //     set => sourceValue = value;
        // }

        /// <summary>
        /// 通常外界只能通过 Value 属性与源值 sourceValue 进行交互
        /// </summary>
        /// <exception cref="InvalidCastException">类型转化异常，没有实现对应的接口</exception>
        public TInterface Value
        {
            get
            {
                return sourceValue switch
                {
                    null => null, // 当_sourceValue为null时，返回null
                    TInterface @interface => @interface, // 当_sourceValue是期望的接口类型时，直接返回
                    _ => throw new InvalidCastException(
                        $"{sourceValue} needs to implement interface {nameof(TInterface)}") // 当_sourceValue类型不匹配时，抛出异常
                };
            }
            set
            {
                sourceValue = value switch
                {
                    null => null,
                    TObject obj => obj,
                    _ => throw new InvalidCastException($"{value} needs to be of type {typeof(TObject)}")
                };
            }
        }

        // 构造函数
        public InterfaceReference() { }
        public InterfaceReference(TObject target) => sourceValue = target;
        public InterfaceReference(TInterface value) => sourceValue = value as TObject;
    }

    /// <summary>
    /// Unity 的简化版本，因为 Unity 中的所有类都继承自 UnityEngine.Object，所以不需要泛型 TObject
    /// </summary>
    [Serializable]
    public class InterfaceReference<TInterface> : InterfaceReference<TInterface, Object> where TInterface : class { }
}