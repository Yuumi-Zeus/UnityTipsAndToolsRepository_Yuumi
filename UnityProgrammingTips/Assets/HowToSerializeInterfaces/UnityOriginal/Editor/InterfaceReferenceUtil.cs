using UnityEditor;
using UnityEngine;

namespace HowToSerializeInterfaces.UnityOriginal.Editor
{
    public class InterfaceReferenceUtil
    {
        static GUIStyle _labelStyle;

        public static void OnGUI(Rect position, SerializedProperty property, GUIContent label, InterfaceArgs args)
        {
            InitializeStyleIfNeeded();

            var controlId = GUIUtility.GetControlID(FocusType.Passive) - 1;
            var isHover = position.Contains(Event.current.mousePosition);
            var displayString = property?.objectReferenceValue == null || isHover
                ? $"({args.InterfaceType.Name})"
                : "*";
            DrawInterfaceReference(position, displayString, controlId);
        }

        static void DrawInterfaceReference(Rect position, string displayString, int controlId)
        {
            if (Event.current.type == EventType.Repaint)
            {
                const int additionalLeftWidth = 3;
                const int verticalIndent = 1;
                // Tr 前缀的方法是本地化友好的
                var content = EditorGUIUtility.TrTextContent(displayString);
                var size = _labelStyle.CalcSize(content);
                var labelPos = position;
                labelPos.width = size.x + additionalLeftWidth;
                labelPos.x += position.width - labelPos.width - 18;
                labelPos.height -= verticalIndent * 2;
                labelPos.y += verticalIndent;
                _labelStyle.Draw(labelPos, content, controlId, DragAndDrop.activeControlID == controlId, false);
            }
        }

        static void InitializeStyleIfNeeded()
        {
            if (_labelStyle != null)
            {
                return;
            }

            var style = new GUIStyle(EditorStyles.label)
            {
                font = EditorStyles.objectField.font,
                fontSize = EditorStyles.objectField.fontSize,
                fontStyle = EditorStyles.objectField.fontStyle,
                alignment = TextAnchor.MiddleRight,
                padding = new RectOffset(0, 2, 0, 0)
            };
            _labelStyle = style;
        }
    }
}