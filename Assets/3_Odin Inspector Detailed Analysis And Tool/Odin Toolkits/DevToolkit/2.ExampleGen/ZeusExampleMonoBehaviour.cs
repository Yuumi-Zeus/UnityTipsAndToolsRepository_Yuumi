using Sirenix.OdinInspector;
using UnityEngine;

namespace Odin_Toolkits.DevToolkit._2.ExampleGen
{
    public abstract class ZeusExampleMonoBehaviour : MonoBehaviour
    {
        [PropertyOrder(-1)]
        [PropertySpace, HideLabel, ShowInInspector, EnableGUI]
        [DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 12)]
        public string UsageTip
        {
            get
            {
#if UNITY_EDITOR
                Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
#endif
                return SetUsageTip();
            }
        }

        void OnGUI()
        {
            CustomGUIUseGUILayout();
        }

        protected abstract void CustomGUIUseGUILayout();
        protected abstract string SetUsageTip();
    }
}