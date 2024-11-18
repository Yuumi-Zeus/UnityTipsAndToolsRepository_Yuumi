using Odin_Toolkits.Editor_API_Usage_Guide.General.Editor;

namespace Odin_Toolkits.Editor_API_Usage_Guide._14.UnityAssetImporterAndAssetPostprocessor.Editor
{
    public class UnityAssetImporterAndAssetPostprocessor
        : AbstractEditorTutorialWindow<UnityAssetImporterAndAssetPostprocessor>
    {
        protected override string SetUsageTip()
        {
            return
                "案例介绍: Unity内置AssetImporter和AssetPostprocessorAPI使用示例，用于资源导入和处理，" +
                "可以自定义一个脚本，继承自AssetPostprocessor，对不同类型的资源在导入时进行处理，统一设置，" +
                "AssetImporter是特定资源类型的资源导入程序的基类，通常是指点击资源的Inspector面板设置，可以设置资源的导入属性，" +
                "如纹理的压缩格式等";
        }
    }
}