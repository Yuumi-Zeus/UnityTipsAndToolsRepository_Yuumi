using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._3.UnityEditorGUIEffect.Editor
{
    public class UnityEditorGUILayoutEffect : OdinEditorWindow
    {
        [MenuItem(OdinToolkitsConfig.OdinEditorAPIUsageGuidePath + "/" + "UnityEditorGUILayout效果（简易版）")]
        static void OpenWindow()
        {
            var win = GetWindow<UnityEditorGUILayoutEffect>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        #region 7.帮助盒子和间隔

        [PropertyOrder(100), Title("帮助盒子和间隔"), OnInspectorGUI]
        void ShowEffect7()
        {
            EditorGUILayout.HelpBox(new GUIContent("这是一个帮助盒子", OdinEditorResources.OdinLogo));
            // 默认是 6f
            EditorGUILayout.Space(10f);
            EditorGUILayout.HelpBox("这是一个帮助盒子", MessageType.Info);
            EditorGUILayout.HelpBox("这是一个帮助盒子", MessageType.Warning);
            EditorGUILayout.Space(10f);
            EditorGUILayout.HelpBox("这是一个帮助盒子", MessageType.Error);
        }

        #endregion

        // [PropertyOrder(100)]
        // [Title("帮助盒子和垂直间隔")]
        // [OnInspectorGUI]
        // void ShowEffect3()
        // {
        //     
        // }

        // [PropertyOrder(100)]
        // [Title("帮助盒子和垂直间隔")]
        // [OnInspectorGUI]
        // void ShowEffect3()
        // {
        //     
        // }
        // 此处直接当成 OnGUI,但是不要删除 base.DrawEditors(); 这一部分实现 Odin 特性的绘制
        protected override void DrawEditors()
        {
            base.DrawEditors();
        }


        #region 1.文本，标签，颜色，枚举

        int _layer;
        string _tag;
        Color _color;

        enum TestType
        {
            One = 1,
            Two = 2,
            Three = 4
        }

        TestType _testType;
        TestType _testType2;

        [PropertyOrder(0), Title("文本，标签，颜色，枚举"), OnInspectorGUI]
        void ShowEffect()
        {
            EditorGUILayout.LabelField("EditorGUILayout.LabelField 用法", new GUIStyle
            {
                active = new GUIStyleState
                {
                    background = Texture2D.blackTexture
                },
                alignment = TextAnchor.MiddleCenter
            });

            // 文本控件
            EditorGUILayout.LabelField(new GUIContent("标题", OdinEditorResources.OdinInspectorLogo, "Odin"),
                SirenixGUIStyles.Label);
            EditorGUILayout.LabelField("UnityEditorGUI效果一览", "测试内容");
            // 使用 GUILayout 中可以返回 GUILayoutOption 的方法进行设置
            EditorGUILayout.LabelField("测试 GUILayoutOption", GUILayout.Width(100), GUILayout.MinWidth(50),
                GUILayout.MaxHeight(50), GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));
            // 层级标签控件
            _layer = EditorGUILayout.LayerField("层级选择", _layer);
            // Tag 
            _tag = EditorGUILayout.TagField("Tag选择", _tag);
            // 颜色选择控件
            _color = EditorGUILayout.ColorField(new GUIContent("自定义颜色", OdinEditorResources.OdinLogo), _color, true,
                true, true);
            // 枚举选择
            _testType = (TestType)EditorGUILayout.EnumPopup("Unity 枚举选择", _testType);
            _testType2 = (TestType)EditorGUILayout.EnumFlagsField("Unity 枚举多选", _testType2);
        }

        #endregion

        #region 2.整数选择，下拉按钮

        readonly string[] _strings = { "One", "Two", "Three" };

        readonly int[] _ints = { 1, 2, 3 };

        int _number;

        [PropertyOrder(5), Title("整数选择，下拉按钮"), OnInspectorGUI]
        void ShowEffect2()
        {
            // 整数选择
            _number = EditorGUILayout.IntPopup("整数选择", _number, _strings, _ints);
            EditorGUILayout.LabelField("当前选择的整数: ", _number.ToString());

            var isDropdowm = EditorGUILayout.DropdownButton(
                new GUIContent("下拉按钮", OdinEditorResources.OdinLogo, "Odin"),
                FocusType.Passive, GUILayout.Width(100));
            // 按下就响应的按钮
            if (isDropdowm) Debug.Log("按钮被点击了");
        }

        #endregion

        #region 3.对象选择，纹理选择，字段输入框

        GameObject _gameObject;
        Texture2D _texture2D;

        int _i;
        long _long;
        float _float;
        double _double;

        string _string;
        Vector2 _vector2;
        Vector3 _vector3;
        Vector4 _vector4;
        Rect _rect;
        Bounds _bounds;
        BoundsInt _boundsInt;

        [PropertyOrder(15), Title("对象选择，纹理选择，字段输入框"), OnInspectorGUI]
        void ShowEffect3()
        {
            // 整数选择
            _number = EditorGUILayout.IntPopup("整数选择", _number, _strings, _ints);
            EditorGUILayout.LabelField("当前选择的整数: ", _number.ToString());

            var isDropdowm = EditorGUILayout.DropdownButton(
                new GUIContent("下拉按钮", OdinEditorResources.OdinLogo, "Odin"),
                FocusType.Passive, GUILayout.Width(100));
            // 按下就响应的按钮
            if (isDropdowm) Debug.Log("按钮被点击了");
            // 对象选择控件
            // EditorGUILayout.ObjectField("对象选择", null, typeof(GameObject), true);
            // EditorGUILayout.ObjectField("对象选择", null, typeof(GameObject), true, GUILayout.Width(200));
            // EditorGUILayout.ObjectField("对象选择", null, typeof(GameObject), true, GUILayout.Height(200));
            _gameObject = EditorGUILayout.ObjectField("对象选择", _gameObject, typeof(GameObject), true) as GameObject;
            _texture2D = EditorGUILayout.ObjectField("纹理选择", _texture2D, typeof(Texture2D), true) as Texture2D;

            _i = EditorGUILayout.IntField("Int输入框: ", _i);
            _long = EditorGUILayout.LongField("long输入框: ", _long);
            _float = EditorGUILayout.FloatField("Float 输入：", _float);
            _double = EditorGUILayout.DoubleField("double 输入：", _double);

            _string = EditorGUILayout.TextField("Text输入：", _string);
            _vector2 = EditorGUILayout.Vector2Field("Vec2输入： ", _vector2);
            _vector3 = EditorGUILayout.Vector3Field("Vec3输入： ", _vector3);
            _vector4 = EditorGUILayout.Vector4Field("Vec4输入： ", _vector4);
            _rect = EditorGUILayout.RectField("rect输入： ", _rect);
            _bounds = EditorGUILayout.BoundsField("Bounds输入： ", _bounds);
            _boundsInt = EditorGUILayout.BoundsIntField("Bounds输入： ", _boundsInt);

            // string变量 = EditorGUILayout.TextField("Text输入：", string变量);
            // vector2变量 = EditorGUILayout.Vector2Field("Vec2输入： ", vector2变量);
            // vector3变量 = EditorGUILayout.Vector3Field("Vec3输入： ", vector3变量);
            // vector4变量 = EditorGUILayout.Vector4Field("Vec4输入： ", vector4变量);
            // rect变量 = EditorGUILayout.RectField("rect输入： ", rect变量);
            // bounds变量 = EditorGUILayout.BoundsField("Bounds输入： ", bounds变量);
            // boundsInt变量 = EditorGUILayout.BoundsIntField("Bounds输入： ", boundsInt变量);

            //EditorGUILayout中还有一些Delayed开头的输入控件他们和普通输入控件最主要的区别是：在用户按 Enter 键或将焦点从字段移开之前，返回值不会更改
        }

        #endregion

        #region 4.折叠

        bool _isFoldout;
        bool _isFoldoutGroup;

        [PropertyOrder(20), Title("折叠"), OnInspectorGUI]
        void ShowEffect4()
        {
            _isFoldout = EditorGUILayout.Foldout(_isFoldout, "折叠控件", true);

            if (_isFoldout) EditorGUILayout.LabelField("折叠控件内容");

            // 折叠组控件
            _isFoldoutGroup = EditorGUILayout.BeginFoldoutHeaderGroup(_isFoldoutGroup, "折叠组控件");
            if (_isFoldoutGroup) EditorGUILayout.LabelField("折叠组控件内容");

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        #endregion

        #region 5.开关组

        bool _isToggle;
        bool _isToggleLeft;
        bool _isToggleGroup;

        [PropertyOrder(25), Title("开关组"), OnInspectorGUI]
        void ShowEffect5()
        {
            _isToggle = EditorGUILayout.Toggle(new GUIContent("开关控件类似折叠", OdinEditorResources.OdinLogo), _isToggle);
            if (_isToggle) EditorGUILayout.LabelField("开关控件内容");

            _isToggleLeft =
                EditorGUILayout.ToggleLeft(new GUIContent("左侧开关控件", OdinEditorResources.OdinValidatorLogo),
                    _isToggleLeft);
            if (_isToggleLeft) EditorGUILayout.LabelField("左侧开关控件对齐内容");

            _isToggleGroup = EditorGUILayout.BeginToggleGroup("开关组控件", _isToggleGroup);
            if (_isToggleGroup) EditorGUILayout.LabelField("开关组控件内容");

            EditorGUILayout.EndToggleGroup();
        }

        #endregion

        #region 6.滑动条

        float _floatSlider;
        int _intSlider;
        float _minMaxSliderMin;
        float _minMaxSliderMax;

        [PropertyOrder(100), Title("滑动条"), OnInspectorGUI]
        void ShowEffect6()
        {
            // 滑动条控件
            _floatSlider = EditorGUILayout.Slider("Float滑动条", _floatSlider, 0, 100);
            _intSlider = EditorGUILayout.IntSlider("Int滑动条", _intSlider, 0, 100);
            // 双块滑动条控件
            _minMaxSliderMin = EditorGUILayout.FloatField("双块滑动条最小值", _minMaxSliderMin);
            _minMaxSliderMax = EditorGUILayout.FloatField("双块滑动条最大值", _minMaxSliderMax);
            EditorGUILayout.MinMaxSlider("双块滑动条", ref _minMaxSliderMin, ref _minMaxSliderMax, 0, 100);
        }

        #endregion

        #region 8.动画曲线控件和布局API

        AnimationCurve _animationCurve = new AnimationCurve();
        Vector2 _scrollPosition;

        [PropertyOrder(100), Title("动画曲线控件和布局API"), OnInspectorGUI]
        void ShowEffect8()
        {
            _animationCurve = EditorGUILayout.CurveField(new GUIContent("动画曲线", OdinEditorResources.OdinValidatorLogo),
                _animationCurve);

            // 布局 API
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("水平布局", GUILayout.Width(100));
            EditorGUILayout.LabelField("水平布局", GUILayout.Width(100));
            EditorGUILayout.LabelField("水平布局", GUILayout.Width(100));
            EditorGUILayout.LabelField("水平布局", GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("垂直布局", GUILayout.Width(100));
            EditorGUILayout.LabelField("垂直布局", GUILayout.Width(100));
            EditorGUILayout.EndVertical();

            // Odin 默认自带滚动视图，内部再次使用有显示冲突
            // _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            // EditorGUILayout.LabelField("滚动视图", GUILayout.Width(100));
            // EditorGUILayout.LabelField("滚动视图", GUILayout.Width(100));
            // EditorGUILayout.LabelField("滚动视图", GUILayout.Width(100));
            // EditorGUILayout.LabelField("滚动视图", GUILayout.Width(100));
            // EditorGUILayout.LabelField("滚动视图", GUILayout.Width(100));
            // EditorGUILayout.LabelField("滚动视图", GUILayout.Width(100));
            // EditorGUILayout.EndScrollView();
        }

        #endregion
    }
}