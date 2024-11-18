using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._6.UnityEditorGUIUtility.Editor
{
    public class ForTestWindow : OdinEditorWindow
    {
        public static void OpenWindow()
        {
            var win = GetWindow<ForTestWindow>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        // 此处直接当成 OnGUI,但是不要删除 base.DrawEditors(); 这一部分实现 Odin 特性的绘制
        protected override void DrawEditors()
        {
            base.DrawEditors();
            if (Event.current.type == EventType.ExecuteCommand)
            {
                if (Event.current.commandName == "ZeusOdinCustomEvent")
                {
                    Debug.Log("ForTestWindow 收到自定义事件");
                }
            }
        }
    }
}