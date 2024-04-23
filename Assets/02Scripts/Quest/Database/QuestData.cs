using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    [field: SerializeField]
    public List<Quest> quests { get; private set; } = new List<Quest>();
}

[System.Serializable]
public class Condition {

    public enum ConditionID {
        BOOL0, BOOL1, BOOL2, BOOL3, BOOL4, BOOL5,
        TALK0, TALK1, TALK2, TALK3, TALK4, TALK5
    }

    [Tooltip("조건 이름")] public string conditionName = "";
    [Tooltip("조건 ID")] public ConditionID conditionID;
    public Sprite icon;
    [Tooltip("조건 설명")] public string description;
    [Tooltip ("횟수")] public int cnt;
}

[System.Serializable]
public class Quest {

    public enum QuestID {
        M0, M1, M2, M3, M4, M5
    }

    [Tooltip("선행 퀘스트 ID")] public QuestID previousQuestID;

    [Tooltip("퀘스트 이름")] public string questName;
    [Tooltip("퀘스트 ID")] public QuestID questID;
    [Tooltip("조건 검사를 순서대로? or 상관 없음")] public bool useOrder;
    [HideInInspector] public int conditionIdx;
    [Tooltip("클리어 세부 조건")] public List<Condition> clearConditions;
    [HideInInspector] public int[] conditionCnt;
}