using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    [CreateAssetMenu(menuName = "Dialogue/AcceptionCondition/GetableQuestCondition", fileName = "GetableQuestCondition")]
    public class GetableQuestCondition : DialogueAcceptionCondition {

        [SerializeField] Quest quest;

        public override bool IsPass(DialogueGraph dialogue) {
            return quest.IsAcceptable;
        }
    }
}

