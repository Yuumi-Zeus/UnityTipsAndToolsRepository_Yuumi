using Odin_Toolkits.DevToolkit._1.TemplateCodeGen.Editor;
using Odin_Toolkits.DevToolkit._2.ExampleGen.Editor;
using Odin_Toolkits.DevToolkit._3.FindUsableAPI.Editor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

namespace Odin_Toolkits.DevToolkit.General.Editor
{
    public class DevToolkitMenuEditorWindow : OdinMenuEditorWindow
    {
        static void OpenWindow()
        {
            var window = GetWindow<DevToolkitMenuEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 650);
        }

        protected override void Initialize()
        {
            base.Initialize();
            TemplateCodeGen.Ins.OwnerWindow = this;
            ExampleGen.Ins.OwnerWindow = this;
            FindUsableAPITool.Ins.OwnerWindow = this;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TemplateCodeGen.Ins.OwnerWindow = null;
            ExampleGen.Ins.OwnerWindow = null;
            FindUsableAPITool.Ins.OwnerWindow = null;
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false)
            {
                { "模板代码生成器", TemplateCodeGen.Ins, SdfIconType.FileCodeFill },
                { "示例文件生成器", ExampleGen.Ins, SdfIconType.FolderFill },
                { "可用API查看工具", FindUsableAPITool.Ins, SdfIconType.Eyeglasses }
            };

            return tree;
        }
    }
}