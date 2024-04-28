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

    [Tooltip("���� �̸�")] public string conditionName = "";
    [Tooltip("���� ID")] public ConditionID conditionID;
    public Sprite icon;
    [Tooltip("���� ����")] public string description;
    [Tooltip ("Ƚ��")] public int cnt;
}

[System.Serializable]
public class Quest {

    public enum QuestID {
        M0, M1, M2, M3, M4, M5
    }

    [Tooltip("���� ����Ʈ ID")] public QuestID previousQuestID;

    [Tooltip("����Ʈ �̸�")] public string questName;
    [Tooltip("����Ʈ ID")] public QuestID questID;
    [Tooltip("���� �˻縦 �������? or ��� ����")] public bool useOrder;
    [HideInInspector] public int conditionIdx;
    [Tooltip("Ŭ���� ���� ����")] public List<Condition> clearConditions;
    [HideInInspector] public int[] conditionCnt;
}