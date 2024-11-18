using Odin_Toolkits.Editor_API_Usage_Guide.General.Editor;
using Sirenix.OdinInspector;

namespace Odin_Toolkits.Editor_API_Usage_Guide._13.UnityCompilationPipeline.Editor
{
    public class UnityCompilationPipelineAPI : AbstractEditorTutorialWindow<UnityCompilationPipelineAPI>
    {
        protected override string SetUsageTip()
        {
            return "案例介绍: Unity 内置 CompilationPipeline API 使用示例";
        }

        [PropertyOrder(10)] [Title("CompilationPipeline API 说明")] [DisplayAsString] [LabelText("编译结束的回调")]
        public string[] options =
        {
            "CompilationPipeline.assemblyCompilationFinished : 某个程序集编译结束的回调，参数为程序集名称和错误或警告信息，可以输出相关信息",
            "CompilationPipeline.compilationFinished : 所有程序集编译结束的回调，参数为 ActiveBuildStatus，可以输出相关信息"
        };
    }
}