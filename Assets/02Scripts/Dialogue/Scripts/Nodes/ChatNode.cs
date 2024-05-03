using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class ChatNode : DialogueBaseNode {

        public TalkerInfo character;
        [TextArea] public string text;
        [Output(dynamicPortList = true)] public List<Answer> answers = new List<Answer>();

        [System.Serializable]
        public class Answer {
            public string text;
        }

        public bool NextNode() {

            NodePort port = GetOutputPort("output");

            if (port == null) return false;
            if (port.ConnectionCount == 0) return false;

            for (int i = 0; i < port.ConnectionCount; i++) {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }

            if (!port.GetConnections().Exists(x => x.node is ChatNode)) return false;

            return true;
        }

        public bool AnswerNode(int index) {

            if (answers.Count == 0) return false;
            if (answers.Count <= index) return false;

            NodePort port = GetOutputPort("answers " + index);

            if (port == null) return false;
            if (port.ConnectionCount == 0) return false;

            for (int i = 0; i < port.ConnectionCount; i++) {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }

            if (!port.GetConnections().Exists(x => x.node is ChatNode)) return false;

            return true;
        }

        public override void Trigger() {
            (graph as DialogueGraph).currentNode = this;
        }
    }
}