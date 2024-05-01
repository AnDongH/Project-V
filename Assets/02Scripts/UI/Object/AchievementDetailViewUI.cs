using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDetailViewUI : ObjectUI
{
    enum TMPs {
        TitleText,
        DescriptionText,
        RewardText
    }

    enum Images {
        AchievementIcon,
        RewardIcon
    }

    enum Objects {
        CompletionScreen
    }

    private Quest target;

    private void Awake() {

        Bind<TextMeshProUGUI>(typeof(TMPs));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(Objects));

    }

    private void OnDestroy()
    {
        if (target != null)
        {
            target.OnTaskConditionChanged -= UpdateDescription;
            target.OnCompleted -= ShowCompletionScreen;
        }
    }

    public void Setup(Quest achievement)
    {
        target = achievement;

        GetImage((int)Images.AchievementIcon).sprite = achievement.Icon;
        GetTMP((int)TMPs.TitleText).text = achievement.DisplayName;

        var task = achievement.CurrentTaskGroup.Tasks[0];
        GetTMP((int)TMPs.DescriptionText).text = BuildTaskDescription(task);

        var reward = achievement.Rewards[0];
        GetImage((int)Images.RewardIcon).sprite = reward.Icon;
        GetTMP((int)TMPs.RewardText).text = $"{reward.Description} +{reward.Quantity}";

        if (achievement.IsComplete)
            GetObject((int)Objects.CompletionScreen).SetActive(true);
        else
        {
            GetObject((int)Objects.CompletionScreen).SetActive(false);
            achievement.OnTaskConditionChanged += UpdateDescription;
            achievement.OnCompleted += ShowCompletionScreen;
        }
    }

    private void UpdateDescription(Quest achievement, Task task, int currentSuccess, int prevSuccess)
        => GetTMP((int)TMPs.DescriptionText).text = BuildTaskDescription(task);

    private void ShowCompletionScreen(Quest achievement)
        => GetObject((int)Objects.CompletionScreen).SetActive(true);

    private string BuildTaskDescription(Task task) => $"{task.Description} {task.CurrentCondition}/{task.NeededConditionToComplete}";
}
