using System;
using System.Collections.Generic;
using System.Linq;
using Odin_Toolkits.DevToolkit.General.Editor;
using Sirenix.OdinInspector;

namespace Odin_Toolkits.DevToolkit._3.FindUsableAPI.Editor
{
    public class FindUsableAPITool : AbstractDevTool<FindUsableAPITool>
    {
        #region 工具配置数据

        public override void ResetData()
        {
            selectedAssemblyConfig = string.Empty;
            SelectedTypeConfig = null;
        }

        protected override string SetUsageTip()
        {
            return "此工具用于快速查看开发者可以使用的API列表，包括public和protected的属性和事件以及public和protected的所有方法";
        }

        [PropertyOrder(5)]
        [TitleGroup("工具配置")]
        [ValueDropdown("GetAssemblies", SortDropdownItems = true)]
        [LabelText("选择程序集: ")]
        public string selectedAssemblyConfig;

        [PropertyOrder(5)]
        [TitleGroup("工具配置")]
        [ValueDropdown("GetAssembliesTypes", FlattenTreeView = false, SortDropdownItems = true)]
        [HideIf("AssemblyIsNull")]
        [LabelText("选择想要查看的类: ")]
        public Type SelectedTypeConfig;

        #endregion

        #region 预期结果

        [PropertyOrder(6)]
        [TitleGroup("预期结果")]
        [ShowInInspector]
        [EnableGUI]
        [LabelText("公共或者受保护的属性和事件方法，每页10个，右侧翻页")]
        [ListDrawerSettings(NumberOfItemsPerPage = 10)]
        public string[] GetSetOrEventMethods =>
            SelectedTypeConfig != null
                ? UsableAPIViewTool.FilterGetAndSetOrEventMethods(SelectedTypeConfig)
                : Array.Empty<string>();

        [PropertyOrder(7)]
        [TitleGroup("预期结果")]
        [ShowInInspector]
        [EnableGUI]
        [LabelText("公共和受保护的非静态方法，每页10个，右侧翻页")]
        [ListDrawerSettings(NumberOfItemsPerPage = 10)]
        public string[] PublicAndProtectedMethods =>
            SelectedTypeConfig != null
                ? UsableAPIViewTool.GetPublicAndProtectedMethods(SelectedTypeConfig)
                : Array.Empty<string>();

        [PropertyOrder(8)]
        [TitleGroup("预期结果")]
        [ShowInInspector]
        [EnableGUI]
        [LabelText("公共和受保护的静态方法，每页10个，右侧翻页")]
        [ListDrawerSettings(NumberOfItemsPerPage = 10)]
        public string[] PublicAndProtectedStaticMethods =>
            SelectedTypeConfig != null
                ? UsableAPIViewTool.GetPublicAndProtectedStaticMethods(SelectedTypeConfig)
                : Array.Empty<string>();

        #endregion

        #region ShowIf

        #endregion

        #region 工具执行

        IEnumerable<Type> GetAssembliesTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.GetName().Name == selectedAssemblyConfig)
                .SelectMany(assembly => assembly.GetTypes());
        }

        IEnumerable<string> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Select(assembly => assembly.GetName().Name);
        }

        bool AssemblyIsNull()
        {
            return selectedAssemblyConfig == string.Empty;
        }

        #endregion
    }
}