using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class QuestProgressNode : DialogueBaseNode {

        [SerializeField] private Category category;
        [SerializeField] private TaskTarget target;
        [SerializeField] private int conditionCount;

        public override void Trigger() {
            if (Access.QuestM.IsTarget(category, target)) Access.QuestM.ReceiveReport(category, target, conditionCount);
        }
    }
}
