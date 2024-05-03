using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class QuestGetNode : DialogueBaseNode {

        [SerializeField] private Quest quest;

        public override void Trigger() {
            if (quest.IsAcceptable) {
                Access.QuestM.OnQuestRegisteredHandler += (quest) => {
                    Define.Log($"New Quest:{quest.ID} Registered");
                    Define.Log($"Active Quests Count:{Access.QuestM.ActiveQuests.Count}");
                };

                Access.QuestM.OnQuestCompletedHandler += (quest) => {
                    Define.Log($"Quest:{quest.ID} Completed");
                    Define.Log($"Completed Quests Count:{Access.QuestM.CompletedQuests.Count}");
                };

                var newQuest = Access.QuestM.Register(quest);
                newQuest.OnTaskConditionChanged += (quest, task, currentSuccess, prevSuccess) => {
                    Define.Log($"Quest:{quest.ID}, Task:{task.ID}, CurrentSuccess:{currentSuccess}");
                };
            }
        }
    }
}
