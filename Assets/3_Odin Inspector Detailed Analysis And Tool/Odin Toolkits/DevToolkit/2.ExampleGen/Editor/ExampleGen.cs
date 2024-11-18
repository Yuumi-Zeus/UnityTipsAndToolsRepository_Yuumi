using System;
using System.IO;
using System.Linq;
using Odin_Toolkits.Common_Utilities;
using Odin_Toolkits.DevToolkit._1.TemplateCodeGen.Editor;
using Odin_Toolkits.DevToolkit.General.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Odin_Toolkits.DevToolkit._2.ExampleGen.Editor
{
    public class ExampleGen : AbstractDevTool<ExampleGen>
    {
        #region 工具配置数据

        public override void ResetData()
        {
            _lastScriptName = string.Empty;
            parentFolderPathConfig = string.Empty;
            exampleFolderNameConfig = string.Empty;
            scriptNamespaceConfig = string.Empty;
            scriptNameConfig = string.Empty;
            sceneNameConfig = string.Empty;
        }

        protected override string SetUsageTip()
        {
            return "此工具用于快速生成一个示例文件夹，其中包含一个场景和一个示例脚本文件，提高开发以及测试效率。路径可以拖拽，自动匹配";
        }

        // 特殊数据存储
        string _lastScriptName;

        [PropertyOrder(10)] [TitleGroup("工具使用配置")] [FolderPath] [LabelText("父文件夹路径: ")] [LabelWidth(120f)]
        public string parentFolderPathConfig;

        [PropertyOrder(11)] [TitleGroup("工具使用配置")] [LabelText("示例文件夹名称: ")] [LabelWidth(120f)]
        public string exampleFolderNameConfig;

        [PropertyOrder(12)] [TitleGroup("工具使用配置")] [LabelText("脚本文件命名空间: ")] [LabelWidth(120f)]
        public string scriptNamespaceConfig;

        [PropertyOrder(15)]
        [TitleGroup("工具使用配置")]
        [LabelText("脚本文件名称: ")]
        [LabelWidth(120f)]
        [InlineButton("FromSceneToScript", "覆盖场景名")]
        public string scriptNameConfig;

        [PropertyOrder(16)]
        [TitleGroup("工具使用配置")]
        [LabelText("场景名称: ")]
        [LabelWidth(120f)]
        [InlineButton("FromFolderToScene", "覆盖文件夹名称")]
        public string sceneNameConfig;

        void FromSceneToScript()
        {
            scriptNameConfig = sceneNameConfig;
        }

        void FromFolderToScene()
        {
            sceneNameConfig = exampleFolderNameConfig;
        }

        #endregion

        #region 预期生成结果

        [PropertyOrder(20)]
        [TitleGroup("预期生成结果")]
        [LabelText("示例文件夹路径: ")]
        [LabelWidth(120f)]
        [ShowInInspector]
        [EnableGUI]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        public string ResultExampleFolderPath
        {
            get
            {
                if (!string.IsNullOrEmpty(parentFolderPathConfig) && !string.IsNullOrEmpty(exampleFolderNameConfig))
                    return ProjectUtils.CombinePath(parentFolderPathConfig, exampleFolderNameConfig);
                return string.Empty;
            }
        }

        [PropertyOrder(20)]
        [TitleGroup("预期生成结果")]
        [LabelText("脚本文件路径: ")]
        [LabelWidth(120f)]
        [ShowInInspector]
        [EnableGUI]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        public string ResultScriptsPathNotExtension
        {
            get
            {
                if (!string.IsNullOrEmpty(ResultExampleFolderPath) &&
                    !string.IsNullOrEmpty(ResultScriptNameNotExtension))
                    return Path.Combine(ResultExampleFolderPath, ResultScriptNameNotExtension);
                return string.Empty;
            }
        }

        [PropertyOrder(20)]
        [TitleGroup("预期生成结果")]
        [LabelText("场景路径: ")]
        [LabelWidth(120f)]
        [ShowInInspector]
        [EnableGUI]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        public string ResultScenePathNotExtension
        {
            get
            {
                if (!string.IsNullOrEmpty(ResultExampleFolderPath) &&
                    !string.IsNullOrEmpty(ResultSceneNameNotExtension))
                    return Path.Combine(ResultExampleFolderPath, ResultSceneNameNotExtension);

                return string.Empty;
            }
        }

        [PropertyOrder(20)]
        [TitleGroup("预期生成结果")]
        [LabelText("脚本命名空间: ")]
        [LabelWidth(120f)]
        [ShowInInspector]
        [EnableGUI]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        public string ResultExampleNameSpace =>
            string.IsNullOrEmpty(scriptNamespaceConfig)
                ? DefaultNamespace
                : ProjectUtils.RegexExtension.CanonicalNamespace(scriptNamespaceConfig);

        [PropertyOrder(20)]
        [TitleGroup("预期生成结果")]
        [LabelText("脚本文件名称: ")]
        [LabelWidth(120f)]
        [ShowInInspector]
        [EnableGUI]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        public string ResultScriptNameNotExtension =>
            string.IsNullOrEmpty(scriptNameConfig)
                ? string.Empty
                : ProjectUtils.RegexExtension.CanonicalScriptClassName(scriptNameConfig);

        [PropertyOrder(20)]
        [TitleGroup("预期生成结果")]
        [LabelText("场景名称: ")]
        [LabelWidth(120f)]
        [ShowInInspector]
        [EnableGUI]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        public string ResultSceneNameNotExtension =>
            string.IsNullOrEmpty(sceneNameConfig)
                ? string.Empty
                : ProjectUtils.RegexExtension.CanonicalScriptClassName(sceneNameConfig);

        #endregion

        #region 工具执行

        [PropertyOrder(25)]
        [PropertySpace]
        [Button("生成示例", ButtonSizes.Large)]
        public void GenerateExample()
        {
            if (!ValidateExecute()) return;
            // 创建文件夹
            ProjectUtils.TryCreateFolder(ResultExampleFolderPath);
            // 保存当前场景，生成一个新的场景，保存，打开新场景，生成一个物体，名称为: 测试启动物体，保存当前场景文件
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), ResultScenePathNotExtension + ".unity");
            EditorSceneManager.OpenScene(ResultScenePathNotExtension + ".unity");
            // 在新场景中创建物体
            var gameObject = new GameObject("测试启动物体");
            Selection.activeGameObject = gameObject;
            // 保存新场景（已修改）
            EditorSceneManager.SaveOpenScenes();
            // 创建脚本文件
            TemplateCodeGen.GenerateCodeFromTemplate(ResultScriptsPathNotExtension + ".cs",
                ReadTemplateFile(ExampleCodeTemplatePath), ResultExampleNameSpace, ResultScriptNameNotExtension);
            OwnerNormalToast($"示例文件夹 {exampleFolderNameConfig} 以及相关文件创建成功");
        }

        bool ValidateExecute()
        {
            if (string.IsNullOrEmpty(ResultExampleFolderPath))
            {
                OwnerWindow.ErrorToast("文件夹路径不能为空");
                return false;
            }

            if (string.IsNullOrEmpty(ResultScenePathNotExtension) || string.IsNullOrEmpty(ResultScriptNameNotExtension))
            {
                OwnerWindow.ErrorToast("场景名称和脚本文件名称不能为空");
                return false;
            }

            if (File.Exists(ResultScenePathNotExtension + ".unity"))
            {
                OwnerWindow.ErrorToast("相同路径下已经存在同名场景，停止生成操作");
                return false;
            }

            _lastScriptName = ResultExampleNameSpace + "." + ResultScriptNameNotExtension;
            var targetTypes = AppDomain.CurrentDomain.GetAssemblies()
                .First(assembly => assembly.GetName().Name == "Assembly-CSharp")
                .GetTypes().Where(type => type.FullName == _lastScriptName).ToArray();
            if (targetTypes.Length == 1) return true;
            OwnerWindow.ErrorToast("该命名空间下已经存在相同名称的脚本文件");
            _lastScriptName = string.Empty;
            return false;
        }

        string ReadTemplateFile(string templateFilePath)
        {
            if (string.IsNullOrEmpty(templateFilePath))
            {
                OwnerWindow.ErrorToast("无法找到模板文件，请勿修改模板文件名称");
                return string.Empty;
            }

            var reader = new StreamReader(templateFilePath);
            string text = reader.ReadToEnd();
            reader.Close();
            return text;
        }

        // [Button("Debug", ButtonSizes.Large)]
        void ZeusDebug()
        {
            var architectureTypes = AppDomain.CurrentDomain.GetAssemblies();
            Debug.Log(architectureTypes.Length);
            var first = architectureTypes.First(assembly => assembly.GetName().Name == "Assembly-CSharp");
            Debug.Log(first);
            Debug.Log(first.FullName);
            var types = first.GetTypes();
            Debug.Log(types.Length);
            var type = types.First(type => type.Name == "Test");
            Debug.Log(type);
        }

        [DidReloadScripts]
        public static void AttachCompiledScriptToGameObject()
        {
            string attachInfo = Ins._lastScriptName;
            Ins._lastScriptName = string.Empty;
            if (string.IsNullOrEmpty(attachInfo)) return;
            var targetType = AppDomain.CurrentDomain.GetAssemblies()
                .First(assembly => assembly.GetName().Name == "Assembly-CSharp")
                .GetTypes()
                .First(type => type.FullName == attachInfo);
            var obj = GameObject.Find("/测试启动物体");
            obj?.AddComponent(targetType);
            Debug.Log("成功找到示例文件脚本并挂载到测试启动物体: " + attachInfo);
            var scene = SceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        #endregion

        #region 脚本模板路径

        const string ConstExampleCodeTemplatePath =
            "Assets/OdinExtension/DevToolkit/2.ExampleGen/Editor/Template/ExampleCodeTemplate.txt";

        static string ExampleCodeTemplatePath =>
            File.Exists(ProjectUtils.GetAbsolutePath(ConstExampleCodeTemplatePath))
                ? ProjectUtils.GetAbsolutePath(ConstExampleCodeTemplatePath)
                : ProjectUtils.FindAssetPath("ExampleCodeTemplate");

        #endregion
    }
}