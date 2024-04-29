using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine.EventSystems;

public class TalkManager : DontDestroySingleton<TalkManager> {

    [Header("��ȭ UIM")]
    [SerializeField] private GameObject talkPanel;
    [SerializeField] private GameObject selectBtnPrefab;

    [field: SerializeField, Header("����Ʈ ����Ÿ���̽�")]
    public OldQuestData QuestDataBase { get; private set; }

    // UI
    private TextMeshProUGUI talkText;
    private GameObject nextText;
    private Image npcUI;
    private Image playerUI;
    private GameObject selectBtnUI;
    private List<GameObject> btnList = new List<GameObject>(5);
    public EventTrigger PanelTrigger { get; set; }

    // ��ȭ ����
    private DialogueClient curClient = null;
    private int curTalkIndex = 0;
    private int maxTalkIndex = 0;
    private bool charEffecting;
    private int curCharIdx = 0;
    private Animator talkUIAnimator;

    // ��ȭ������?
    public bool IsTalking { get; set; }


    protected override void Awake() {

        base.Awake();
        // UI �ʱ�ȭ
     // talkText = talkPanel.transform.Find("TalkText").gameObject.GetComponent<TextMeshProUGUI>();
     // nextText = talkPanel.transform.Find("NextText").gameObject;
     // npcUI = talkPanel.transform.Find("NPC UI").gameObject.GetComponent<Image>();
     // playerUI = talkPanel.transform.Find("Player UI").gameObject.GetComponent<Image>();
     // selectBtnUI = talkPanel.transform.Find("BtnGRP").gameObject;
     //
     // // UI Animator �ʱ�ȭ
     // talkUIAnimator = talkPanel.GetComponent<Animator>();
     // playerUIAnimator = playerUI.gameObject.GetComponent<Animator>();
     // npcUIAnimator = npcUI.gameObject.GetComponent<Animator>();
     //
     // PanelTrigger = talkPanel.GetComponent <EventTrigger>();
    }

    /// <summary>
    /// ��ȭ ��ó�� -> ��ȭ�� �������϶�
    /// </summary>
    /// <param name="isCut"></param>
    /// <param name="client"></param>
    public void StartTalk(List<DialogueClient> clients) {

        if (clients == null) {
            return;
        }

        IsTalking = true;
        nextText.SetActive(false);
        npcUI.gameObject.SetActive(false);
        selectBtnUI.SetActive(true);
        PanelTrigger.enabled = false;
        playerUI.color = Color.white;
        talkText.text = "������ �����?";
        for (int i = 0; i < clients.Count; i++) {
            int idx = i;
            GameObject g = Instantiate(selectBtnPrefab, selectBtnUI.transform);
            btnList.Add(g);
            g.GetComponent<Button>().onClick.AddListener(() => SelectTalk(idx, clients));
            g.GetComponentInChildren<TextMeshProUGUI>().text = clients[idx].TalkName;
        }

        talkUIAnimator.SetTrigger("On");
    }

    /// <summary>
    /// ��ȭ ��ó�� -> ��ȭ�� �ϳ��϶�
    /// </summary>
    /// <param name="client"></param>
    public void StartTalk(DialogueClient client) {

        if (client == null) {
            return;
        }

        curClient = client;
        IsTalking = true;
        selectBtnUI.SetActive(false);
        maxTalkIndex = curClient.Datas.Count;
        nextText.SetActive(true);
        talkUIAnimator.SetTrigger("On");

        NextTalk();
    }

    /// <summary>
    /// ��ư �̺�Ʈ -> ��ȭ ����
    /// </summary>
    /// <param name="i"></param>
    /// <param name="clients"></param>
    private void SelectTalk(int i, List<DialogueClient> clients) {
        curClient = clients[i];


        foreach (GameObject g in btnList) Destroy(g);
        btnList.Clear();
        selectBtnUI.SetActive(false);

        maxTalkIndex = curClient.Datas.Count;

        nextText.SetActive(false);

        PanelTrigger.enabled = true;

        NextTalk();
    }

    /// <summary>
    /// ���� ��ȭ �ִ��� Ȯ��
    /// </summary>
    /// <param name="isCut"></param>
    public void NextTalk() {

        if (curClient == null) return;


        if (charEffecting) {
            TypeEffectEnd();
            return;
        }

       if (curTalkIndex == maxTalkIndex) {
         // if (Access.Cut.IsCutPlaying) {
         //     Access.Cut.ResumeTimeline();
         // }
           curClient.OnTalkEnd();
           return;
       }

        TalkSet();
    }

    /// <summary>
    /// ��ȭ ��ó��
    /// </summary>
    public void StopTalk() {
        curTalkIndex = 0;
        maxTalkIndex = 0;
        IsTalking = false;
        curClient = null;
        talkUIAnimator.SetTrigger("Off");
        PanelTrigger.enabled = true;
    }

