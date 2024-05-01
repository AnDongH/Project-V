using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestTrackerUI : ObjectUI
{
    enum TMPs {
        QuestTitleText
    }

    [SerializeField] private TaskDescriptorUI taskDescriptorPrefab;

    private Dictionary<Task, TaskDescriptorUI> taskDesriptorsByTask = new Dictionary<Task, TaskDescriptorUI>();

    private Quest targetQuest;

    private void Awake() {
        Bind<TextMeshProUGUI>(typeof(TMPs));
    }

    private void OnDestroy()
    {
        if (targetQuest != null)
        {
            targetQuest.OnNewTaskGroup -= UpdateTaskDescriptos;
            targetQuest.OnCompleted -= DestroySelf;
        }

        foreach (var tuple in taskDesriptorsByTask)
        {
            var task = tuple.Key;
            task.OnConditionChanged -= UpdateText;
        }
    }

    public void Setup(Quest targetQuest, Color titleColor)
    {

        this.targetQuest = targetQuest;

        GetTMP((int)TMPs.QuestTitleText).text = targetQuest.Category == null ?
            targetQuest.DisplayName :
            $"[{targetQuest.Category.DisplayName}] {targetQuest.DisplayName}";

        GetTMP((int)TMPs.QuestTitleText).color = titleColor;

        targetQuest.OnNewTaskGroup += UpdateTaskDescriptos;
        targetQuest.OnCompleted += DestroySelf;

        var taskGroups = targetQuest.TaskGroups;
        UpdateTaskDescriptos(targetQuest, taskGroups[0]); 

        if (taskGroups[0] != targetQuest.CurrentTaskGroup)
        {
            for (int i = 1; i < taskGroups.Count; i++)
            {
                var taskGroup = taskGroups[i];
                UpdateTaskDescriptos(targetQuest, taskGroup, taskGroups[i - 1]);

                if (taskGroup == targetQuest.CurrentTaskGroup)
                    break;
            }
        }
    }

    private void UpdateTaskDescriptos(Quest quest, TaskGroup currentTaskGroup, TaskGroup prevTaskGroup = null)
    {
        foreach (var task in currentTaskGroup.Tasks)
        {
            var taskDesriptor = Instantiate(taskDescriptorPrefab, transform);
            taskDesriptor.UpdateText(task);
            task.OnConditionChanged += UpdateText;

            taskDesriptorsByTask.Add(task, taskDesriptor);
        }

        if (prevTaskGroup != null)
        {
            foreach (var task in prevTaskGroup.Tasks)
            {
                var taskDesriptor = taskDesriptorsByTask[task];
                taskDesriptor.UpdateTextUsingStrikeThrough(task);
            }
        }
    }

    private void UpdateText(Task task, int currentCondition, int prevCondition)
    {
        taskDesriptorsByTask[task].UpdateText(task);
    }

    private void DestroySelf(Quest quest)
    {
        Destroy(gameObject);
    }
}
