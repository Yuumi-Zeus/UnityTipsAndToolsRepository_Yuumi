# Unity Serialize

## Script Serialization 脚本序列化 

- [Unity 脚本序列化官方文档](https://docs.unity3d.com/Manual/script-serialization.html)

### 基础概念

**Serialization** is the automatic process of transforming data structures or **GameObject **states into a format that Unity can store and reconstruct later.

序列化是转换数据结构或者物体状态成为一种 Unity 能够存储和稍后重新生成的格式的自动化过程。

How you organize data in your Unity project affects how Unity serializes that data, which can have a significant impact on the performance of your project. This page outlines serialization in Unity and how to optimize your project for it.

在 Unity 项目中如何组织数据结构会影响 Unity 怎样去序列化数据，这会对你项目的性能有重大影响，本页概述了Unity的序列化以及如何去针对它优化你的项目。

#### Serialize Rule 序列化规则

Serializers in Unity are specifically designed to operate efficiently at runtime. Because of this, serialization in Unity behaves differently to serialization in other programming environments. Serializers in Unity work directly on the **fields** of your C# classes rather than their properties, so Unity only serializes your fields if they meet certain conditions. The following section outlines how to use field serialization in Unity.

Unity 中的序列化器经过专门设计，可在运行时高效运行。因此，Unity 中的序列化行为与其他编程环境中的序列化不同。 Unity 中的序列化器直接作用于 C# 类的**字段**而不是其属性，因此 Unity 仅在字段满足特定条件时才序列化字段。以下部分概述了如何在 Unity 中使用字段序列化。

> [!NOTE]
>
> Unity 不序列化属性，序列化只涉及具体的，实际的<u>**字段**</u>，属性提供了一种访问字段的方式，但它们本身并不是数据存储的地方，而是字段的<u>**包装器**</u>。可以有检查和验证逻辑。

## SerializeReference

- [Serialize Reference 参考文档链接](https://docs.unity3d.com/ScriptReference/SerializeReference.html)

### 基础概念

一个[脚本属性](https://docs.unity3d.com/Manual/Attributes.html)，指示 Unity 将字段序列化为引用而不是值。

### Unity 反序列化以及 [SerializeReference] 属性实践案例

案例代码：

```c#
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HowToSerializeInterfaces.UnitySerializeDemo.UnitySerializeAndSerializeReference
{
    public class InspectorSerializeReferenceExample : MonoBehaviour
    {
        #region 定义

        /// <summary>
        /// 没有继承 UnityEngine.Object 的基类，如果进行序列化，默认会被序列化为值
        /// </summary>
        [Serializable]
        public class BaseInspector
        {
            public int number;
        }

        [Serializable]
        public class DerivedInspector : BaseInspector
        {
            public bool show;
        }

        [Serializable]
        public class OtherInspector : BaseInspector
        {
            public int otherNumber;
        }

        #endregion

        [InfoBox("默认值的 DerivedInspector 对象，该类的 show 字段的初始值为 false")] [ReadOnly]
        public DerivedInspector nullDerivedInspector = new DerivedInspector();

        [InfoBox("声明 BaseInspector 类型字段，但是不采用 [SerializeReference] 序列化引用，" +
                 "因为 BaseInspector 没有继承 UnityEngine.Object，那么对于这种自定义类的序列化，" +
                 "如果字段类型是 Unity 可以按值自动序列化的类型（简单字段类型，如 int、string、Vector3 等），" +
                 "或者如果它是标有 [Serializable] 属性的自定义可序列化类或结构，则会将其序列化为值 \n" +
                 "所以序列化仅仅只是 BaseInspector 的 number 字段，此时字段定义的 show 初始值为 True")]
        public BaseInspector noSerializeReferenceInspector = new DerivedInspector()
        {
            number = 100,
            show = true
        };

        [InfoBox("声明 BaseInspector 类型字段，标记 [SerializeReference] 序列化引用，" +
                 "那么会以引用类型进行序列化，可以分配相同类型或者派生类型对象 \n" +
                 "该字段开启了 Odin Draw，使用 Odin 方式绘制 Inspector，可以序列化多种对象，可以在 Inspector 面板修改引用对象的类型")]
        [SerializeReference]
        public BaseInspector odinDrawSerializeReferenceInspector = new DerivedInspector()
        {
            number = 8,
            show = true
        };

        [InfoBox("声明 BaseInspector 类型字段，标记 [SerializeReference] 序列化引用，" +
                 "那么会以引用类型进行序列化，可以分配相同类型或者派生类型对象 \n" +
                 "该字段使用 Unity 原生绘制，可以序列化对象，但是必须修改代码，调用 Reset 重绘，才能修改引用对象的类型")]
        [DrawWithUnity]
        [SerializeReference]
        public BaseInspector unityDrawSerializeReferenceInspector = new OtherInspector()
        {
            number = 10,
            otherNumber = 100
        };

        void Start()
        {
            Debug.Log($"--- 开始检查 {nameof(nullDerivedInspector)} 字段序列化 ---");
            Debug.Log($"{nameof(nullDerivedInspector)} 的类型为 {nullDerivedInspector.GetType()}");
            Debug.Log(
                $"{nameof(nullDerivedInspector)}.number 的值为 {nullDerivedInspector.number}, " +
                $"{nameof(nullDerivedInspector)}.show 的值为 {nullDerivedInspector.show}");
            Debug.Log($"--- 开始检查 {nameof(noSerializeReferenceInspector)} 字段序列化 ---");
            Debug.Log($"{nameof(noSerializeReferenceInspector)} 的类型为 {noSerializeReferenceInspector.GetType()}");
            Debug.Log(
                $"{nameof(noSerializeReferenceInspector)}.number 的值为 {noSerializeReferenceInspector.number}, " +
                $"{nameof(noSerializeReferenceInspector)}.show 的值为 {((DerivedInspector)noSerializeReferenceInspector).show}");
            Debug.Log(
                "此时 number 的值 == Inspector 面板值，而 show 的值为该字段定义的初始值 true ，" +
                "说明字段的初始值赋值是有效的，同时 number 序列化的值也是有效的，覆盖了字段初始值的定义" +
                "结论为反序列化的赋值是在使用构造函数生成对象之后，再进行赋值的，所以会覆盖部分值");
            Debug.Log($"--- 开始检查 {nameof(odinDrawSerializeReferenceInspector)} 字段序列化 ---");
            Debug.Log(
                $"{nameof(odinDrawSerializeReferenceInspector)} 的类型为 {odinDrawSerializeReferenceInspector.GetType()}");
            Debug.Log(
                $"{nameof(odinDrawSerializeReferenceInspector)}.number 的值为 {odinDrawSerializeReferenceInspector.number}");
            switch (odinDrawSerializeReferenceInspector)
            {
                case DerivedInspector derived:
                    Debug.Log(
                        $"{nameof(odinDrawSerializeReferenceInspector)}.show 的值为 {derived.show}");
                    break;
                case OtherInspector otherInspector:
                    Debug.Log(
                        $"{nameof(odinDrawSerializeReferenceInspector)}.otherNumber 的值为 {otherInspector.otherNumber}");
                    break;
            }

            Debug.Log($"--- 开始检查 {nameof(unityDrawSerializeReferenceInspector)} 字段序列化 ---");
            Debug.Log(
                $"{nameof(unityDrawSerializeReferenceInspector)} 的类型为 {unityDrawSerializeReferenceInspector.GetType()}");
        }
    }
}
```

案例结果截图：

<img src="./Unity 序列化学习文档.assets/UnitySerializeAndSerializeReferenceExampleRuntime.png" style="zoom: 67%;" />

## 结论总结

1. **<u>Inspector 的优先级高于类生成时的字段初始化</u>**，原因是 Unity 中的反序列化是在类对象生成之后，对类对象的字段进行赋值。而不是在类生成时进行反序列化赋初始值。

> [!IMPORTANT]
>
> 反序列化过程解析 - 通义AI
>
> 1. 创建对象实例
> 2. 读取存储的数据，并将这些数据赋值给对象的各个字段

2. 序列化的简单含义是可以在 Inspector 面板中修改值，并且可以通过 Unity 自身的反序列化作用到具体的类对象中。
3. 如果一个类继承自 UnityEngine.Object ，那么这个类型的字段默认会使用序列化引用，指向的是一个类对象，而其他没有继承 UnityEngine.Object 的自定义类或结构，在使用`[Serializable]` 标记后，默认会以值进行序列化，此时使用`[SerializeReference]`属性标记，则会以引用对象进行序列化。
4. 使用`[SerializeReference]` 主要有三种情况：
   1. 希望对自定义可序列化类的同一实例进行多个引用
   2. 希望对类型为自定义可序列化类的字段使用多态性，即使用抽象类或者基类进行存储（重点）
   3. 想要序列化 null 值

