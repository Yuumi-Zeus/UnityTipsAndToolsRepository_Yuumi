using Odin_Toolkits.Editor_API_Usage_Guide._1.OdinEditorWindow.Editor;
using Odin_Toolkits.Editor_API_Usage_Guide._10.UnityAssetDatabase.Editor;
using Odin_Toolkits.Editor_API_Usage_Guide._11.UnityPrefabUtility.Editor;
using Odin_Toolkits.Editor_API_Usage_Guide._12.UnityEditorApplication.Editor;
using Odin_Toolkits.Editor_API_Usage_Guide._13.UnityCompilationPipeline.Editor;
using Odin_Toolkits.Editor_API_Usage_Guide._14.UnityAssetImporterAndAssetPostprocessor.Editor;
using Odin_Toolkits.Editor_API_Usage_Guide._9.UnityEditorUtility.Editor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Odin_Toolkits.Editor_API_Usage_Guide.General.Editor
{
    public class EditorAPILearnMenuEditorWindow : OdinMenuEditorWindow
    {
        
        static void ShowWindow()
        {
            var window = GetWindow<EditorAPILearnMenuEditorWindow>();
            window.titleContent = new GUIContent("EditorAPILearnMenuEditorWindow");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 650);
            window.Show();
        }

        #region Windows

        PublicShowToast _publicShowToast;
        UnityEditorUtilityAPI _unityEditorUtilityAPI;
        UnityAssetDatabaseAPI _unityAssetDatabaseAPI;
        UnityPrefabUtilityAPI _unityPrefabUtilityAPI;
        UnityEditorApplicationAPI _unityEditorApplicationAPI;
        UnityCompilationPipelineAPI _unityCompilationPipelineAPI;
        UnityAssetImporterAndAssetPostprocessor _unityAssetImporterAndAssetPostprocessor;

        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            MenuWidth = 220;
            WindowPadding = new Vector4(7, 7, 7, 7);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _publicShowToast = CreateInstance<PublicShowToast>();
            // ---
            _unityEditorUtilityAPI = CreateInstance<UnityEditorUtilityAPI>();
            _unityEditorUtilityAPI.OwnerWindow = this;
            // ---
            _unityAssetDatabaseAPI = CreateInstance<UnityAssetDatabaseAPI>();
            _unityAssetDatabaseAPI.OwnerWindow = this;
            // ---
            _unityPrefabUtilityAPI = CreateInstance<UnityPrefabUtilityAPI>();
            _unityPrefabUtilityAPI.OwnerWindow = this;
            // ---
            _unityEditorApplicationAPI = CreateInstance<UnityEditorApplicationAPI>();
            _unityEditorApplicationAPI.OwnerWindow = this;
            // ---
            _unityCompilationPipelineAPI = CreateInstance<UnityCompilationPipelineAPI>();
            _unityCompilationPipelineAPI.OwnerWindow = this;
            // ---
            _unityAssetImporterAndAssetPostprocessor = CreateInstance<UnityAssetImporterAndAssetPostprocessor>();
            _unityAssetImporterAndAssetPostprocessor.OwnerWindow = this;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _publicShowToast = null;
            // --- 
            _unityEditorUtilityAPI.OwnerWindow = null;
            _unityEditorUtilityAPI = null;
            // ---
            _unityAssetDatabaseAPI.OwnerWindow = null;
            _unityAssetDatabaseAPI = null;
            // ---
            _unityPrefabUtilityAPI.OwnerWindow = null;
            _unityPrefabUtilityAPI = null;
            // ---
            _unityEditorApplicationAPI.OwnerWindow = null;
            _unityEditorApplicationAPI = null;
            // ---
            _unityCompilationPipelineAPI.OwnerWindow = null;
            _unityCompilationPipelineAPI = null;
            // ---
            _unityAssetImporterAndAssetPostprocessor.OwnerWindow = null;
            _unityAssetImporterAndAssetPostprocessor = null;
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true)
            {
                { "OdinEditorWindow/ShowToast 方法", _publicShowToast },
                { "Unity 内置/EditorUtility API 简易版", _unityEditorUtilityAPI },
                { "Unity 内置/AssetDatabase API 简易版", _unityAssetDatabaseAPI },
                { "Unity 内置/PrefabUtility API 简易版", _unityPrefabUtilityAPI },
                { "Unity 内置/EditorApplication API 简易版", _unityEditorApplicationAPI },
                { "Unity 内置/CompilationPipeline API 简易版", _unityCompilationPipelineAPI },
                { "Unity 内置/AssetImporter & AssetPostprocessor 说明简易版", _unityAssetImporterAndAssetPostprocessor }
            };
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}