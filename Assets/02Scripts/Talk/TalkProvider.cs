using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkProvider: MonoBehaviour {

    [field: SerializeField, Header("�Ϲ� ��ȭ Ŭ���̾�Ʈ")]
    public NormalClient NormalTalk { get; set; }

    [field: SerializeField, Header("����Ʈ ���� ��ȭ Ŭ���̾�Ʈ")]
    public List<QuestGetClient> QuestGetTalks { get; set; }

    [field: SerializeField, Header("����Ʈ ���� ��ȭ Ŭ���̾�Ʈ")]
    public List<QuestProgressClient> QuestProgressTalks { get; set; }

    private void Start() {
        ClientInit();
    }

    private void ClientInit() {

        NormalTalk.ClientInit();

        for (int i = 0; i < QuestGetTalks.Count; i++) {
            QuestGetTalks[i].ClientInit();
        }
        for (int i = 0; i < QuestProgressTalks.Count; i++) {
            QuestProgressTalks[i].ClientInit();
        }
       
    }

    private List<DialogueClient> GetAvailableTalks() {

        List<DialogueClient> curClients = new();
        PlayerDInfo info = Access.Player.P_DInfo;
        Quest quest = info.CurQuest;
     
        // �Ϲ� ��ȭ �߰�
        curClients.Add(NormalTalk);
     
        // ������ ����Ʈ ���� ��ȭ �߰�
        curClients.AddRange(QuestGetTalks.FindAll(x => info.ClearQuestsID.Contains(x.Quest.previousQuestID) &&
                                                         !info.ClearQuestsID.Contains(x.QuestID) && 
                                                         x.QuestID != quest?.questID));
     
        // ������ ����Ʈ ���� ��ȭ �߰�
        curClients.AddRange(QuestProgressTalks.FindAll(x => quest != null && 
                                                              x.LinkedQuestID == quest.questID && 
                                                              quest.conditionCnt[quest.clearConditions.FindIndex(y => y.conditionID == x.ConditionID)]!= -1));
        
        return curClients;
    }

    public void StartTalk() {

        List<DialogueClient> curClients = GetAvailableTalks();

        if (curClients.Count >= 2)
            Access.Talk.StartTalk(curClients);
        else
            Access.Talk.StartTalk(NormalTalk);

    }

}
