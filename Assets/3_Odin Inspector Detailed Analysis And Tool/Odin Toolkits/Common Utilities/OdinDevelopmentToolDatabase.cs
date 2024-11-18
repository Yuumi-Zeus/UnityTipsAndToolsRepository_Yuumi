using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Odin_Toolkits.Common_Utilities
{
    public abstract class OdinDevelopmentToolDatabase<T> : ScriptableObject where T : OdinDevelopmentToolDatabase<T>
    {
        [PropertyOrder(980), TitleGroup("工具数据库通用操作"), HorizontalGroup("工具数据库通用操作/Split", 0.3f), Button("清空工具数据库",
             ButtonSizes.Medium, Icon = SdfIconType.ArrowCounterclockwise,
             IconAlignment = IconAlignment.LeftOfText)]
        public abstract void ClearDatabase();

        [PropertyOrder(990), TitleGroup("工具数据库通用操作"), HorizontalGroup("工具数据库通用操作/Split", 0.3f), Button("初始化数据库",
             ButtonSizes.Medium, Icon = SdfIconType.App,
             IconAlignment = IconAlignment.LeftOfText)]
        public abstract void InitializeData();

        [PropertyOrder(1000), TitleGroup("工具数据库通用操作"), HorizontalGroup("工具数据库通用操作/Split", 0.4f),
         Button("锁定数据库原始脚本", ButtonSizes.Medium, Icon = SdfIconType.EyeFill)]
        protected void PingScript()
        {
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(OdinWithProjectUtil.FindAndSelectedScript(typeof(T).Name));
#endif
        }
    }
}