using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuestDetailViewUI : ObjectUI
{
    enum TMPs {
        QuestTitleText,
        QuestDescriptionText,
    }

    enum Buttons {
        CancelBtn
    }

    enum Rects {
        TaskDescriptorGroup,
        RewardGroup
    }

    enum Objects {
        DisplayGroup
    }

    [Header("Task Description")]
    [SerializeField] private TaskDescriptorUI taskDescriptorPrefab;
    [SerializeField] private int taskDescriptorPoolCount;

    [Header("Reward Description")]
    [SerializeField] private TextMeshProUGUI rewardDescriptionPrefab;
    [SerializeField] private int rewardDescriptionPoolCount;

    private List<TaskDescriptorUI> taskDescriptorPool;
    private List<TextMeshProUGUI> rewardDescriptionPool;

    public Quest Target { get; private set; }

    private void Awake() {
        Bind<TextMeshProUGUI>(typeof(TMPs));
        Bind<Button>(typeof(Buttons));
        Bind<RectTransform>(typeof(Rects));
        Bind<GameObject>(typeof(Objects));
    }

    private void Start()
    {
        taskDescriptorPool = CreatePool(taskDescriptorPrefab, taskDescriptorPoolCount, GetRect((int)Rects.TaskDescriptorGroup));
        rewardDescriptionPool = CreatePool(rewardDescriptionPrefab, rewardDescriptionPoolCount, GetRect((int)Rects.RewardGroup));
        GetObject((int)Objects.DisplayGroup).SetActive(false);
        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(CancelQuest);
    }

    private List<T> CreatePool<T>(T prefab, int count, RectTransform parent)
        where T : MonoBehaviour
    {
        var pool = new List<T>(count);
        for (int i = 0; i < count; i++)
            pool.Add(Instantiate(prefab, parent));
        return pool;
    }

    private void CancelQuest(PointerEventData eventData)
    {
        if (Target.IsCancelable) Target.Cancel();
    }

    public void Show(Quest quest)
    {
        GetObject((int)Objects.DisplayGroup).SetActive(true);
        Target = quest;

        GetTMP((int)TMPs.QuestTitleText).text = quest.DisplayName;
        GetTMP((int)TMPs.QuestDescriptionText).text = quest.Description;

        int taskIndex = 0;
        foreach (var taskGroup in quest.TaskGroups)
        {
            foreach (var task in taskGroup.Tasks)
            {
                var poolObject = taskDescriptorPool[taskIndex++];
                poolObject.gameObject.SetActive(true);

                if (taskGroup.IsComplete)
                    poolObject.UpdateTextUsingStrikeThrough(task);
                else if (taskGroup == quest.CurrentTaskGroup)
                    poolObject.UpdateText(task);
                else
                    poolObject.UpdateText("¡Ü ??????????");
            }
        }

        for (int i = taskIndex; i < taskDescriptorPool.Count; i++)
            taskDescriptorPool[i].gameObject.SetActive(false);

        var rewards = quest.Rewards;
        var rewardCount = rewards.Count;
        for (int i = 0; i < rewardDescriptionPoolCount; i++)
        {
            var poolObject = rewardDescriptionPool[i];
            if (i < rewardCount)
            {
                var reward = rewards[i];
                poolObject.text = $"¡Ü {reward.Description} +{reward.Quantity}";
                poolObject.gameObject.SetActive(true);
            }
            else
                poolObject.gameObject.SetActive(false);
        }

        GetButton((int)Buttons.CancelBtn).gameObject.SetActive(quest.IsCancelable && !quest.IsComplete);
    }

    public void Hide()
    {
        Target = null;
        GetObject((int)Objects.DisplayGroup).SetActive(false);
        GetButton((int)Buttons.CancelBtn).gameObject.SetActive(false);
    }
}
