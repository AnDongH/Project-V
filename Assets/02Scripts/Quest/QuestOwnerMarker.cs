using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOwnerMarker : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    private void Start() {
        foreach (var quest in quests) {
            if (quest.IsAcceptable) {

            }
        }
    }
}
