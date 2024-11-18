using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide._1.OdinEditorWindow.Editor
{
    public class PublicStaticInspectObjectOrInDropDown : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        PublicShowToast _targetObject;

        [MenuItem("Zeus Framework/Odin 扩展模块/Editor API 演示/OdinEditorWindow/2.InspectObject", priority = 20)]
        static void OpenWindow()
        {
            var win = GetWindow<PublicStaticInspectObjectOrInDropDown>();
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        [PropertySpace, HideLabel, ShowInInspector, EnableGUI]
        [DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 12)]
        public string Display
        {
            get
            {
#if UNITY_EDITOR
                GUIHelper.RequestRepaint();
#endif
                return "使用提示: " +
                       "Rect全部为0时，将紧贴左上角，当X为宽度的负数时将会紧贴左边，通常只要注意X的值其他三个值全部为零即可\n" +
                       "内部会进行一次 GUIUtility.GUIToScreenPoint(btnRect.position) 方法转换\n";
            }
        }

        [InfoBox("Zeus 推荐使用！是直接打开一个新窗口，可以通过返回值设置相关信息")]
        [Button("触发 InspectObject(object a) 方法")]
        void Method1()
        {
            // 创建一个 OdinEditorWindow 类型的实例
            _targetObject = CreateInstance<PublicShowToast>();
            var win = InspectObject(_targetObject);
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(300f, 400f);
        }

        [InfoBox("选择一个已经有的窗口，直接霸占并代替原窗口的内容，可以通过返回值设置相关信息")]
        [Button("触发 InspectObject(OdinEditorWindow a,object b) 方法")]
        void Method2()
        {
            _targetObject = CreateInstance<PublicShowToast>();
            var win = GetWindow<PublicStaticInspectObjectOrInDropDown>();
            InspectObject(win, _targetObject);
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(500f, 600f);
        }

        [InfoBox("以点击位置为左上角，绘制一个窗口，点击其他地方就会关闭")]
        [Button("触发 InspectObjectInDropDown(object a) 方法")]
        void Method3()
        {
            _targetObject = CreateInstance<PublicShowToast>();
            InspectObjectInDropDown(_targetObject);
        }

        [InfoBox("Zeus 推荐使用！可以控制位置，建议Rect 的 X 值与 Vector2 的 X 值相反，第二个参数是控制大小")]
        [Button("触发 InspectObjectInDropDown(object obj,Rect btnRect,Vector2 windowSize) 方法")]
        void Method4()
        {
            _targetObject = CreateInstance<PublicShowToast>();
            InspectObjectInDropDown(_targetObject, new Rect(-300f, 0f, 0f, 0f), new Vector2(300f, 400f));
        }

        [InfoBox("Zeus 推荐使用！可以控制位置，建议Rect 的 X 值与 Vector2 的 X 值相反，第二个参数是控制宽度，自适应高度")]
        [Button("触发 InspectObjectInDropDown(object obj,Rect btnRect,float width) 方法")]
        void Button()
        {
            // 创建一个 OdinEditorWindow 类型的实例
            _targetObject = CreateInstance<PublicShowToast>();
            // InspectObjectInDropDown 优先自适应高度，
            // Rect 全部为0时 将紧贴左上角 当X为宽度的负数时将会紧贴左边  通常只要注意X的值其他三个值全部为零即可 
            InspectObjectInDropDown(_targetObject, new Rect(-300f, 0f, 0f, 0f), 300f);
        }

        [InfoBox("可以控制位置，第二个参数的位置通常以面板的左上角为原点，它会自适应偏左还是偏右，内部进行了转换，不方便控制。 ")]
        [Button("触发 InspectObjectInDropDown(object obj,Vector2 position) 方法")]
        void Button2()
        {
            _targetObject = CreateInstance<PublicShowToast>();
            InspectObjectInDropDown(_targetObject, new Vector2(300f, 400f));
        }

        [InfoBox("可以控制位置，第二个参数的位置通常以面板的左上角为原点，它会自适应偏左还是偏右，可以自定义宽度，内部进行了转换，不方便控制。")]
        [Button("触发 InspectObjectInDropDown(object obj,Vector2 position,float w) 方法")]
        void Button3()
        {
            _targetObject = CreateInstance<PublicShowToast>();
            InspectObjectInDropDown(_targetObject, new Vector2(300f, 400f), 500f);
        }

        [InfoBox("根据点击位置确定面板的四角，可以控制宽和高。")]
        [Button("触发 InspectObjectInDropDown(object obj,float w,float h) 方法")]
        void Button4()
        {
            _targetObject = CreateInstance<PublicShowToast>();
            InspectObjectInDropDown(_targetObject, 400f, 500f);
        }

        [InfoBox("根据点击位置确定面板的四角，可以控制宽，自适应高度。")]
        [Button("触发 InspectObjectInDropDown(object obj,float w) 方法")]
        void Button5()
        {
            _targetObject = CreateInstance<PublicShowToast>();
            InspectObjectInDropDown(_targetObject, 300f);
        }

        protected override void DrawEditors()
        {
            base.DrawEditors();
            var btnRect = new Rect(100f, 300f, 300f, 50f);
            // GUI.Box(btnRect, "测试窗口-new Rect(100f, 300f, 300f, 50f)");
        }
    }
}