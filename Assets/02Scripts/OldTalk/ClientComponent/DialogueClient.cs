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
        Access.TalkM.StopTalk();
    }
}

[System.Serializable]
public class NormalClient : DialogueClient {

}

[System.Serializable]
public class QuestClient : DialogueClient {
    [field: SerializeField, Tooltip("줄 퀘스트 ID")] public OldQuest.QuestID QuestID { get; private set; }

    public OldQuest Quest { get; protected set; }

    public override void ClientInit() {
        Quest = Access.TalkM.QuestDataBase.quests.Find(x => x.questID == QuestID);
        Quest.conditionCnt = new int[Quest.clearConditions.Count];
    }
}

[System.Serializable]
public class QuestGetClient : QuestClient {

    public override void ClientInit() {
        base.ClientInit();
    }

    public override void OnTalkEnd() {
        Access.TalkM.QuestProvide(Quest);
        Access.TalkM.StopTalk();
    }
}

[System.Serializable]
public class QuestProgressClient : QuestClient {

    [field: SerializeField, Tooltip("조건 ID")] public OldCondition.ConditionID ConditionID { get; private set; }
    [field: SerializeField, Tooltip("연결된 퀘스트 ID")] public OldQuest.QuestID LinkedQuestID { get; private set; }
    [field: SerializeField, Tooltip("퀘스트를 추가로 주고싶다면?")] public bool IsQuest { get; private set; }

    public override void ClientInit() {
        if (IsQuest)
            base.ClientInit();
    }

    public override void OnTalkEnd() {
        Access.TalkM.UpdateCondition(ConditionID, LinkedQuestID);
        Access.TalkM.ClearCheck(LinkedQuestID);
        if (IsQuest) Access.TalkM.QuestProvide(Quest);
        base.OnTalkEnd();
    }
}

[System.Serializable]
public class Dialogue {
    [field: SerializeField] public string Side { get; private set; }
    [field: SerializeField, TextArea] public string TalkText { get; private set; }
}