using Odin_Toolkits.Common_Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Odin_Toolkits.DevToolkit.General.Editor
{
    public interface IDevTool
    {
        public OdinMenuEditorWindow OwnerWindow { set; }
        void ResetData();
    }

    public abstract class AbstractDevTool<T> : SerializedScriptableObject, IDevTool where T : AbstractDevTool<T>
    {
        public const string DefaultNamespace = "ZeusDevToolkitCodeGen";

        const string ScriptableObjectFolderPath ="";

        static T _dataIns;

        public static T Ins
        {
            get
            {
                if (_dataIns == null)
                {
                    _dataIns = ProjectUtils.GetSingleSoAndDeleteExtra<T>(ScriptableObjectFolderPath + "/" +
                                                                         typeof(T).Name + ".asset");
                }

                return _dataIns;
            }
        }

        public OdinMenuEditorWindow OwnerWindow { get; set; }

        [PropertyOrder(-1)]
        [TitleGroup("使用提示")]
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

        [PropertyOrder(980)]
        [TitleGroup("工具箱通用操作")]
        [HorizontalGroup("工具箱通用操作/Split", 0.5f)]
        [Button("重置工具配置数据", ButtonSizes.Medium, Icon = SdfIconType.ArrowCounterclockwise,
            IconAlignment = IconAlignment.LeftOfText)]
        public abstract void ResetData();

        [PropertyOrder(1000)]
        [TitleGroup("工具箱通用操作")]
        [HorizontalGroup("工具箱通用操作/Split", 0.5f)]
        [Button("锁定脚本", ButtonSizes.Medium, Icon = SdfIconType.EyeFill)]
        protected void PingScript()
        {
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(ProjectUtils.Scripts.FindAndSelectedScript(typeof(T).Name));
#endif
        }

        protected abstract string SetUsageTip();
    }
}