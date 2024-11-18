using Odin_Toolkits.Editor_API_Usage_Guide.General.Editor;
using Sirenix.OdinInspector;

namespace Odin_Toolkits.Editor_API_Usage_Guide._12.UnityEditorApplication.Editor
{
    public class UnityEditorApplicationAPI : AbstractEditorTutorialWindow<UnityEditorApplicationAPI>
    {
        protected override string SetUsageTip()
        {
            return "案例介绍: Unity 内置 UnityEditorApplication API 使用示例";
        }

        [PropertyOrder(10)] [Title("UnityEditorApplication API 说明")] [DisplayAsString] [LabelText("五个编辑器事件")]
        public string[] options =
        {
            "EditorApplication.update",
            "EditorApplication.hierarchyChanged",
            "EditorApplication.projectChanged",
            "EditorApplication.playModeStateChanged",
            "EditorApplication.pauseStateChanged"
        };

        [PropertyOrder(10)] [DisplayAsString] [LabelText("编辑器生命周期相关")]
        public string[] options2 =
        {
            "EditorApplication.isPlaying",
            "EditorApplication.isPaused",
            "EditorApplication.isCompiling",
            "EditorApplication.isUpdating"
        };

        [PropertyOrder(10)] [DisplayAsString] [LabelText("Unity 相关路径")]
        public string[] options3 =
        {
            "EditorApplication.applicationPath : Unity 编辑器的安装路径",
            "EditorApplication.applicationContentsPath : Unity 安装目录 Data 路径"
        };

        [PropertyOrder(10)] [DisplayAsString] [LabelText("常用方法")]
        public string[] options4 =
        {
            "EditorApplication.Exit(0) : 直接退出 Unity 编辑器，关闭程序",
            "EditorApplication.ExitPlaymode() : 退出播放模式",
            "EditorApplication.EnterPlaymode() : 进入播放模式"
        };
    }
}