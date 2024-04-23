using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class Player_DInfo : Creature_DInfo
{
    public Quest CurQuest { get; set; } = null;
    public List<Quest.QuestID> ClearQuestsID { get; set; } = new List<Quest.QuestID>() { Quest.QuestID.M0 };
    public PlayableDirector CurDirector { get; set; } = null;
}