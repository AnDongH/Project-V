using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestViewUI : PopUpUI
{
    [SerializeField] private QuestListViewControllerUI questListViewController;
    [SerializeField] private QuestDetailViewUI questDetailView;

    protected override void Init() {
        base.Init();
    }

    private void Start()
    {
        Init();

        var questSystem = Access.QuestM;

        foreach (var quest in questSystem.ActiveQuests)
            AddQuestToActiveListView(quest);

        foreach (var quest in questSystem.CompletedQuests)
            AddQuestToCompletedListView(quest);

        questSystem.OnQuestRegisteredHandler += AddQuestToActiveListView;
        questSystem.OnQuestCompletedHandler += RemoveQuestFromActiveListView;
        questSystem.OnQuestCompletedHandler += AddQuestToCompletedListView;
        questSystem.OnQuestCompletedHandler += HideDetailIfQuestCanceled;
        questSystem.OnQuestCanceledHandler += HideDetailIfQuestCanceled;
        questSystem.OnQuestCanceledHandler += RemoveQuestFromActiveListView;

        foreach (var tab in questListViewController.Tabs)
            tab.onValueChanged.AddListener(HideDetail);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        var questSystem = Access.QuestM;
        if (questSystem)
        {
            questSystem.OnQuestRegisteredHandler -= AddQuestToActiveListView;
            questSystem.OnQuestCompletedHandler -= RemoveQuestFromActiveListView;
            questSystem.OnQuestCompletedHandler -= AddQuestToCompletedListView;
            questSystem.OnQuestCompletedHandler -= HideDetailIfQuestCanceled;
            questSystem.OnQuestCanceledHandler -= HideDetailIfQuestCanceled;
            questSystem.OnQuestCanceledHandler -= RemoveQuestFromActiveListView;
        }
    }

    private void OnEnable()
    {
        if (questDetailView.Target != null) questDetailView.Show(questDetailView.Target);
    }

    private void ShowDetail(bool isOn, Quest quest)
    {
        if (isOn) questDetailView.Show(quest);
    }

    private void HideDetail(bool isOn)
    {
        questDetailView.Hide();
    }

    private void AddQuestToActiveListView(Quest quest)
        => questListViewController.AddQuestToActiveListView(quest, isOn => ShowDetail(isOn, quest));

    private void AddQuestToCompletedListView(Quest quest)
        => questListViewController.AddQuestToCompletedListView(quest, isOn => ShowDetail(isOn, quest));

    private void HideDetailIfQuestCanceled(Quest quest)
    {
        if (questDetailView.Target == quest) questDetailView.Hide();
    }

    private void RemoveQuestFromActiveListView(Quest quest)
    {
        questListViewController.RemoveQuestFromActiveListView(quest);
        if (questDetailView.Target == quest) questDetailView.Hide();
    }
}
