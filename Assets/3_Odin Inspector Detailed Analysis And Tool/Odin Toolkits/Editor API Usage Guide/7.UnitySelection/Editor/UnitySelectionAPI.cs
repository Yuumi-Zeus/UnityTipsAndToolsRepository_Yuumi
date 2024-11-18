using System.Text;
using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Odin_Toolkits.Editor_API_Usage_Guide._7.UnitySelection.Editor
{
    public class UnitySelectionAPI : OdinEditorWindow
    {
        readonly StringBuilder _sb = new StringBuilder("没有选择");

        [MenuItem(OdinToolkitsConfig.OdinEditorAPIUsageGuidePath + "/" + "UnitySelection API")]
        static void OpenWindow()
        {
            var win = GetWindow<UnitySelectionAPI>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        #region 1.输出选择Object的名称

        [Title("输出选择物体的名称"), OnInspectorGUI]
        void GUISelection()
        {
            if (GUILayout.Button("获取当前选择的Object的名称"))
            {
                if (Selection.activeObject != null)
                {
                    _sb.Clear();
                    _sb.Append(Selection.activeObject.name);
                    if (Selection.activeObject is GameObject)
                        Debug.Log("当前选择的是GameObject");
                    else if (Selection.activeObject is Texture)
                        Debug.Log("当前选择的是Texture");
                    else if (Selection.activeObject is TextAsset)
                        Debug.Log("当前选择的是TextAsset");
                    else
                        Debug.Log("当前选择的是其他类型");
                }
                else
                {
                    _sb.Clear();
                    _sb.Append("没有选择");
                }
            }

            EditorGUILayout.LabelField(new GUIContent("当前选择的Object的名称"), new GUIContent(_sb.ToString()));
        }

        #endregion

        #region 5.选择所有 GameObject 的名称

        [Title("其他类似方法"), InfoBox("还可以选择多个 GameObject 和多个 Transform"), OnInspectorGUI]
        void GUISelection5() { }

        #endregion

        #region 7.获取并筛选其他对象

        [Title("Selection 常用静态方法 - 2"), InfoBox("可以通过一个对象，去获取并筛选其他对象的方法"), OnInspectorGUI]
        void GUISelection7()
        {
            if (GUILayout.Button("获取并筛选其他对象，可以获取文件夹，它也是一个 Object"))
            {
                var objects = Selection.GetFiltered<Object>(SelectionMode.DeepAssets | SelectionMode.Assets);
                foreach (var obj in objects) Debug.Log(obj.name);
            }
        }

        #endregion

        // 此处直接当成 OnGUI,但是不要删除 base.DrawEditors(); 这一部分实现 Odin 特性的绘制
        protected override void DrawEditors()
        {
            base.DrawEditors();
        }

        #region 2.选择GameObject

        string _selectionText = "";

        [Title("输出选择的GameObject的名称"), OnInspectorGUI]
        void GUISelection2()
        {
            if (GUILayout.Button("输出选择的 GameObject 的名称"))
                _selectionText = Selection.activeGameObject != null ? Selection.activeGameObject.name : "没有选择";

            EditorGUILayout.LabelField(new GUIContent("当前选择的 GameObject 的名称"), new GUIContent(_selectionText));
        }

        #endregion

        #region 3.选择Transform

        string _selectTransform = "";

        [Title("输出当前选择的 Transform 的名称"), InfoBox("只能选择到场景中的对象的 Transform"), OnInspectorGUI]
        void GUISelection3()
        {
            if (GUILayout.Button("输出选择的 Transform 的名称"))
                _selectTransform = Selection.activeTransform != null ? Selection.activeTransform.name : "没有选择";

            EditorGUILayout.LabelField(new GUIContent("当前选择的 Transform 的名称"), new GUIContent(_selectTransform));
        }

        #endregion

        #region 4.选择所有 Object 的名称

        readonly StringBuilder _builder = new StringBuilder();

        [Title("输出当前选择的所有 Object 的名称"), OnInspectorGUI]
        void GUISelection4()
        {
            if (GUILayout.Button("输出选择的所有 Object 的名称"))
                if (Selection.objects.Length != 0)
                {
                    _builder.Clear();
                    foreach (var t in Selection.objects) _builder.Append(t.name + "||");
                }

            EditorGUILayout.LabelField(new GUIContent("当前选择的所有 Object 的名称"), new GUIContent(_builder.ToString()));
        }

        #endregion

        #region 6.常用静态方法

        Object _obj;

        [Title("Selection 常用静态方法 - 1"), InfoBox("ObjectField 选择一个 GameObject 对象检测是否被选中，清除选择，高亮选择，复制选择，选择复制路径的对象"),
         OnInspectorGUI]
        void GUISelection6()
        {
            _obj = EditorGUILayout.ObjectField(_obj, typeof(GameObject), true);
            if (GUILayout.Button("检测是否被选中"))
                if (Selection.objects.Length > 0)
                {
                    if (Selection.Contains(_obj))
                        Debug.Log(_obj.name + " 被选中");
                    else
                        Debug.Log(_obj.name + " 没有被选中");
                }

            if (GUILayout.Button("清除选择"))
                Selection.activeObject = null;

            if (GUILayout.Button("高亮选择"))
                if (Selection.activeObject != null)
                    EditorGUIUtility.PingObject(Selection.activeObject);

            if (GUILayout.Button("复制选择"))
                if (Selection.activeObject != null)
                    EditorGUIUtility.systemCopyBuffer = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (GUILayout.Button("选择复制路径的对象"))
                if (!string.IsNullOrEmpty(EditorGUIUtility.systemCopyBuffer))
                    Selection.activeObject =
                        AssetDatabase.LoadAssetAtPath(EditorGUIUtility.systemCopyBuffer, typeof(Object));
        }

        #endregion

        #region 8.选择改变的委托 Action

        protected override void Initialize()
        {
            base.Initialize();
            Selection.selectionChanged += SelectionChange;
        }

        protected override void OnDestroy() => Selection.selectionChanged -= SelectionChange;

        static void SelectionChange()
        {
            Debug.Log("选择改变");
        }

        #endregion
    }
}