using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Dialogue {
    [CreateAssetMenu(menuName = "Dialogue/DialogueGraph", fileName = "DialogueGraph", order = 0)]
    public class DialogueGraph : NodeGraph {
        [HideInInspector]
        public ChatNode currentNode;
        public bool isSkipable;
        public bool isCancelable;
        public bool isReTalkable;

        public DialogueAcceptionCondition[] acceptionConditions;
        public string dialogueName;

        public bool IsAcceptable => acceptionConditions.All(x => x.IsPass(this));

        public void FindFirstNode() {
            //Find the first DialogueNode without any inputs. This is the starting node.
            currentNode = nodes.Find(x => x is ChatNode && x.Inputs.All(y => !y.IsConnected)) as ChatNode;
        }

        public bool NextNode() {
            return currentNode.NextNode();
        }

        public bool AnswerNode(int i) {
            return currentNode.AnswerNode(i);
        }
    }
}