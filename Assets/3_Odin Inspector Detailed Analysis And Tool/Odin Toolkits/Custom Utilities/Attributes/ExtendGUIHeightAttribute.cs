using System;

namespace Odin_Toolkits.Custom_Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ExtendGUIHeightAttribute : Attribute
    {
        public readonly float Height;

        public ExtendGUIHeightAttribute(float height) => Height = height;
    }
}