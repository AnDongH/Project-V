using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueClient {

    [field: SerializeField, Header("대화 정보")]
    public string TalkName { get; private set; }
    [field: SerializeField] public Sprite P_DefaultImg { get; private set; }
    [field: SerializeField] public Sprite N_DefaultImg { get; private set; }

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
    [field: SerializeField, Tooltip("줄 퀘스트 ID")] public Quest.QuestID questID { get; private set; }

    public Quest quest { get; protected set; }

    public override void ClientInit() {
        quest = Access.Talk.QuestDataBase.quests.Find(x => x.questID == questID);
        quest.conditionCnt = new int[quest.clearConditions.Count];
    }
}

[System.Serializable]
public class QuestGetClient : QuestClient {

    public override void ClientInit() {
        base.ClientInit();
    }

    public override void OnTalkEnd() {
        if (System.Enum.GetName(typeof(Quest.QuestID), questID)[0] == 'M') {
            Access.Talk.QuestProvide(quest);
            Access.Talk.StopTalk();
        }
        else
            Access.Talk.SelectBtnSet(quest);
    }
}

[System.Serializable]
public class QuestProgressClient : QuestClient {
    [field: SerializeField, Tooltip("조건 ID")] public Condition.ConditionID conditionID { get; private set; }
    [field: SerializeField, Tooltip("연결된 퀘스트 ID")] public Quest.QuestID linkedID { get; private set; }
    [field: SerializeField, Tooltip("퀘스트를 추가로 주고싶다면?")] public bool isQuest { get; private set; }


    public override void ClientInit() {
        if (isQuest)
            base.ClientInit();
    }

    public override void OnTalkEnd() {
        Access.Talk.UpdateCondition(conditionID, linkedID);
        Access.Talk.ClearCheck(linkedID);
        if (isQuest) Access.Talk.QuestProvide(quest);
        base.OnTalkEnd();
    }
}

[System.Serializable]
public class Dialogue {
    [field: SerializeField] public string side { get; private set; }
    [field: SerializeField] public Sprite portrait { get; private set; }
    [field: SerializeField, TextArea] public string talkText { get; private set; }
}