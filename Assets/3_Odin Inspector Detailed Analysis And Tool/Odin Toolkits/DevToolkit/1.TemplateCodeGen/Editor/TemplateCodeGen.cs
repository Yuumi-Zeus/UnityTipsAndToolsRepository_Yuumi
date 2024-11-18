using System;
using System.IO;
using Odin_Toolkits.Common_Utilities;
using Odin_Toolkits.DevToolkit.General.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.DevToolkit._1.TemplateCodeGen.Editor
{
    public class TemplateCodeGen : AbstractDevTool<TemplateCodeGen>
    {
        #region 工具配置数据

        protected override string SetUsageTip()
        {
            return "此工具用于一键生成设定好的模板代码，可以直接拖入文件夹，自动匹配路径";
        }

        public override void ResetData()
        {
            folderPathConfig = string.Empty;
            scriptNamespaceConfig = string.Empty;
            scriptNameConfig = string.Empty;
            menuItemPathConfig = string.Empty;
            menuItemPriorityConfig = 0;
        }

        [PropertyOrder(0)] [TitleGroup("工具使用配置")] [EnumPaging] [LabelText("选择生成的脚本模板类型: ")]
        public TemplateScriptType templateType;

        [PropertyOrder(5)] [TitleGroup("工具使用配置")] [LabelText("脚本所在文件夹: ")] [LabelWidth(150f)] [FolderPath]
        public string folderPathConfig;

        [PropertyOrder(10)] [TitleGroup("工具使用配置")] [LabelText("脚本文件命名空间: ")] [LabelWidth(150f)]
        public string scriptNamespaceConfig;

        [PropertyOrder(15)]
        [TitleGroup("工具使用配置")]
        [LabelText("脚本文件名称以及类名: ")]
        [LabelWidth(150f)]
        [SuffixLabel("脚本文件名称仅保留英文字符和汉字", true)]
        public string scriptNameConfig;

        [PropertyOrder(20)]
        [TitleGroup("工具使用配置")]
        [LabelText("MenuItem 菜单路径: ")]
        [LabelWidth(150f)]
        [ShowIf("ShowIfMenuConfig")]
        public string menuItemPathConfig;

        [PropertyOrder(25)]
        [TitleGroup("工具使用配置")]
        [LabelText("Priority 菜单优先级: ")]
        [LabelWidth(150f)]
        [ShowIf("ShowIfMenuConfig")]
        public int menuItemPriorityConfig;

        #endregion

        #region 预期生成结果

        [PropertyOrder(20)]
        [TitleGroup("预期生成结果")]
        [LabelText("脚本命名空间: ")]
        [LabelWidth(150f)]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        [EnableGUI]
        [ShowInInspector]
        public string ResultScriptNamespace =>
            string.IsNullOrEmpty(scriptNamespaceConfig)
                ? DefaultNamespace
                : ProjectUtils.RegexExtension.CanonicalNamespace(scriptNamespaceConfig);

        [PropertyOrder(25)]
        [TitleGroup("预期生成结果")]
        [LabelText("脚本文件路径: ")]
        [LabelWidth(150f)]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        [EnableGUI]
        [ShowInInspector]
        public string ResultScriptPath
        {
            get
            {
                if (string.IsNullOrEmpty(folderPathConfig) || string.IsNullOrEmpty(ResultScriptName))
                    return string.Empty;
                return PathUtilities.Combine(folderPathConfig, ResultScriptName);
            }
        }

        [PropertyOrder(30)]
        [TitleGroup("预期生成结果")]
        [LabelText("脚本类名: ")]
        [LabelWidth(150f)]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        [EnableGUI]
        [ShowInInspector]
        public string ResultScriptName => ProjectUtils.RegexExtension.CanonicalScriptClassName(scriptNameConfig);

        [PropertyOrder(30)]
        [TitleGroup("预期生成结果")]
        [LabelText("MenuItem 菜单路径: ")]
        [LabelWidth(150f)]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        [EnableGUI]
        [ShowInInspector]
        [ShowIf("ShowIfMenuConfig")]
        public string ResultMenuItemPath => menuItemPathConfig;

        [PropertyOrder(30)]
        [TitleGroup("预期生成结果")]
        [LabelText("Priority 菜单优先级: ")]
        [LabelWidth(150f)]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        [EnableGUI]
        [ShowInInspector]
        [ShowIf("ShowIfMenuConfig")]
        public int ResultMenuItemPriority => menuItemPriorityConfig;

        #endregion

        #region ShowIf

        bool ShowIfMenuConfig()
        {
            return templateType switch
            {
                TemplateScriptType.OdinEditorWindow => true,
                TemplateScriptType.OdinMenuEditorWindow => true,
                TemplateScriptType.OdinEditor => false,
                TemplateScriptType.DevToolkit => false,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion

        #region Button

        [PropertyOrder(100)]
        [TitleGroup("工具执行")]
        [Button("生成脚本", ButtonSizes.Large)]
        public void GenCode()
        {
            if (ResultScriptPath == string.Empty)
            {
                OwnerErrorToast("请先配置好路径和脚本名称");
                return;
            }

            if (ShowIfMenuConfig() && ResultMenuItemPath == string.Empty)
            {
                OwnerErrorToast("请先配置好 MenuItem 菜单路径");
                return;
            }

            switch (templateType)
            {
                case TemplateScriptType.OdinEditor:
                    GenerateCodeFromTemplate(ResultScriptPath + ".cs", ReadTemplateFile(OdinEditorTemplatePath),
                        ResultScriptNamespace, ResultScriptName, ResultMenuItemPath, ResultMenuItemPriority);
                    OwnerNormalToast("生成 OdinEditor 脚本时报错是正常的，需要补充自定义绘制目标类型");
                    break;
                case TemplateScriptType.OdinEditorWindow:
                    GenerateCodeFromTemplate(ResultScriptPath + ".cs", ReadTemplateFile(OdinEditorWindowTemplatePath),
                        ResultScriptNamespace, ResultScriptName, ResultMenuItemPath, ResultMenuItemPriority);
                    break;
                case TemplateScriptType.OdinMenuEditorWindow:
                    GenerateCodeFromTemplate(ResultScriptPath + ".cs",
                        ReadTemplateFile(OdinMenuEditorWindowTemplatePath), ResultScriptNamespace, ResultScriptName,
                        ResultMenuItemPath, ResultMenuItemPriority);
                    break;
                case TemplateScriptType.DevToolkit:
                    GenerateCodeFromTemplate(ResultScriptPath + ".cs", ReadTemplateFile(DevToolTemplatePath),
                        ResultScriptNamespace, ResultScriptName, ResultMenuItemPath, ResultMenuItemPriority);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OwnerNormalToast("成功生成脚本");
        }

        string ReadTemplateFile(string templateFilePath)
        {
            if (string.IsNullOrEmpty(templateFilePath))
            {
                OwnerErrorToast("无法找到模板文件，是否修改了模板文件名称");
                return string.Empty;
            }

            var reader = new StreamReader(templateFilePath);
            string text = reader.ReadToEnd();
            reader.Close();
            return text;
        }

        static void GenerateCodeFromTemplate(string scriptPathName, string text,
            string namespaceName, string className, string menuItemPath, int priority)
        {
            if (string.IsNullOrEmpty(text)) return;
            text = text.Replace("#NAMESPACE#", namespaceName);
            text = text.Replace("#CLASSNAME#", className);
            text = text.Replace("#MENUITEMPATH#", menuItemPath);
            text = text.Replace("#PRIORITY#", priority.ToString());
            string fullPath = Path.GetFullPath(scriptPathName);
            var writer = new StreamWriter(fullPath, false, System.Text.Encoding.UTF8);
            writer.Write(text);
            writer.Close();
            AssetDatabase.ImportAsset(scriptPathName);
            AssetDatabase.Refresh();
        }

        #endregion

        #region 脚本模板路径

        const string ConstOdinEditorTemplatePath =
            "Assets/OdinExtension/DevToolkit/1.TemplateCodeGen/Editor/Templates/OdinEditorTemplate.txt";

        const string ConstOdinEditorWindowTemplatePath =
            "Assets/OdinExtension/DevToolkit/1.TemplateCodeGen/Editor/Templates/OdinEditorWindowTemplate.txt";

        const string ConstOdinMenuEditorWindowTemplatePath =
            "Assets/OdinExtension/DevToolkit/1.TemplateCodeGen/Editor/Templates/OdinMenuEditorWindowTemplate.txt";

        const string ConstDevToolTemplatePath =
            "Assets/OdinExtension/DevToolkit/1.TemplateCodeGen/Editor/Templates/DevToolTemplate.txt";

        static string OdinEditorTemplatePath =>
            File.Exists(ProjectUtils.GetAbsolutePath(ConstOdinEditorTemplatePath))
                ? ProjectUtils.GetAbsolutePath(ConstOdinEditorTemplatePath)
                : ProjectUtils.FindAssetPath("OdinEditorTemplate");

        static string OdinEditorWindowTemplatePath =>
            File.Exists(ProjectUtils.GetAbsolutePath(ConstOdinEditorWindowTemplatePath))
                ? ProjectUtils.GetAbsolutePath(ConstOdinEditorWindowTemplatePath)
                : ProjectUtils.FindAssetPath("OdinEditorWindowTemplate");

        static string OdinMenuEditorWindowTemplatePath =>
            File.Exists(ProjectUtils.GetAbsolutePath(ConstOdinMenuEditorWindowTemplatePath))
                ? ProjectUtils.GetAbsolutePath(ConstOdinMenuEditorWindowTemplatePath)
                : ProjectUtils.FindAssetPath("OdinMenuEditorWindowTemplate");

        static string DevToolTemplatePath => File.Exists(ProjectUtils.GetAbsolutePath(ConstDevToolTemplatePath))
            ? ProjectUtils.GetAbsolutePath(ConstDevToolTemplatePath)
            : ProjectUtils.FindAssetPath("DevToolTemplate");

        #endregion

        #region 公开静态方法，方便其他工具调用

        public static void GenerateCodeFromTemplate(string scriptPathName, string text,
            string namespaceName, string className)
        {
            GenerateCodeFromTemplate(scriptPathName, text, namespaceName, className, "", 0);
        }

        #endregion

        #region 过时方法存档

        [Obsolete("请使用 txt 模板文件进行生成")]
        void GenerateOdinEditorWindow(string scriptPathNoExtension)
        {
            string path = scriptPathNoExtension + ".cs";
            // 创建脚本文件对象
            var script = new FileInfo(path);
            // 使用using语句打开写入流
            using var writer = script.CreateText();
            // 写入原始脚本内容
            writer.WriteLine("using Sirenix.OdinInspector.Editor;");
            writer.WriteLine("using Sirenix.Utilities;");
            writer.WriteLine("using Sirenix.Utilities.Editor;");
            writer.WriteLine("using UnityEditor;");
            writer.WriteLine();
            writer.WriteLine($"namespace {ResultScriptNamespace}");
            writer.WriteLine("{");
            writer.WriteLine($"    public class {ResultScriptName} : OdinEditorWindow");
            writer.WriteLine("    {");
            writer.WriteLine($"        [MenuItem(\"{ResultMenuItemPath}\", priority = {ResultMenuItemPriority})]");
            writer.WriteLine("        static void ShowWindow()");
            writer.WriteLine("        {");
            writer.WriteLine($"            var win = GetWindow<{ResultScriptName}>();");
            writer.WriteLine("            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);");
            writer.WriteLine("            win.Show();");
            writer.WriteLine("        }");
            writer.WriteLine();
            writer.WriteLine("        // 此处可以直接当成 OnGUI,但是不要删除 base.DrawEditors(); 这一部分实现 Odin 特性的绘制");
            writer.WriteLine("        protected override void DrawEditors()");
            writer.WriteLine("        {");
            writer.WriteLine("            base.DrawEditors();");
            writer.WriteLine("        }");
            writer.WriteLine("    }");
            writer.WriteLine("}");
            writer.Flush();
            AssetDatabase.ImportAsset(path);
            AssetDatabase.Refresh();
        }

        #endregion
    }
}