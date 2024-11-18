using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Odin_Toolkits.Common_Utilities
{
    public enum InnerRectType
    {
        Center,
        LeftBorder,
        RightBorder,
        TopBorder,
        BottomBorder,
        TopLeftCorner,
        TopRightCorner,
        BottomLeftCorner,
        BottomRightCorner
    }

    public static class OdinEditorGUIUtil
    {
#if UNITY_EDITOR
        /// <summary>
        /// 绘制一个 Rect 的外框线，以方便在编辑器中观察
        /// </summary>
        /// <param name="rect">想要查看的 Rect</param>
        /// <param name="outlineColor">外框线颜色</param>
        /// <param name="distanceToBorder">外框线与实际 Rect 的边界的距离，默认为 1，即默认大一圈，该值不为外框线本身的厚度</param>
        public static void DrawRectOutlineWithBorder(Rect rect, Color outlineColor, float distanceToBorder = 1)
        {
            // 绘制边框
            Handles.DrawSolidRectangleWithOutline(
                new Rect(rect.x - distanceToBorder, rect.y - distanceToBorder, rect.width + distanceToBorder * 2,
                    rect.height + distanceToBorder * 2),
                Color.clear, outlineColor);
        }

        /// <summary>
        /// 从一个 Rect 中获取一个内部 Rect，
        /// </summary>
        /// <remarks>Offset 偏移符合 Rect 逻辑，左上角坐标为（0，0），先判断对齐边界类型，再计算偏移</remarks>
        /// <param name="outerRect">外部 Rect</param>
        /// <param name="innerRectType">内部 Rect 的对齐边界类型</param>
        /// <param name="innerWidth">内部 Rect 的宽</param>
        /// <param name="innerHeight">内部 Rect 的高</param>
        /// <param name="horizontalOffset">负数向左偏移，正数向右偏移</param>
        /// <param name="verticalOffset">负数向上偏移，正数向下偏移</param>
        /// <returns>内部 Rect</returns>
        /// <exception cref="ArgumentOutOfRangeException">枚举错误</exception>
        public static Rect GetInnerRectFromRect(
            Rect outerRect,
            InnerRectType innerRectType,
            float innerWidth,
            float innerHeight,
            float horizontalOffset = 0f,
            float verticalOffset = 0f)
        {
            var finalRect = innerRectType switch
            {
                InnerRectType.Center => new Rect((outerRect.width - innerWidth) / 2,
                    (outerRect.height - innerHeight) / 2, innerWidth, innerHeight),
                InnerRectType.LeftBorder => new Rect(outerRect.x, (outerRect.height - innerHeight) / 2, innerWidth,
                    innerHeight),
                InnerRectType.RightBorder => new Rect(outerRect.xMax - innerWidth,
                    (outerRect.height - innerHeight) / 2, innerWidth, innerHeight),
                InnerRectType.TopBorder => new Rect((outerRect.width - innerWidth) / 2, outerRect.y, innerWidth,
                    innerHeight),
                InnerRectType.BottomBorder => new Rect((outerRect.width - innerWidth) / 2,
                    outerRect.yMax - innerHeight, innerWidth, innerHeight),
                InnerRectType.TopLeftCorner => new Rect(outerRect.x, outerRect.y, innerWidth, innerHeight),
                InnerRectType.TopRightCorner => new Rect(outerRect.xMax - innerWidth, outerRect.y, innerWidth,
                    innerHeight),
                InnerRectType.BottomLeftCorner => new Rect(outerRect.x, outerRect.yMax - innerHeight, innerWidth,
                    innerHeight),
                InnerRectType.BottomRightCorner => new Rect(outerRect.xMax - innerWidth,
                    outerRect.yMax - innerHeight, innerWidth, innerHeight),
                _ => throw new ArgumentOutOfRangeException(nameof(innerRectType), innerRectType, null)
            };
            // 计算偏移
            finalRect.x += horizontalOffset;
            finalRect.y -= verticalOffset;
            // 要加上最开始的坐标值
            finalRect.x += outerRect.x;
            finalRect.y += outerRect.y;
            return finalRect;
        }
#endif
    }
}