using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueProvider: MonoBehaviour {

    [field: SerializeField, Header("�Ϲ� ��ȭ Ŭ���̾�Ʈ")]
    public NormalClient m_NormalClient { get; set; }

    [field: SerializeField, Header("����Ʈ ���� ��ȭ Ŭ���̾�Ʈ")]
    public List<QuestGetClient> m_QuestGetClients { get; set; }

    [field: SerializeField, Header("����Ʈ ���� ��ȭ Ŭ���̾�Ʈ")]
    public List<QuestProgressClient> m_QuestProgressClients { get; set; }

    private List<DialogueClient> curClients = new List<DialogueClient>(4);

    private void Start() {
        ClientInit();
    }

    private void ClientInit() {

        m_NormalClient.ClientInit();

        for (int i = 0; i < m_QuestGetClients.Count; i++) {
            m_QuestGetClients[i].ClientInit();
        }
        for (int i = 0; i < m_QuestProgressClients.Count; i++) {
            m_QuestProgressClients[i].ClientInit();
        }
       
    }

    private void GetAvailableTalks() {
       curClients.Clear();
     
      Player_DInfo info = Access.Player.P_DInfo;
      Quest quest = info.CurQuest;
     
      // �Ϲ� ��ȭ �߰�
      curClients.Add(m_NormalClient);
     
      // ������ ����Ʈ ���� ��ȭ �߰�
      curClients.AddRange(m_QuestGetClients.FindAll(x => info.ClearQuestsID.Contains(x.quest.previousQuestID) &&
                                                         !info.ClearQuestsID.Contains(x.questID) && 
                                                         x.questID != quest?.questID));
     
      // ������ ����Ʈ ���� ��ȭ �߰�
      curClients.AddRange(m_QuestProgressClients.FindAll(x => quest != null && 
                                                              x.linkedID == quest.questID && 
                                                              quest.conditionCnt[quest.clearConditions.FindIndex(y => y.conditionID == x.conditionID)]!= -1));
    }

    public void StartTalk() {
        GetAvailableTalks();
        if (curClients.Count >= 2)
            Access.Talk.StartTalk(curClients);
        else
            Access.Talk.StartTalk(m_NormalClient);
    }

}
