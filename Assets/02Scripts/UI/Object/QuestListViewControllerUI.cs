using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestListViewControllerUI : ObjectUI
{
    [SerializeField] private ToggleGroup tabGroup;
    [SerializeField] private QuestListViewUI activeQuestListView;
    [SerializeField] private QuestListViewUI completedQuestListView;

    public IEnumerable<Toggle> Tabs => tabGroup.ActiveToggles();

    public void AddQuestToActiveListView(Quest quest, UnityAction<bool> onClicked)
        => activeQuestListView.AddElement(quest, onClicked);

    public void RemoveQuestFromActiveListView(Quest quest)
        => activeQuestListView.RemoveElement(quest);

    public void AddQuestToCompletedListView(Quest quest, UnityAction<bool> onClicked)
        => completedQuestListView.AddElement(quest, onClicked);
}
