using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide.General.Editor
{
    public abstract class AbstractEditorTutorialWindow<T> : OdinEditorWindow where T : AbstractEditorTutorialWindow<T>
    {
        public OdinMenuEditorWindow OwnerWindow { get; set; }

        [PropertyOrder(-1)]
        [TitleGroup("案例介绍")]
        [ShowInInspector, EnableGUI, HideLabel]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        public string UsageTip
        {
            get
            {
                GUIHelper.RequestRepaint();
                return SetUsageTip();
            }
        }

        [PropertyOrder(1000)]
        [TitleGroup("锁定脚本工具")]
        [Button("锁定脚本", ButtonSizes.Medium, Icon = SdfIconType.EyeFill)]
        protected void PingScript()
        {
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(ProjectUtils.Scripts.FindAndSelectedScript(typeof(T).Name));
#endif
        }

        protected void OwnerNormalToast(string text, float duration = 3f)
        {
            if (OwnerWindow != null)
            {
                OwnerWindow.NormalToast(text, duration);
            }
        }

        protected void OwnerErrorToast(string text, float duration = 3f)
        {
            if (OwnerWindow != null)
            {
                OwnerWindow.ErrorToast(text, duration);
            }
        }

        protected abstract string SetUsageTip();
    }
}