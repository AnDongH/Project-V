using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlayerDInfo : CreatureDInfo
{
    public OldQuest CurQuest { get; set; } = null;
    public List<OldQuest.QuestID> ClearQuestsID { get; set; } = new List<OldQuest.QuestID>() { OldQuest.QuestID.M0 };
    public PlayableDirector CurDirector { get; set; } = null;
}