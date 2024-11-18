using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._8.UnityEventClass.Editor
{
    public class UnityGUIEvent : OdinEditorWindow
    {
        // Event公共类，它提供了许多属性和方法，允许你检查和处理用户输入
        // 主要用于在unity编辑器拓展开发中，
        // 因为input相关内容需要在运行时才能监听输入。 event专门提供给编辑模式下使用。
        // 可以帮助我们检测鼠标键盘输入等事件相关操作。 在on GUI和on scene view中都能使用。 
        [MenuItem(OdinToolkitsConfig.OdinEditorAPIUsageGuidePath + "/" + "UnityGUIEvent Tutorial")]
        static void OpenWindow()
        {
            var win = GetWindow<UnityGUIEvent>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        // 此处可以直接当成 OnGUI,但是不要删除 base.DrawEditors(); 这一部分实现 Odin 特性的绘制
        // 此时的鼠标位置是正确的，窗口左上角是原点
        protected override void DrawEditors()
        {
            base.DrawEditors();
            var @event = Event.current;
            EditorGUILayout.HelpBox(new GUIContent("Event.current 很多都需要使用组合判断，这样才能更加准确"));
            EditorGUILayout.LabelField(@event.alt ? "Alt 键按下" : "正在检测是否按下 Alt 键中...");
            EditorGUILayout.LabelField(@event.shift ? "Shift 键按下" : "正在检测是否按下 Shift 键中...");
            EditorGUILayout.LabelField(@event.control ? "Control 键按下" : "正在检测是否按下 Control 键中...");

            EditorGUILayout.BeginHorizontal(SirenixGUIStyles.BoxContainer);
            {
                bool a = @event.isMouse && @event.OnMouseDown(0);
                if (a) Debug.Log("鼠标事件，按下鼠标左键");

                EditorGUILayout.LabelField(a ? "正处于鼠标事件，按下鼠标左键" : "正在检测 isMouse 是否为真...");
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Vector2Field("鼠标位置: ", @event.mousePosition);
            EditorGUILayout.BeginHorizontal(SirenixGUIStyles.BoxContainer);
            {
                bool b = @event.isKey && @event.OnKeyDown(KeyCode.A);
                if (b)
                {
                    Debug.Log("character: " + @event.character);
                    Debug.Log("A键输入");
                }

                EditorGUILayout.LabelField(b ? "正处于键盘事件，KeyCode: " + @event.keyCode : "正在检测 isKey 是否为真...");
            }
            EditorGUILayout.EndHorizontal();
        }
        //11.判断输入类型
        //  Event.current.type
        //  EventType枚举和它比较即可
        //  EventType中有常用的 鼠标按下抬起拖拽，键盘按下抬起等等类型
        //  一般会配合它 来判断 比如 键盘 鼠标的抬起按下相关的操作

        //12.是否锁定大写 对应键盘上caps键是否开启
        //  Event.current.capsLock
        // if (eve.capsLock)
        //     Debug.Log("大小写锁定开启");
        //     else
        // Debug.Log("大小写锁定关闭");

        //13.Windows键或Command键是否按下
        //  Event.current.command
        // if (eve.command)
        //     Debug.Log("PC win键按下 或 Mac Command键按下");

        //14.键盘事件 字符串
        //  Event.current.commandName
        //  可以用来判断是否触发了对应的键盘事件
        //  返回值：
        //  Copy:拷贝
        //  Paste:粘贴
        //  Cut:剪切
        //     if(eve.commandName == "Copy")
        // {
        //     Debug.Log("按下了ctrl + c");
        // }
        // if (eve.commandName == "Paste")
        // {
        //     Debug.Log("按下了ctrl + v");
        // }
        // if (eve.commandName == "Cut")
        // {
        //     Debug.Log("按下了ctrl + x");
        // }

        //15.鼠标间隔移动距离
        //  Event.current.delta

        //Debug.Log(eve.delta);

        //16.是否是功能键输入
        //  Event.current.functionKey
        //  功能键指小键盘中的 方向键, page up, page down, backspace等等
        // if (eve.functionKey)
        //     Debug.Log("有功能按键输入");
        //
        //     //17.小键盘是否开启
        //     //  Event.current.numeric
        //     if(eve.numeric)
        //     Debug.Log("小键盘是否开启");

        //18.避免组合键冲突
        //  Event.current.Use()
        //  在处理完对应输入事件后，调用该方法，可以阻止事件继续派发，放置和Unity其他编辑器事件逻辑冲突
        //  eve.Use();
    }
}