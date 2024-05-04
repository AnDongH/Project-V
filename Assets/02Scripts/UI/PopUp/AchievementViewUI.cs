using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementViewUI : PopUpUI
{
    [SerializeField] private RectTransform achievementGroup;
    [SerializeField] private AchievementDetailViewUI achievementDetailViewPrefab;

    protected override void Init() {
        base.Init();
    }

    private void Start()
    {
        Init();

        var questSystem = Access.QuestM;
        CreateDetailViews(questSystem.ActiveAchievements);
        CreateDetailViews(questSystem.CompletedAchievements);

        gameObject.SetActive(false);
    }

    private void CreateDetailViews(IReadOnlyList<Quest> achievements)
    {
        foreach (var achievement in achievements)
            Instantiate(achievementDetailViewPrefab, achievementGroup).Setup(achievement);
    }
}
