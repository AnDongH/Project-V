using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(Dialogue.EventNode))]
    public class EventEditor : NodeEditor {
        public override void OnBodyGUI() {
            serializedObject.Update();

            Dialogue.EventNode node = target as Dialogue.EventNode;
            NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            EditorGUILayout.Space();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("trigger"));

            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth() {
            return 336;
        }
    }
}