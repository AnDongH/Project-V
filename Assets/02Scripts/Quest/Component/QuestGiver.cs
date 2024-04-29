using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    public void GiveQuest() {
        foreach (var quest in quests) {
            if (quest.IsAcceptable) {
                Access.QuestM.OnQuestRegisteredHandler += (quest) =>
                {
                    print($"New Quest:{quest.ID} Registered");
                    print($"Active Quests Count:{Access.QuestM.ActiveQuests.Count}");
                };

                Access.QuestM.OnQuestCompletedHandler += (quest) =>
                {
                    print($"Quest:{quest.ID} Completed");
                    print($"Completed Quests Count:{Access.QuestM.CompletedQuests.Count}");
                };

                var newQuest = Access.QuestM.Register(quest);
                newQuest.OnTaskConditionChanged += (quest, task, currentSuccess, prevSuccess) =>
                {
                    print($"Quest:{quest.ID}, Task:{task.ID}, CurrentSuccess:{currentSuccess}");
                };
            }
        }
    }
}
