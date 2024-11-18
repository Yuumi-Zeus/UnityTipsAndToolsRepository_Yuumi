using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Odin_Demos_Analysis.Editor_Windows_Demo_Analysis.Scripts.Editor
{
    public class OdinMenuStyleExample : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Odin/Demos/Odin Editor Window Demos/Odin Menu Style Example")]
        static void OpenWindow()
        {
            var window = GetWindow<OdinMenuStyleExample>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);

            var customMenuStyle = new OdinMenuStyle
            {
                BorderPadding = 0f,
                AlignTriangleLeft = true,
                TriangleSize = 16f,
                TrianglePadding = 0f,
                Offset = 20f,
                Height = 23,
                IconPadding = 0f,
                BorderAlpha = 0.323f
            };

            tree.DefaultMenuStyle = customMenuStyle;

            tree.Config.DrawSearchToolbar = true;

            // Adds the custom menu style to the tree, so that you can play around with it.
            // Once you are happy, you can press Copy C# Snippet copy its settings and paste it in code.
            // And remove the "Menu Style" menu item from the tree.
            tree.AddObjectAtPath("Menu Style", customMenuStyle);

            for (var i = 0; i < 5; i++)
            {
                var customObject = new SomeCustomClass() { Name = i.ToString() };
                var customMenuItem = new MyCustomMenuItem(tree, customObject);
                tree.AddMenuItemAtPath("Custom Menu Items", customMenuItem);
            }

            tree.AddAllAssetsAtPath("Scriptable Objects in Plugins Tree", "Plugins", typeof(ScriptableObject), true,
                false);
            tree.AddAllAssetsAtPath("Scriptable Objects in Plugins Flat", "Plugins", typeof(ScriptableObject), true,
                true);
            tree.AddAllAssetsAtPath("Only Configs has Icons", "Plugins/Sirenix", true, false);

            tree.EnumerateTree()
                .AddThumbnailIcons()
                .SortMenuItemsByName();

            return tree;
        }

        //// The editor window itself can also be customized.
        //protected override void OnEnable()
        //{
        //    base.OnEnable();

        //    this.MenuWidth = 200;
        //    this.ResizableMenuWidth = true;
        //    this.WindowPadding = new Vector4(10, 10, 10, 10);
        //    this.DrawUnityEditorPreview = true;
        //    this.DefaultEditorPreviewHeight = 20;
        //    this.UseScrollView = true;
        //}

        class MyCustomMenuItem : OdinMenuItem
        {
            readonly SomeCustomClass instance;

            public MyCustomMenuItem(OdinMenuTree tree, SomeCustomClass instance) :
                base(tree, instance.Name, instance) => this.instance = instance;

            public override string SmartName => instance.Name;

            protected override void OnDrawMenuItem(Rect rect, Rect labelRect)
            {
                labelRect.x -= 16;
                instance.Enabled = GUI.Toggle(labelRect.AlignMiddle(18).AlignLeft(16), instance.Enabled,
                    GUIContent.none);

                // Toggle selection when pressing space.
                if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space)
                {
                    var selection = MenuTree.Selection
                        .Select(x => x.Value)
                        .OfType<SomeCustomClass>();

                    if (selection.Any())
                    {
                        var enabled = !selection.FirstOrDefault().Enabled;
                        selection.ForEach(x => x.Enabled = enabled);
                        Event.current.Use();
                    }
                }
            }
        }

        class SomeCustomClass
        {
            public bool Enabled = true;
            public string Name;
        }
    }
}
#endif