    /// <summary>
    /// ��ȭ ����
    /// </summary>
    private void TalkSet() {
        // ���� ��ȭ �ҷ�����
        curCharIdx = 0;
        talkText.text = "";
        charEffecting = true;

        TypeEffectStart();
    }

    /// <summary>
    /// ��ȭ ����Ʈ ����
    /// </summary>
    private void TypeEffectStart() {
        if (curCharIdx == curClient.Datas[curTalkIndex].TalkText.Length) {
            charEffecting = false;
            curTalkIndex++;
            return;
        }

        talkText.text += curClient.Datas[curTalkIndex].TalkText[curCharIdx++];

        Invoke("TypeEffectStart", 0.05f);
    }

    /// <summary>
    /// ��ȭ ����Ʈ ����
    /// </summary>
    private void TypeEffectEnd() {
        CancelInvoke("TypeEffectStart");
        curCharIdx = curClient.Datas[curTalkIndex].TalkText.Length;
        talkText.text = curClient.Datas[curTalkIndex].TalkText;
        charEffecting = false;
        curTalkIndex++;
    }

    /// <summary>
    /// ����Ʈ ����
    /// </summary>
    /// <param name="questID"></param>
    public void QuestProvide(OldQuest quest) {

       if (quest == null) {
           Debug.Log("����Ʈ�� �������� �ʽ��ϴ�.");
           return;
       }

       if (Access.Player.P_DInfo.CurQuest == null && !Access.Player.P_DInfo.ClearQuestsID.Contains(quest.questID)) {
            Access.Player.P_DInfo.CurQuest = quest;
            Debug.Log("����Ʈ �Ҵ�");
       }
       else {
            Debug.Log("�̹� �Ҵ�� ����Ʈ");

            foreach (OldQuest.QuestID q in Access.Player.P_DInfo.ClearQuestsID)
                Debug.Log(q);
            Debug.Log(quest.questID);


       }
    }

    public void SelectBtnSet(OldQuest quest) {

        selectBtnUI.SetActive(true);
        npcUI.gameObject.SetActive(false);
        nextText.SetActive(false);

        GameObject g1 = Instantiate(selectBtnPrefab, selectBtnUI.transform);
        GameObject g2 = Instantiate(selectBtnPrefab, selectBtnUI.transform);
        btnList.Add(g1);
        btnList.Add(g2);
        g1.GetComponent<Button>().onClick.AddListener(() => YesOrNo(false));
        g1.GetComponentInChildren<TextMeshProUGUI>().text = "�ƴϿ�";
        g2.GetComponent<Button>().onClick.AddListener(() => YesOrNo(true, quest));
        g2.GetComponentInChildren<TextMeshProUGUI>().text = "��";

        talkText.text = "����Ʈ�� �����ðڽ��ϱ�?";
    }

    private void YesOrNo(bool flag, OldQuest quest = null) {
        if (flag) QuestProvide(quest);
        foreach (GameObject g in btnList) Destroy(g);
        btnList.Clear();
        selectBtnUI.SetActive(false);
        StopTalk();
    }

    /// <summary>
    /// ����Ʈ ���� ������Ʈ
    /// </summary>
    /// <param name="conditionID"></param>
    /// <param name="questID"></param>
    public void UpdateCondition(OldCondition.ConditionID conditionID, OldQuest.QuestID questID) {

       OldQuest quest;

       quest = Access.Player.P_DInfo.CurQuest;

       int i = 0;
       i = quest.clearConditions.FindIndex(x => x.conditionID == conditionID);
    
       if (quest.useOrder && i == Access.Player.P_DInfo.CurQuest.conditionIdx) {
           quest.conditionIdx++;
       }
       if (quest.conditionCnt[i] != -1) {
           quest.conditionCnt[i]++;
           if (quest.conditionCnt[i] == quest.clearConditions[i].cnt)
               quest.conditionCnt[i] = -1;
       }
    }

    /// <summary>
    /// ����Ʈ Ŭ���� üũ
    /// </summary>
    public void ClearCheck(OldQuest.QuestID questID) {

       OldQuest quest;

       quest = Access.Player.P_DInfo.CurQuest;


       if (quest == null) return;


       for (int i = 0; i < quest.clearConditions.Count; i++) {
           if (quest.conditionCnt[i] != -1) {
               return;
           }
       }
     
       Debug.Log("����Ʈ Ŭ����");
       QuestClear(questID);
    }

    /// <summary>
    /// ����Ʈ Ŭ����
    /// </summary>
    /// <param name="questID"></param>
    public void QuestClear(OldQuest.QuestID questID) {
        Access.Player.P_DInfo.CurQuest = null;
        Access.Player.P_DInfo.ClearQuestsID.Add(questID);
    }
}

