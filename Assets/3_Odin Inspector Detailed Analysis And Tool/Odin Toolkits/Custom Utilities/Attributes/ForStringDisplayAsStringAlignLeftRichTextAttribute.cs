using System;

namespace Odin_Toolkits.Custom_Utilities.Attributes
{
    // 这个属性的实现是针对 string 类型的，通过 AttributeProcessor 来实现，它将直接跳过序列化系统，也无法处理序列化相关的 Attribute
    // 而默认情况下属性（Property） 是无法序列化的，因此，它不会处理，除非它序列化了，或者简单来说，不借助 [ShowInInspector] 可以显示在面板上
    // 所以直接限制它只能用于字段，而不是属性
    [AttributeUsage(AttributeTargets.Field)]
    public class ForStringDisplayAsStringAlignLeftRichTextAttribute : Attribute
    {
        public readonly int FontSize;
        public ForStringDisplayAsStringAlignLeftRichTextAttribute(int fontSize) => FontSize = fontSize;
    }
}