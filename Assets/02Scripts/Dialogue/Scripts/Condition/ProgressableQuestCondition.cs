using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    [CreateAssetMenu(menuName = "Dialogue/AcceptionCondition/ProgressableQuestCondition", fileName = "ProgressableQuestCondition")]
    public class ProgressableQuestCondition : DialogueAcceptionCondition {

        [SerializeField] private Category category;
        [SerializeField] private TaskTarget target;

        public override bool IsPass(DialogueGraph dialogue) {
            return Access.QuestM.IsTarget(category, target);
        }
    }
}
