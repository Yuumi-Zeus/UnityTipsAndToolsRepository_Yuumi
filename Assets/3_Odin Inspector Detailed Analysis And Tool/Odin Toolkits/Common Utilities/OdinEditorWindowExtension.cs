using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Odin_Toolkits.Common_Utilities
{
    public static class OdinEditorWindowExtension
    {
#if UNITY_EDITOR
        public static void NormalToast(this OdinEditorWindow window, string message, float duration = 3f)
        {
            message = "Yuumi Say" + message;
            window.ShowToast(ToastPosition.BottomRight, SdfIconType.Info, message, Color.white, duration);
        }

        public static void ErrorToast(this OdinEditorWindow window, string message, float duration = 3f)
        {
            message = "Yuumi Say" + message;
            window.ShowToast(ToastPosition.BottomRight, SdfIconType.ExclamationSquareFill, message, Color.yellow,
                duration);
        }
#endif
    }
}