using System;
using System.Collections.Generic;
using Odin_Toolkits.Common_Utilities;
using Odin_Toolkits.DevToolkit._3.FindUsableAPI;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Odin_Toolkits.Editor_API_Usage_Guide._1.OdinEditorWindow.Editor
{
    public class OdinEditorWindowAPI : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        Label _label;

        // EditorWindow.CreateGUI() 方法官方案例参数
        string _mouseOver = "Nothing...";

        [MenuItem(OdinToolkitsConfig.OdinEditorAPIUsageGuidePath + "/" + "OdinEditorWindow")]
        static void OpenWindow()
        {
            var win = GetWindow<OdinEditorWindowAPI>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        [InfoBox("单个报错，不用管，是官方案例中选择当前鼠标所在面板的错误，但不知道是哪一行"), OnInspectorGUI]
        void Space() { }

        [PropertyOrder(1000), TitleGroup("工具箱通用操作"), Button("锁定脚本", ButtonSizes.Medium, Icon = SdfIconType.EyeFill)]
        protected void PingScript()
        {
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(ProjectUtils.Scripts.FindAndSelectedScript(nameof(OdinEditorWindowAPI)));
#endif
        }

        #region Odin 属性，不要 override，直接使用即可，OdinEditorWindow 基类使用了内部私有字段进行赋值

        // public override Vector4 WindowPadding { get; set; }
        //
        // public override float DefaultLabelWidth { get; set; }
        //
        // public override bool UseScrollView { get; set; }
        //
        // public override float DefaultEditorPreviewHeight { get; set; }
        //
        // public override bool DrawUnityEditorPreview { get; set; }

        #endregion

        #region Odin API 以及极小部分 Unity API，通常不需要 Override，直接使用即可

        /// <summary>
        /// Unity API
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Type> GetExtraPaneTypes()
        {
            Debug.Log("GetExtraPaneTypes 触发了");
            return base.GetExtraPaneTypes();
        }

        /// <summary>
        /// Odin API，通常直接使用，不需要 Override
        /// </summary>
        protected override object GetTarget()
        {
            Debug.Log("GetTarget 触发了");
            return base.GetTarget();
        }

        /// <summary>
        /// Odin API，通常直接使用，不需要 Override
        /// </summary>
        protected override IEnumerable<object> GetTargets()
        {
            Debug.Log("GetTargets 触发了");
            return base.GetTargets();
        }

        /// <summary>
        /// Unity API，通常用于需要保存的某些界面
        /// </summary>
        public override void SaveChanges()
        {
            Debug.Log("SaveChanges 触发了");
            base.SaveChanges();
        }

        /// <summary>
        /// Unity API，通常用于需要保存的某些界面
        /// </summary>
        public override void DiscardChanges()
        {
            Debug.Log("DiscardChanges 触发了");
            base.DiscardChanges();
        }

        #endregion

        #region API列表

        [TitleGroup("API 列表"), ShowInInspector, EnableGUI, LabelText("可以使用的属性和事件方法，每页10个，右侧翻页")]
        public string[] GetSetOrEventMethods =>
            UsableAPIViewTool.FilterGetAndSetOrEventMethods(typeof(Sirenix.OdinInspector.Editor.OdinEditorWindow));

        [TitleGroup("API 列表"), ShowInInspector, EnableGUI, LabelText("可以使用的公共和受保护的非静态方法，每页10个，右侧翻页")]
        public string[] PublicAndProtectedMethods =>
            UsableAPIViewTool.GetPublicAndProtectedMethods(typeof(Sirenix.OdinInspector.Editor.OdinEditorWindow));

        [TitleGroup("API 列表"), ShowInInspector, EnableGUI, LabelText("可以使用的公共和受保护的静态方法，每页10个，右侧翻页")]
        public string[] PublicAndProtectedStaticMethods =>
            UsableAPIViewTool.GetPublicAndProtectedStaticMethods(typeof(Sirenix.OdinInspector.Editor.OdinEditorWindow));

        #endregion

        #region 事件函数

        void Awake()
        {
            Debug.Log("Awake 触发了");
        }

        void Reset()
        {
            Debug.Log("Reset 触发了");
        }

        void Update()
        {
            // EditorWindow.CreateGUI() 方法官方案例
            _label.schedule.Execute(() =>
            {
                _mouseOver = mouseOverWindow != null
                    ? mouseOverWindow.ToString()
                    : "Nothing...";
                _label.text = _mouseOver != null
                    ? $"Mouse over: {_mouseOver}"
                    : "Mouse over: _mouseOver == null";
                // _label.text = $"Mouse over: {_mouseOver}";
            }).Every(10);
            Debug.Log("Update 触发了");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Debug.Log("Odin OnEnable 触发了");
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Debug.Log("Odin OnDisable 触发了");
        }

        protected override void OnDestroy()
        {
            Debug.Log("Odin OnDestroy 触发了");
            base.OnDestroy();
        }

        void ModifierKeysChanged()
        {
            Debug.Log("ModifierKeysChanged 触发了");
        }

        void OnAddedAsTab()
        {
            Debug.Log("OnAddedAsTab 触发了");
        }

        void OnBecameInvisible()
        {
            Debug.Log("OnBecameInvisible 触发了");
        }

        void OnBecameVisible()
        {
            Debug.Log("OnBecameVisible 触发了");
        }

        void OnBeforeRemovedAsTab()
        {
            Debug.Log("OnBeforeRemovedAsTab 触发了");
        }

        void OnDidOpenScene()
        {
            Debug.Log("OnDidOpenScene 触发了");
        }

        void OnFocus()
        {
            Debug.Log("OnFocus 触发了");
        }

        void OnHierarchyChange()
        {
            Debug.Log("OnHierarchyChange 触发了");
        }

        /// <summary>
        /// 一般情况下推荐使用 OnInspectorUpdate 方法来代替 Update 方法作为界面刷新方法
        /// </summary>
        void OnInspectorUpdate()
        {
            Debug.Log("OnInspectorUpdate 触发了");
        }

        void OnLostFocus()
        {
            Debug.Log("OnLostFocus 触发了");
        }

        void OnMainWindowMove()
        {
            Debug.Log("OnMainWindowMove 触发了");
        }

        void OnProjectChange()
        {
            Debug.Log("OnProjectChange 触发了");
        }

        void OnSelectionChange()
        {
            Debug.Log("OnSelectionChange 触发了");
        }

        void OnTabDetached()
        {
            Debug.Log("OnTabDetached 触发了");
        }

        void OnValidate()
        {
            Debug.Log("OnValidate 触发了");
        }

        /// <summary>
        /// 触发时机不明确，官方文档没有说明，不使用
        /// </summary>
        void ShowButton(Rect rect)
        {
            Debug.Log("ShowButton 触发了");
        }

        #endregion

        #region Odin API Protected Methods 事件函数 会在 OdinEditorWindow 中自动触发

        /// <summary>
        /// 此方法在 CreateGUI 方法中被调用，用于绘制窗口内容，但是不等于 CreateGUI 方法，会重复调用很多次
        /// </summary>
        protected override void OnImGUI()
        {
            base.OnImGUI();
            // EditorWindow.CreateGUI() 方法官方案例
            // _label = new Label($"Mouse over: {_mouseOver}");
            // rootVisualElement.Add(_label);
            // --- 
            Debug.Log("Odin OnImGUI 触发了");
        }

        protected override void DrawEditor(int index)
        {
            base.DrawEditor(index);
            Debug.Log("Odin DrawEditor (int index) 触发了");
        }

        protected override void DrawEditors()
        {
            base.DrawEditors();
            Debug.Log("Odin DrawEditors 触发了");
        }

        protected override void DrawEditorPreview(int index, float height)
        {
            base.DrawEditorPreview(index, height);
            Debug.Log("Odin DrawEditorPreview (int index, float height) 触发了");
        }

        protected override void Initialize()
        {
            base.Initialize();
            // EditorWindow.CreateGUI() 方法官方案例
            _label = _mouseOver != null
                ? new Label($"Mouse over: {_mouseOver}")
                : new Label($"Mouse over: _mouseOver == null");

            rootVisualElement.Add(_label);
            // --- 
            Debug.Log("Odin Initialize 触发了");
        }

        protected override void OnBeginDrawEditors()
        {
            base.OnBeginDrawEditors();
            Debug.Log("Odin OnBeginDrawEditors 触发了");
        }

        protected override void OnEndDrawEditors()
        {
            base.OnEndDrawEditors();
            Debug.Log("Odin OnEndDrawEditors 触发了");
        }

        #endregion
    }
}