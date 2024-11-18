using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._2.SirenixGUIStylesGraphic.Editor
{
    public enum TestType
    {
        One,
        Two,
        Three
    }

    public class SirenixGUIStylesGraphic : OdinEditorWindow
    {
        bool _isFoldout;

        bool _isFoldout2;
        bool _isFoldoutGroup;

        bool _isToggle;
        bool _isToggleGroup;
        bool _isToggleLeft;
        bool _isToggleLeft2;
        bool _isToggleLeft3;

        string _tag;

        TestType _testType;

        [MenuItem(OdinToolkitsConfig.OdinEditorAPIUsageGuidePath + "/" + "SirenixGUIStyles 样式（简易版）")]
        static void OpenWindow()
        {
            var win = GetWindow<SirenixGUIStylesGraphic>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        [PropertyOrder(10), TitleGroup("基础的文字 Label 相关样式"), BoxGroup("基础的文字 Label 相关样式/BoxGroup", ShowLabel = false),
         OnInspectorGUI]
        void Text()
        {
            EditorGUILayout.LabelField("SirenixGUIStyles.BoldLabel 样式", SirenixGUIStyles.BoldLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.BoldLabelCentered 样式", SirenixGUIStyles.BoldLabelCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.Label 样式", SirenixGUIStyles.Label);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.HighlightedLabel 样式", SirenixGUIStyles.HighlightedLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.WhiteLabel 样式", SirenixGUIStyles.WhiteLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.BlackLabel 样式", SirenixGUIStyles.BlackLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.LabelCentered 样式", SirenixGUIStyles.LabelCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.RichTextLabelCentered 样式",
                SirenixGUIStyles.RichTextLabelCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.WhiteLabelCentered 样式", SirenixGUIStyles.WhiteLabelCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.BlackLabelCentered 样式", SirenixGUIStyles.BlackLabelCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MiniLabelCentered 样式", SirenixGUIStyles.MiniLabelCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.LeftAlignedCenteredLabel 样式",
                SirenixGUIStyles.LeftAlignedCenteredLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.LeftAlignedGreyMiniLabel 样式",
                SirenixGUIStyles.LeftAlignedGreyMiniLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.LeftAlignedGreyLabel 样式",
                SirenixGUIStyles.LeftAlignedGreyLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.CenteredGreyMiniLabel 样式",
                SirenixGUIStyles.CenteredGreyMiniLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.LeftAlignedWhiteMiniLabel 样式",
                SirenixGUIStyles.LeftAlignedWhiteMiniLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.CenteredWhiteMiniLabel 样式",
                SirenixGUIStyles.CenteredWhiteMiniLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.CenteredBlackMiniLabel 样式",
                SirenixGUIStyles.CenteredBlackMiniLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MultiLineWhiteLabel 样式", SirenixGUIStyles.MultiLineWhiteLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MultiLineLabel 样式", SirenixGUIStyles.MultiLineLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MultiLineCenteredLabel 样式",
                SirenixGUIStyles.MultiLineCenteredLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.CenteredTextField 样式", SirenixGUIStyles.CenteredTextField);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.RichTextLabel 样式", SirenixGUIStyles.RichTextLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.RightAlignedGreyMiniLabel 样式",
                SirenixGUIStyles.RightAlignedGreyMiniLabel);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.RightAlignedWhiteMiniLabel 样式",
                SirenixGUIStyles.RightAlignedWhiteMiniLabel);
            EditorGUILayout.Space(10f);
        }

        [PropertyOrder(15), TitleGroup("标题 Title 相关样式"), BoxGroup("标题 Title 相关样式/BoxGroup", ShowLabel = false),
         OnInspectorGUI]
        void Text2()
        {
            EditorGUILayout.LabelField("SirenixGUIStyles.Title 样式", SirenixGUIStyles.Title);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.BoldTitle 样式", SirenixGUIStyles.BoldTitle);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.BoldTitleCentered 样式", SirenixGUIStyles.BoldTitleCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.BoldTitleRight 样式", SirenixGUIStyles.BoldTitleRight);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.TitleCentered 样式", SirenixGUIStyles.TitleCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.TitleRight 样式", SirenixGUIStyles.TitleRight);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.Subtitle 样式", SirenixGUIStyles.Subtitle);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.SubtitleCentered 样式", SirenixGUIStyles.SubtitleCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.SubtitleRight 样式", SirenixGUIStyles.SubtitleRight);
            EditorGUILayout.Space(10f);
        }

        [PropertyOrder(15), TitleGroup("TagButton 相关样式"), InfoBox("内部使用的是(GUIStyle) \"MiniToolbarButton\""),
         BoxGroup("TagButton 相关样式/BoxGroup", ShowLabel = false), OnInspectorGUI]
        void Text3()
        {
            _tag = EditorGUILayout.TagField(
                new GUIContent("SirenixGUIStyles.TagButton 样式", OdinEditorResources.OdinLogo), _tag,
                SirenixGUIStyles.TagButton);
            EditorGUILayout.Space(10f);
        }

        [PropertyOrder(20), TitleGroup("枚举 Pop 相关样式"), BoxGroup("枚举 Pop 相关样式/BoxGroup", ShowLabel = false),
         OnInspectorGUI]
        void Text4()
        {
            _testType = (TestType)EditorGUILayout.EnumPopup("Odin 枚举选择", _testType, SirenixGUIStyles.Popup);
            EditorGUILayout.Space(10f);
        }

        [PropertyOrder(21), TitleGroup("DropdownButton相关样式"),
         BoxGroup("DropdownButton相关样式/BoxGroup", ShowLabel = false),
         InfoBox("DropdownButton相关样式,有一个下拉符号的按钮？，应该是点击，然后接下来绘制下拉菜单"), OnInspectorGUI]
        void Text5()
        {
            var isDropdowm = EditorGUILayout.DropdownButton(new GUIContent("下拉按钮", EditorIcons.UnityGameObjectIcon),
                FocusType.Passive, SirenixGUIStyles.DropDownMiniButton);
            // 按下就响应的按钮
            if (isDropdowm) Debug.Log("按钮被点击了");

            EditorGUILayout.Space(10f);
        }

        [PropertyOrder(22), TitleGroup("Foldout相关样式"), BoxGroup("Foldout相关样式/BoxGroup", ShowLabel = false),
         InfoBox("折叠组件的样式，内部和 Unity 的 Foldout 很相似，如果 Unity 原生没问题就不用换"), InfoBox(
             "以下是折叠组控件，使用 BeginFoldoutHeaderGroup 和 EndFoldoutHeaderGroup 来实现，" +
             "同时使用了 SirenixGUIStyles.ModuleHeader ，可以看到折叠组控件的范围是一整行，可以和下一个对比，但是反而丢失了箭头标识"), OnInspectorGUI]
        void Text6()
        {
            _isFoldout = EditorGUILayout.Foldout(_isFoldout, "折叠控件", true, SirenixGUIStyles.Foldout);

            if (_isFoldout) EditorGUILayout.LabelField("折叠控件内容");

            EditorGUILayout.Space(10f);
            // ShurikenModuleTitle = SirenixGUIStyles.ModuleHeader
            // 折叠组控件
            _isFoldoutGroup =
                EditorGUILayout.BeginFoldoutHeaderGroup(_isFoldoutGroup, "折叠组控件", SirenixGUIStyles.ModuleHeader);
            if (_isFoldoutGroup) EditorGUILayout.LabelField("折叠组控件内容");

            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space(10f);
        }

        [PropertyOrder(25), TitleGroup("ModuleHeader 样式"), BoxGroup("ModuleHeader 样式/BoxGroup", ShowLabel = false),
         InfoBox("内部使用 Unity 的 ShurikenModuleTitle "), OnInspectorGUI]
        void Text7()
        {
            // ShurikenModuleTitle = SirenixGUIStyles.ModuleHeader
            _isFoldout2 = EditorGUILayout.Foldout(_isFoldout2, "用于测试 SirenixGUIStyles.ModuleHeader 的折叠控件", true,
                SirenixGUIStyles.ModuleHeader);

            if (_isFoldout2) EditorGUILayout.LabelField("折叠控件内容");


            EditorGUILayout.Space(10f);
        }

        [PropertyOrder(25), TitleGroup("Toggle 相关样式"), BoxGroup("Toggle 相关样式/BoxGroup", ShowLabel = false),
         InfoBox("Zeus！总体不推荐直接单独使用，使用时可以参考内部实现，重新设计 GUIStyle，或者直接使用 Odin 特性绘制即可", InfoMessageType.Warning),
         InfoBox("一共有四个，ToggleGroupBackground,ToggleGroupCheckbox,ToggleGroupPadding,ToggleGroupTitleBg"),
         InfoBox("这些样式应该是在 Toggle 的基础上进行其他样式的绘制，而不是直接作用到 EditorGUILayout.Toggle"), InfoBox(
             "SirenixGUIStyles.toggleGroupCheckbox 内部实现 = new GUIStyle((GUIStyle) \"ShurikenCheckMark\"，只能作用到左侧 ToggleLeft 控件上"),
         InfoBox(
             "SirenixGUIStyles.ToggleGroupPadding 内部 =  new GUIStyle(GUIStyle.none){padding = new RectOffset(5, 5, 5, 5)};看起来几乎没有用处"),
         OnInspectorGUI]
        void Text8()
        {
            _isToggle = EditorGUILayout.ToggleLeft("SirenixGUIStyles.ToggleGroupBackground", _isToggle,
                SirenixGUIStyles.ToggleGroupBackground);

            if (_isToggle) EditorGUILayout.LabelField("左侧开关控件内容,SirenixGUIStyles.ToggleGroupBackground");

            _isToggleLeft = EditorGUILayout.ToggleLeft("开关控件，SirenixGUIStyles.ToggleGroupCheckbox", _isToggleLeft,
                SirenixGUIStyles.ToggleGroupCheckbox);
            if (_isToggleLeft) EditorGUILayout.LabelField("开关控件内容,SirenixGUIStyles.ToggleGroupCheckbox");

            _isToggleLeft2 = EditorGUILayout.Toggle("开关控件，SirenixGUIStyles.ToggleGroupPadding", _isToggleLeft2,
                SirenixGUIStyles.ToggleGroupPadding);
            if (_isToggleLeft2) EditorGUILayout.LabelField("开关控件内容,SirenixGUIStyles.ToggleGroupPadding");

            _isToggleLeft3 = EditorGUILayout.ToggleLeft("左侧开关控件，SirenixGUIStyles.ToggleGroupTitleBg", _isToggleLeft3,
                SirenixGUIStyles.ToggleGroupTitleBg);
            if (_isToggleLeft3) EditorGUILayout.LabelField("左侧开关控件内容,SirenixGUIStyles.ToggleGroupTitleBg");

            // 开关组控件
            // 在EditorWindow中就和唐老狮编辑器课程中显示不同，这里的开关组控件是可以折叠的，而不是失效显示的
            _isToggleGroup = EditorGUILayout.BeginToggleGroup(new GUIContent("开关组控件", OdinEditorResources.OdinLogo),
                _isToggleGroup);
            if (_isToggleGroup) EditorGUILayout.LabelField("这是开关组控件内容!");

            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(10f);
        }

        [PropertyOrder(100), TitleGroup("暂未找到使用场景进行归类的样式"), OnInspectorGUI]
        void Text100()
        {
            EditorGUILayout.LabelField("SirenixGUIStyles.BoxContainer 样式", SirenixGUIStyles.BoxContainer);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.BoxHeaderStyle 样式", SirenixGUIStyles.BoxHeaderStyle);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.Button 样式", SirenixGUIStyles.Button);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ButtonLeft 样式", SirenixGUIStyles.ButtonLeft);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ButtonMid 样式", SirenixGUIStyles.ButtonMid);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ButtonRight 样式", SirenixGUIStyles.ButtonRight);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MiniButton 样式", SirenixGUIStyles.MiniButton);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MiniButtonLeft 样式", SirenixGUIStyles.MiniButtonLeft);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MiniButtonMid 样式", SirenixGUIStyles.MiniButtonMid);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MiniButtonRight 样式", SirenixGUIStyles.MiniButtonRight);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ColorFieldBackground 样式",
                SirenixGUIStyles.ColorFieldBackground);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.IconButton 样式", SirenixGUIStyles.IconButton);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ListItem 样式", SirenixGUIStyles.ListItem);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MenuButtonBackground 样式",
                SirenixGUIStyles.MenuButtonBackground);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.None 样式", SirenixGUIStyles.None);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.OdinEditorWrapper 样式", SirenixGUIStyles.OdinEditorWrapper);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.PaddingLessBox 样式", SirenixGUIStyles.PaddingLessBox);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ContentPadding 样式", SirenixGUIStyles.ContentPadding);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.PropertyPadding 样式", SirenixGUIStyles.PropertyPadding);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.PropertyMargin 样式", SirenixGUIStyles.PropertyMargin);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.SectionHeader 样式", SirenixGUIStyles.SectionHeader);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.SectionHeaderCentered 样式",
                SirenixGUIStyles.SectionHeaderCentered);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ToolbarBackground 样式", SirenixGUIStyles.ToolbarBackground);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ToolbarButton 样式", SirenixGUIStyles.ToolbarButton);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ToolbarButtonSelected 样式",
                SirenixGUIStyles.ToolbarButtonSelected);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ToolbarSearchCancelButton 样式",
                SirenixGUIStyles.ToolbarSearchCancelButton);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ToolbarSearchTextField 样式",
                SirenixGUIStyles.ToolbarSearchTextField);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ToolbarTab 样式", SirenixGUIStyles.ToolbarTab);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.MessageBox 样式", SirenixGUIStyles.MessageBox);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.DetailedMessageBox 样式", SirenixGUIStyles.DetailedMessageBox);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.BottomBoxPadding 样式", SirenixGUIStyles.BottomBoxPadding);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.PaneOptions 样式", SirenixGUIStyles.PaneOptions);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.ContainerOuterShadow 样式",
                SirenixGUIStyles.ContainerOuterShadow);
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("SirenixGUIStyles.CardStyle 样式", SirenixGUIStyles.CardStyle);
            EditorGUILayout.Space(10f);

            // 部分被标记过时的样式 --------------------------------
            // EditorGUILayout.LabelField("SirenixGUIStyles.ButtonSelected 样式", SirenixGUIStyles.ButtonSelected);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.ContainerOuterShadowGlow 样式",SirenixGUIStyles.ContainerOuterShadowGlow);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.ToolbarSeachTextField 样式", SirenixGUIStyles.ToolbarSeachTextField);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.ToolbarSeachCancelButton 样式", SirenixGUIStyles.ToolbarSeachCancelButton);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.ButtonLeftSelected 样式", SirenixGUIStyles.ButtonLeftSelected);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.ButtonMidSelected 样式", SirenixGUIStyles.ButtonMidSelected);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.ButtonRightSelected 样式", SirenixGUIStyles.ButtonRightSelected);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.MiniButtonSelected 样式", SirenixGUIStyles.MiniButtonSelected);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.MiniButtonLeftSelected 样式", SirenixGUIStyles.MiniButtonLeftSelected);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.MiniButtonMidSelected 样式", SirenixGUIStyles.MiniButtonMidSelected);
            // EditorGUILayout.Space(10f);
            // EditorGUILayout.LabelField("SirenixGUIStyles.MiniButtonRightSelected 样式", SirenixGUIStyles.MiniButtonRightSelected);
            // EditorGUILayout.Space(10f);
            // 部分被标记过时的样式 --------------------------------
        }
    }
}