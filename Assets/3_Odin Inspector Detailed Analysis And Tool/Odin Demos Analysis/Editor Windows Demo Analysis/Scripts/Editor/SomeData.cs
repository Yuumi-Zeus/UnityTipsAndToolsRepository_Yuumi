using System;
using Sirenix.OdinInspector;
using UnityEditor;

#if UNITY_EDITOR
namespace Odin_Demos_Analysis.Editor_Windows_Demo_Analysis.Scripts.Editor
{
    [HideLabel]
    [Serializable]
    public class SomeData
    {
        [MultiLineProperty(3)]
        [Title("Basic Odin Menu Editor Window", "Inherit from OdinMenuEditorWindow, and build your menu tree")]
        public string test1 =
            "This value is persistent cross reloads, but will reset once you restart Unity or close the window.";
        // 这个值在重载时持久，一旦重启 Unity 或关闭窗口就会重置。

        [MultiLineProperty(3)] [ShowInInspector] [NonSerialized]
        public string Test2 =
            "This value is not persistent cross reloads, and will reset once you hit play or recompile.";
        // 这个值在重载时不持久，一旦点击播放或重新编译就会重置。

        [MultiLineProperty(3)]
        [ShowInInspector]
        string Test3
        {
            get =>
                EditorPrefs.GetString("OdinDemo.PersistentString",
                    "This value is persistent forever, even cross Unity projects. But it's not saved together " +
                    "with your project. That's where ScriptableObjects and OdinEditorWindows come in handy.");
            set => EditorPrefs.SetString("OdinDemo.PersistentString", value);
        }
        // 这个值是永久存在的，即使是跨 Unity 项目。但它并没有和你的项目一起保存。
        // 这就是 ScriptableObjects 和 OdinEditorWindows 派上用场的地方。
    }
}
#endif