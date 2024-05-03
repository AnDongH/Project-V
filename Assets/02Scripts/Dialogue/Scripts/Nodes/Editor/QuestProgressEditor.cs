using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(Dialogue.QuestProgressNode))]
    public class QuestProgressEditor : NodeEditor {
        public override void OnBodyGUI() {
            serializedObject.Update();

            Dialogue.QuestProgressNode node = target as Dialogue.QuestProgressNode;
            NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            EditorGUILayout.Space();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("category"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("target"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("conditionCount"));

            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth() {
            return 336;
        }
    }
}
