using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class QuestCompletionNotifierUI : PopUpUI
{
    enum TMPs {
        TitleText,
        RewardText
    }

    [SerializeField] private string titleDescription;
    [SerializeField] private float showTime = 3f;

    private Queue<Quest> reservedQuests = new Queue<Quest>();
    private StringBuilder stringBuilder = new StringBuilder();

    protected override void Init() {
        base.Init();
    }

    private void Start()
    {
        Init();

        Access.UIM.ShowPopupUI<PopUpUI>(name);

        var questSystem = Access.QuestM;
        questSystem.OnQuestCompletedHandler += Notify;
        questSystem.OnAchievementCompletedHandler += Notify;

        Bind<TextMeshProUGUI>(typeof(TMPs));

        ClosePopUpUI();
    }

    private void OnDestroy()
    {
        var questSysem = Access.QuestM;
        if (questSysem != null)
        {
            questSysem.OnQuestCompletedHandler -= Notify;
            questSysem.OnAchievementCompletedHandler -= Notify;
        }
    }

    private void Notify(Quest quest)
    {
        reservedQuests.Enqueue(quest);

        if (!gameObject.activeSelf)
        {
            Access.UIM.ShowPopupUI<PopUpUI>(name);
            StartCoroutine("ShowNotice");
        }
    }

    private IEnumerator ShowNotice()
    {
        var waitSeconds = new WaitForSeconds(showTime);

        Quest quest;
        while (reservedQuests.TryDequeue(out quest))
        {
            GetTMP((int)TMPs.TitleText).text = titleDescription.Replace("%{dn}", quest.DisplayName);
            foreach (var reward in quest.Rewards)
            {
                stringBuilder.Append(reward.Description);
                stringBuilder.Append(" ");
                stringBuilder.Append(reward.Quantity);
                stringBuilder.Append(" ");
            }
            GetTMP((int)TMPs.RewardText).text = stringBuilder.ToString();
            stringBuilder.Clear();

            yield return waitSeconds;
        }

        ClosePopUpUI();
    }
}
