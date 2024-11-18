using System.IO;
using Odin_Toolkits.Editor_API_Usage_Guide.General.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._9.UnityEditorUtility.Editor
{
    public class UnityEditorUtilityAPI : AbstractEditorTutorialWindow<UnityEditorUtilityAPI>
    {
        protected override string SetUsageTip()
        {
            return "案例介绍: Unity 内置 EditorUtility API 使用示例";
        }

        [PropertyOrder(10)]
        [TitleGroup("EditorUtility API 按钮测试")]
        [BoxGroup("EditorUtility API 按钮测试/对话框测试")]
        [Button("测试 EditorUtility.DisplayDialog ", ButtonSizes.Medium)]
        void Display()
        {
            Debug.Log(EditorUtility.DisplayDialog("测试对话框", "这是一个测试对话框", "确定", "取消") ? "点击了确定按钮" : "点击了取消按钮");

            Debug.Log("阻塞逻辑，点击按钮后才会继续执行这行代码");
        }

        [PropertyOrder(10)]
        [TitleGroup("EditorUtility API 按钮测试")]
        [BoxGroup("EditorUtility API 按钮测试/对话框测试")]
        [Button("测试 EditorUtility.DisplayDialogComplex ", ButtonSizes.Medium)]
        void Display2()
        {
            Debug.Log(EditorUtility.DisplayDialogComplex("测试三键对话框", "这是一个测试三键对话框", "确定", "取消", "忽略") switch
            {
                0 => "点击了确定按钮",
                1 => "点击了取消按钮",
                2 => "点击了忽略按钮",
                _ => "点击了未知按钮"
            });

            Debug.Log("阻塞逻辑，点击按钮后才会继续执行这行代码");
        }

        [PropertyOrder(10)]
        [TitleGroup("EditorUtility API 按钮测试")]
        [BoxGroup("EditorUtility API 按钮测试/进度条测试")]
        [Button("测试 EditorUtility.DisplayProgressBar 和 ClearProgressBar ", ButtonSizes.Medium)]
        void Display3()
        {
            for (var i = 0; i < 100; i++)
            {
                EditorUtility.DisplayProgressBar("测试进度条", "这是一个测试进度条", i / 100f);
                System.Threading.Thread.Sleep(100);
            }

            EditorUtility.ClearProgressBar();
        }

        [PropertyOrder(10)]
        [TitleGroup("EditorUtility API 按钮测试")]
        [BoxGroup("EditorUtility API 按钮测试/文件管理")]
        [Button("测试 EditorUtility.SaveFilePanel，测试默认 .txt 文件", ButtonSizes.Medium)]
        void Display4()
        {
            // returns the selected path name.
            string str = EditorUtility.SaveFilePanel("测试保存文件面板", Application.dataPath, "Test", "txt");
            Debug.Log("方法返回的路径: " + str);
            string defaultAbsolutePath = Application.dataPath + "/Test.txt";
            Debug.Log("默认路径: " + defaultAbsolutePath);
            File.WriteAllText(string.IsNullOrEmpty(str) ? defaultAbsolutePath : str, "这是一个测试文件");
        }

        [PropertyOrder(10)]
        [TitleGroup("EditorUtility API 按钮测试")]
        [BoxGroup("EditorUtility API 按钮测试/文件管理")]
        [Button("测试 EditorUtility.SaveFilePanelInProject，仅限工程文件夹下，测试默认 .txt 文件", ButtonSizes.Medium)]
        void Display5()
        {
            string str = EditorUtility.SaveFilePanelInProject("测试保存项目内文件", "TestInProject", "txt", "自定义Msg");
            Debug.Log("方法返回的路径: " + str);
            string defaultAbsolutePath = Application.dataPath + "/TestInProject.txt";
            Debug.Log("默认路径: " + defaultAbsolutePath);
            File.WriteAllText(string.IsNullOrEmpty(str) ? defaultAbsolutePath : str, "这是一个测试文件");
        }

        [PropertyOrder(10)]
        [TitleGroup("EditorUtility API 按钮测试")]
        [BoxGroup("EditorUtility API 按钮测试/文件管理")]
        [Button("测试 EditorUtility.SaveFolderPanel 选择文件夹", ButtonSizes.Medium)]
        void Display6()
        {
            string str = EditorUtility.SaveFolderPanel("测试保存文件夹面板", Application.dataPath, "TestFolder");
            Debug.Log("方法返回的文件夹路径: " + str + ",可以用于选择文件夹路径");
        }

        [PropertyOrder(10)]
        [TitleGroup("EditorUtility API 按钮测试")]
        [BoxGroup("EditorUtility API 按钮测试/文件管理")]
        [Button("测试 EditorUtility.OpenFilePanel，打开文件面板，获取文件路径", ButtonSizes.Medium)]
        void Display7()
        {
            string str = EditorUtility.OpenFilePanel("打开文件面板", Application.dataPath, "txt");
            Debug.Log("方法返回的文件路径: " + str);
            if (str == null) return;
            string txt = File.ReadAllText(str);
            Debug.Log("读取文件内容: " + txt);
        }

        [PropertyOrder(10)]
        [TitleGroup("EditorUtility API 按钮测试")]
        [BoxGroup("EditorUtility API 按钮测试/文件管理")]
        [Button("测试 EditorUtility.OpenFolderPanel，打开文件夹面板，获取文件夹路径", ButtonSizes.Medium)]
        void Display8()
        {
            string str = EditorUtility.OpenFolderPanel("打开文件夹面板", Application.dataPath, "TestFolder");
            Debug.Log("方法返回的路径: " + str);
        }

        [PropertyOrder(15)] [TitleGroup("EditorUtility API 获取依赖项")] [LabelText("选择的 GameObject")]
        public GameObject myGameObject;

        [PropertyOrder(15)]
        [TitleGroup("EditorUtility API 获取依赖项")]
        [Button("测试 EditorUtility.OpenFolderPanel，打开文件夹面板，获取文件夹路径", ButtonSizes.Medium)]
        void Display9()
        {
            var objects = EditorUtility.CollectDependencies(new Object[] { myGameObject });
            foreach (var item in objects)
            {
                Debug.Log(item.name);
            }
        }
    }
}