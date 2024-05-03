using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(Dialogue.QuestGetNode))]
    public class QuestGetEditor : NodeEditor {
        public override void OnBodyGUI() {
            serializedObject.Update();

            Dialogue.QuestGetNode node = target as Dialogue.QuestGetNode;
            NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            EditorGUILayout.Space();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("quest"));

            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth() {
            return 336;
        }
    }
}
