using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._1.OdinEditorWindow.Editor
{
    public class PublicShowToast : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        [TitleGroup("ShowToast 方法参数配置")] [LabelText("Toast 相对位置: ")] [EnumPaging]
        public ToastPosition toastPosition = ToastPosition.BottomLeft;

        [TitleGroup("ShowToast 方法参数配置")] [LabelText("前置图标: ")]
        public SdfIconType icon;

        [TitleGroup("ShowToast 方法参数配置")] [LabelText("背景颜色: ")] [InfoBox("初始透明度默认修正为 1")]
        public Color backGroundColor;

        [TitleGroup("ShowToast 方法参数配置")] [LabelText("显示时长: ")] [InfoBox("初始时间默认修正为 3 秒")]
        public float duration;

        [TitleGroup("ShowToast 方法参数配置")] [ShowInInspector] [LabelText("显示文本: ")]
        public const string Text = "这是一个 toast 消息 --- by Zeus --- ";

        protected override void OnEnable()
        {
            base.OnEnable();
            if (icon == SdfIconType.None)
            {
                icon = SdfIconType.Info;
            }

            if (duration == 0)
            {
                duration = 3f;
            }

            if (backGroundColor.a == 0)
            {
                backGroundColor.a = 1;
            }
        }

        [Button("根据配置触发 ShowToast 方法")]
        void Invoke()
        {
            ShowToast(toastPosition, icon, Text, backGroundColor, duration);
        }
    }
}