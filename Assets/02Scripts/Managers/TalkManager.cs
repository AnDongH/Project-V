using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine.EventSystems;

public class TalkManager : MonoBehaviour {

    [Header("대화 UI")]
    [SerializeField] private GameObject talkPanel;
    [SerializeField] private GameObject selectBtnPrefab;

    [field: SerializeField, Header("퀘스트 데이타베이스")]
    public QuestData QuestDataBase { get; private set; }

    // UI
    private TextMeshProUGUI talkText;
    private GameObject nextText;
    private Image npcUI;
    private Image playerUI;
    private GameObject selectBtnUI;
    private List<GameObject> btnList = new List<GameObject>(5);
    public EventTrigger PanelTrigger { get; set; }

    // 대화 정보
    private DialogueClient curClient = null;
    private int curTalkIndex = 0;
    private int maxTalkIndex = 0;
    private bool charEffecting;
    private int curCharIdx = 0;
    private Animator talkUIAnimator;
    private Animator playerUIAnimator;
    private Animator npcUIAnimator;

    // 대화중인지?
    public bool IsTalking { get; set; }


    protected void Awake() {

        // UI 초기화
     // talkText = talkPanel.transform.Find("TalkText").gameObject.GetComponent<TextMeshProUGUI>();
     // nextText = talkPanel.transform.Find("NextText").gameObject;
     // npcUI = talkPanel.transform.Find("NPC UI").gameObject.GetComponent<Image>();
     // playerUI = talkPanel.transform.Find("Player UI").gameObject.GetComponent<Image>();
     // selectBtnUI = talkPanel.transform.Find("BtnGRP").gameObject;
     //
     // // UI Animator 초기화
     // talkUIAnimator = talkPanel.GetComponent<Animator>();
     // playerUIAnimator = playerUI.gameObject.GetComponent<Animator>();
     // npcUIAnimator = npcUI.gameObject.GetComponent<Animator>();
     //
     // PanelTrigger = talkPanel.GetComponent <EventTrigger>();
    }

    /// <summary>
    /// 대화 선처리 -> 대화가 여러개일때
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
        talkText.text = "무엇을 물어볼까?";
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
    /// 대화 선처리 -> 대화가 하나일때
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
        npcUI.gameObject.SetActive(client.N_DefaultImg != null);
        npcUI.sprite = client.N_DefaultImg;
        talkUIAnimator.SetTrigger("On");

        NextTalk();
    }

    /// <summary>
    /// 버튼 이벤트 -> 대화 선택
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

        npcUI.gameObject.SetActive(curClient.N_DefaultImg != null);
        npcUI.sprite = curClient.N_DefaultImg;
        PanelTrigger.enabled = true;

        NextTalk();
    }

    /// <summary>
    /// 다음 대화 있는지 확인
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
    /// 대화 후처리
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
    /// 대화 세팅
    /// </summary>
    private void TalkSet() {
        // 현재 대화 불러오기
        curCharIdx = 0;
        talkText.text = "";
        charEffecting = true;
        
        // side 판단해서 이미지 불러오기
        if (curClient.Datas[curTalkIndex].side == "Player") {
            playerUI.sprite = curClient.Datas[curTalkIndex].portrait;
            playerUI.color = Color.white;

            if (npcUI != null) npcUI.color = Color.gray;

            playerUIAnimator.SetTrigger("Action");
        }
        else {
            if (curClient.Datas[curTalkIndex].portrait != null) {
                npcUI.sprite = curClient.Datas[curTalkIndex].portrait;
                npcUI.color = Color.white;
                npcUIAnimator.SetTrigger("Action");
            }
            playerUI.color = Color.gray;
        }

        TypeEffectStart();
    }

    /// <summary>
    /// 대화 이펙트 시작
    /// </summary>
    private void TypeEffectStart() {
        if (curCharIdx == curClient.Datas[curTalkIndex].talkText.Length) {
            charEffecting = false;
            curTalkIndex++;
            return;
        }

        talkText.text += curClient.Datas[curTalkIndex].talkText[curCharIdx++];

        Invoke("TypeEffectStart", 0.05f);
    }

    /// <summary>
    /// 대화 이펙트 종료
    /// </summary>
    private void TypeEffectEnd() {
        CancelInvoke("TypeEffectStart");
        curCharIdx = curClient.Datas[curTalkIndex].talkText.Length;
        talkText.text = curClient.Datas[curTalkIndex].talkText;
        charEffecting = false;
        curTalkIndex++;
    }

    /// <summary>
    /// 퀘스트 제공
    /// </summary>
    /// <param name="questID"></param>
    public void QuestProvide(Quest quest) {

       if (quest == null) {
           Debug.Log("퀘스트가 존재하지 않습니다.");
           return;
       }

       if (Access.Player.P_DInfo.CurQuest == null && !Access.Player.P_DInfo.ClearQuestsID.Contains(quest.questID)) {
            Access.Player.P_DInfo.CurQuest = quest;
            Debug.Log("퀘스트 할당");
       }
       else {
            Debug.Log("이미 할당된 퀘스트");

            foreach (Quest.QuestID q in Access.Player.P_DInfo.ClearQuestsID)
                Debug.Log(q);
            Debug.Log(quest.questID);


       }
    }

    public void SelectBtnSet(Quest quest) {

        selectBtnUI.SetActive(true);
        npcUI.gameObject.SetActive(false);
        nextText.SetActive(false);

        GameObject g1 = Instantiate(selectBtnPrefab, selectBtnUI.transform);
        GameObject g2 = Instantiate(selectBtnPrefab, selectBtnUI.transform);
        btnList.Add(g1);
        btnList.Add(g2);
        g1.GetComponent<Button>().onClick.AddListener(() => YesOrNo(false));
        g1.GetComponentInChildren<TextMeshProUGUI>().text = "아니오";
        g2.GetComponent<Button>().onClick.AddListener(() => YesOrNo(true, quest));
        g2.GetComponentInChildren<TextMeshProUGUI>().text = "예";

        talkText.text = "퀘스트를 받으시겠습니까?";
    }

    private void YesOrNo(bool flag, Quest quest = null) {
        if (flag) QuestProvide(quest);
        foreach (GameObject g in btnList) Destroy(g);
        btnList.Clear();
        selectBtnUI.SetActive(false);
        StopTalk();
    }

    /// <summary>
    /// 퀘스트 조건 업데이트
    /// </summary>
    /// <param name="conditionID"></param>
    /// <param name="questID"></param>
    public void UpdateCondition(Condition.ConditionID conditionID, Quest.QuestID questID) {

       Quest quest;

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
    /// 퀘스트 클리어 체크
    /// </summary>
    public void ClearCheck(Quest.QuestID questID) {

       Quest quest;

       quest = Access.Player.P_DInfo.CurQuest;


       if (quest == null) return;


       for (int i = 0; i < quest.clearConditions.Count; i++) {
           if (quest.conditionCnt[i] != -1) {
               return;
           }
       }
     
       Debug.Log("퀘스트 클리어");
       QuestClear(questID);
    }

    /// <summary>
    /// 퀘스트 클리어
    /// </summary>
    /// <param name="questID"></param>
    public void QuestClear(Quest.QuestID questID) {
        Access.Player.P_DInfo.CurQuest = null;
        Access.Player.P_DInfo.ClearQuestsID.Add(questID);
    }
}

