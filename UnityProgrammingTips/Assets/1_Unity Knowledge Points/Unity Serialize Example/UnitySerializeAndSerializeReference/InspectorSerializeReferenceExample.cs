using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HowToSerializeInterfaces.UnitySerializeLearnExample.UnitySerializeAndSerializeReference
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