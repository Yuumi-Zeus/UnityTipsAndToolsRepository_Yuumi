using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._6.UnityEditorGUIUtility.Editor
{
    public class UnityEditorGUIUtility : OdinEditorWindow
    {
        [MenuItem("Zeus Framework/Odin 扩展模块/Editor API 演示/6.UnityEditorGUIUtility API", priority = 45)]
        static void OpenWindow()
        {
            var win = GetWindow<UnityEditorGUIUtility>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 800);
            win.Show();
        }


        // 类似于显示初始化
        [OnInspectorInit]
        void CreateData() { }

        #region 1.资源加载

        [LabelText("图片纹理")] public Texture img;

        [Title("资源加载")]
        [OnInspectorGUI]
        void OnCustomGUI1()
        {
            if (GUILayout.Button("加载编辑器图片资源"))
            {
                img = EditorGUIUtility.Load("ZeusFramework.png") as Texture;
            }

            if (img != null)
            {
                GUI.DrawTexture(new Rect(10, 50, 100, 100), img);
            }

            if (GUILayout.Button("加载编辑器图片资源，要求存在"))
            {
                img = EditorGUIUtility.LoadRequired("ZeusFramework.png") as Texture;
            }

            if (img != null)
            {
                GUI.DrawTexture(new Rect(10, 50, 100, 100), img);
            }
        }

        #endregion

        #region 2.搜索框查询和对象选中

        [Title("搜索框查询和对象选中")]
        [OnInspectorGUI]
        void OnCustomGUI2()
        {
            if (GUILayout.Button("打开搜索框查询窗口"))
            {
                EditorGUIUtility.ShowObjectPicker<Texture>(null, true, "Zeus", 0);
            }

            // 在 Picker 窗口中操作时，会发送事件给打开这个搜索框的界面
            // 检查当前Unity编辑器中的事件（Event.current）是否为“ObjectSelectorUpdated”
            // 这种事件通常在用户通过鼠标操作完成对象选择器（如项目视图、层次视图等）更新时触发
            if (Event.current.commandName == "ObjectSelectorUpdated")
            {
                // 使用 EditorGUIUtility.GetObjectPickerObject() 获取当前对象选择器选中的物体
                // 此处将其强制转换为 Texture 类型，假设用户选择的是纹理资源
                img = EditorGUIUtility.GetObjectPickerObject() as Texture;
                if (img != null)
                {
                    Debug.Log(img.name);
                }
            }
            else if (Event.current.commandName == "ObjectSelectorClosed")
            {
                img = EditorGUIUtility.GetObjectPickerObject() as Texture;
                if (img != null)
                {
                    Debug.Log("搜索选择框关闭: " + img.name);
                }
            }

            if (GUILayout.Button("高亮选中对象"))
            {
                if (img != null)
                {
                    EditorGUIUtility.PingObject(img);
                }
            }
        }

        #endregion

        #region 3.窗口事件传递和坐标转换

        Event _customEvent;
        Vector2 _mousePosition;

        [OnInspectorInit]
        void DefineEvent()
        {
            _customEvent = EditorGUIUtility.CommandEvent("ZeusOdinCustomEvent");
            Debug.Log("声明创造一个事件");
        }

        [Title("窗口事件传递和坐标转换")]
        [InfoBox("Zeus 注意: 转换函数在如果包裹在布局函数中，那么位置需要加上布局的偏移，再进行转换！Odin 内部有布局，但是不清楚实现，无法准确计算偏移。")]
        [InfoBox("Zeus 注意: 在 Odin 中的鼠标位置，是从开始 Odin 绘制的位置开始计算的，最上面的部分是ImGUI方法内的，" +
                 "鼠标位置的起点不确定，Odin内部在某些情况下会转换" +
                 "而鼠标位置的Y轴的0点在ImGUI绘制结束点，X是窗口左侧", InfoMessageType.Warning)]
        [InfoBox("Zeus 注意: 在 Odin 转换坐标函数中，X = 50 转换后的X的0点是距离屏幕左侧50的位置，X往左为正，往右为负" +
                 "Y= 50 转换之后的Y的0点是距离屏幕上方的50的位置，往下为负", InfoMessageType.Warning)]
        [OnInspectorGUI]
        void OnCustomGUI3()
        {
            // 第一步，声明事件 声明一个特殊的自定义事件，只需要声明一次就行 
            // 第二步，发送事件，让其他 window 发送事件，然后在其他 window 中监测进行事件拦截，如果发现了，就触发事件
            if (GUILayout.Button("发送自定义事件"))
            {
                var testWindow = GetWindow<ForTestWindow>();
                // 增加了对null的检查
                if (testWindow != null)
                {
                    testWindow.SendEvent(_customEvent);
                }
                else
                {
                    Debug.LogError("无法获取 ForTestWindow 窗口实例。");
                }
            }

            // 只给别人发送事件,或者说只能给其他界面发送事件，在Odin中是这样设计的
            if (GUILayout.Button("给自己发送事件"))
            {
                var utility = CreateWindow<UnityEditorGUIUtility>();
                // 增加了对null的检查
                if (utility != null)
                {
                    utility.SendEvent(_customEvent);
                }
                else
                {
                    Debug.LogError("无法获取 UnityEditorGUIUtility 窗口实例。");
                }
            }

            // 第三步，由窗口检测，并接受事件。 
            if (Event.current.type == EventType.ExecuteCommand)
            {
                if (Event.current.commandName == "ZeusOdinCustomEvent")
                {
                    Debug.Log(Event.current.commandName);
                    Debug.Log("UnityEditorGUIUtility 收到自定义事件");
                }
            }

            // 坐标转换
            // 在 Odin 中的鼠标位置，是从开始 Odin 绘制的位置开始计算的，比如下面代码有 ImGUI，鼠标位置的Y轴的0点在ImGUI绘制结束点，X是窗口左侧
            _mousePosition = EditorGUILayout.Vector2Field("鼠标位置", Event.current.mousePosition);
            var rect = new Rect(10f, 10, 100, 100);
            var v = new Vector2(50, 50);
            EditorGUILayout.Vector2Field("测试位置: ", v);
            EditorGUILayout.Vector2Field("转换后位置: ", GUIUtility.ScreenToGUIPoint(v));
        }

        #endregion

        protected override void OnImGUI()
        {
            var v = new Vector2(50, 50);
            EditorGUILayout.Vector2Field("ImGUI 测试位置: ", v);
            EditorGUILayout.Vector2Field("ImGUI转换后位置: ", GUIUtility.ScreenToGUIPoint(v));
            EditorGUILayout.Space(10f);
            base.OnImGUI();
        }

        #region 4.鼠标指针

        [Title("鼠标指针")]
        [OnInspectorGUI]
        [InfoBox("绿色区域会改变鼠标样式")]
        void OnCustomGUI4()
        {
            const float floatNumber = 630f;
            EditorGUI.DrawRect(new Rect(300f, floatNumber, 100f, 50f), Color.green);
            EditorGUIUtility.AddCursorRect(new Rect(300f, floatNumber, 100f, 50f), MouseCursor.Zoom);
        }

        #endregion

        #region 5.绘制色板和曲线

        Color _color;
        AnimationCurve _curve = new();

        [Title("绘制色板和曲线")]
        [OnInspectorGUI]
        [InfoBox("cyan 青色色板，是用来吸取，或者参考的，通常是固定颜色的板，或者也可以使用一个颜色变量")]
        [InfoBox("Swatch 样板，色板，用来更好的查看，通常是配合使用")]
        void OnCustomGUI5()
        {
            const float floatNumber = 630f;
            _color = EditorGUILayout.ColorField(new GUIContent("颜色：", OdinEditorResources.OdinLogo), _color,
                true, true, true);
            EditorGUI.DrawRect(new Rect(10, floatNumber, 100, 50), _color);
            // cyan 青色色板
            EditorGUIUtility.DrawColorSwatch(new Rect(150f, floatNumber, 100, 50), Color.cyan);

            // 绘制曲线
            _curve = EditorGUILayout.CurveField(new GUIContent("曲线"), _curve);
            // 用于显示的曲线面板，通常是配合使用
            EditorGUIUtility.DrawCurveSwatch(new Rect(600f, floatNumber, 100, 50), _curve, null, Color.red,
                Color.green);
        }

        #endregion

        // 此处直接当成 OnGUI,但是不要删除 base.DrawEditors(); 这一部分实现 Odin 特性的绘制
        protected override void DrawEditors()
        {
            base.DrawEditors();
        }
    }
}