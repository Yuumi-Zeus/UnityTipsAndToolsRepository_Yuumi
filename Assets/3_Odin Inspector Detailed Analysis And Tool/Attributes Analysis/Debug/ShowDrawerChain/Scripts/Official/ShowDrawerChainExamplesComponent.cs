// ShowDrawerChainExamplesComponent.cs
// 要点 API
// Sirenix.Utilities.Editor.GUIHelper.RequestRepaint(); 强制重新绘制
// UnityEditor.EditorApplication.timeSinceStartup 自编辑器启动以来的时间
// UnityEditor.EditorApplication.timeSinceStartup % 3 < 1.5f; 使用这个可以表示 1.5f 刷新一次

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Attributes_Analysis.Debug.ShowDrawerChain.Scripts.Official
{
    public class ShowDrawerChainExamplesComponent : MonoBehaviour
    {
#if UNITY_EDITOR // 编辑器相关代码必须从构建中排除
        [HorizontalGroup(Order = 1)]
        [ShowInInspector, ToggleLeft]
        public bool ToggleHideIf
        {
            get
            {
                Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
                return UnityEditor.EditorApplication.timeSinceStartup % 3 < 1.5f;
            }
        }

        // 在检查器中定义一个进度条，以演示ProgressBar属性的效果
        [HorizontalGroup]
        [ShowInInspector, HideLabel, ProgressBar(0, 1.5f)]
        private double Animate
        {
            get { return Math.Abs(UnityEditor.EditorApplication.timeSinceStartup % 3 - 1.5f); }
        }
#endif

        // 显示一个信息框，后跟一个带有ShowDrawerChain属性的GameObject属性
        // 用于演示自定义抽屉和HideIf属性的效果
        [InfoBox(
            "任何未使用的抽屉将被灰显，以便更容易调试抽屉链。你可以通过切换上面的切换字段来查看这一点。\n" +
            "如果你有自定义抽屉，它们将在抽屉链中显示为绿色名称。")]
        [ShowDrawerChain]
        [HideIf("ToggleHideIf")]
        [PropertyOrder(2)]
        public GameObject SomeObject;

        // 使用Range和ShowDrawerChain属性的属性，用于演示目的
        [Range(0, 10)] [ShowDrawerChain] public float SomeRange;
    }
}