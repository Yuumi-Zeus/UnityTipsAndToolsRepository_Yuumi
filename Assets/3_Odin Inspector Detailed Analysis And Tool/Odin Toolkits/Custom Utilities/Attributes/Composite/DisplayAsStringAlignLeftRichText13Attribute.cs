using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Odin_Toolkits.Custom_Utilities.Attributes.Composite
{
    /// <summary>
    /// 由 [PropertySpace] [HideLabel] [ShowInInspector] [EnableGUI]
    /// [DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 14)] 组合而成。
    /// </summary>
    [IncludeMyAttributes, HideLabel, ShowInInspector, EnableGUI,
     DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 14)]
    public class DisplayAsStringAlignLeftRichText14Attribute : Attribute { }

    [IncludeMyAttributes, HideLabel, ShowInInspector, EnableGUI,
     DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 28)]
    public class DisplayAsStringAlignLeftRichText28Attribute : Attribute { }

    [IncludeMyAttributes, HideLabel, ShowInInspector, EnableGUI,
     DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 36)]
    public class DisplayAsStringAlignLeftRichText36Attribute : Attribute { }

    [IncludeMyAttributes, HideLabel, ShowInInspector, EnableGUI,
     DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 50)]
    public class DisplayAsStringAlignLeftRichText50Attribute : Attribute { }
}