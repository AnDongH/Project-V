using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueClient {

    [field: SerializeField, Header("대화 정보")]
    public string TalkName { get; private set; }
    [field: SerializeField] public List<Dialogue> Datas { get; private set; }

    public virtual void ClientInit() {

    }

    public virtual void OnTalkEnd() {
        Access.Talk.StopTalk();
    }
}

[System.Serializable]
public class NormalClient : DialogueClient {

}

[System.Serializable]
public class QuestClient : DialogueClient {
    [field: SerializeField, Tooltip("줄 퀘스트 ID")] public Quest.QuestID QuestID { get; private set; }

    public Quest Quest { get; protected set; }

    public override void ClientInit() {
        Quest = Access.Talk.QuestDataBase.quests.Find(x => x.questID == QuestID);
        Quest.conditionCnt = new int[Quest.clearConditions.Count];
    }
}

[System.Serializable]
public class QuestGetClient : QuestClient {

    public override void ClientInit() {
        base.ClientInit();
    }

    public override void OnTalkEnd() {
        Access.Talk.QuestProvide(Quest);
        Access.Talk.StopTalk();
    }
}

[System.Serializable]
public class QuestProgressClient : QuestClient {

    [field: SerializeField, Tooltip("조건 ID")] public Condition.ConditionID ConditionID { get; private set; }
    [field: SerializeField, Tooltip("연결된 퀘스트 ID")] public Quest.QuestID LinkedQuestID { get; private set; }
    [field: SerializeField, Tooltip("퀘스트를 추가로 주고싶다면?")] public bool IsQuest { get; private set; }

    public override void ClientInit() {
        if (IsQuest)
            base.ClientInit();
    }

    public override void OnTalkEnd() {
        Access.Talk.UpdateCondition(ConditionID, LinkedQuestID);
        Access.Talk.ClearCheck(LinkedQuestID);
        if (IsQuest) Access.Talk.QuestProvide(Quest);
        base.OnTalkEnd();
    }
}

[System.Serializable]
public class Dialogue {
    [field: SerializeField] public string Side { get; private set; }
    [field: SerializeField, TextArea] public string TalkText { get; private set; }
}