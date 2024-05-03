using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskTargetMarker : MonoBehaviour
{
    [SerializeField]
    private TaskTarget target;
    [SerializeField]
    private MarkerMaterialData[] markerMaterialDatas;

    private Dictionary<Quest, Task> targetTasksByQuest = new Dictionary<Quest, Task>();
    private Renderer rend;

    private int currentRunningTargetTaskCount;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        gameObject.SetActive(false);

        Access.QuestM.OnQuestRegisteredHandler += TryAddTargetQuest;
        foreach (var quest in Access.QuestM.ActiveQuests)
            TryAddTargetQuest(quest);
    }

    private void OnDestroy()
    {
        if (Access.QuestM != null) Access.QuestM.OnQuestRegisteredHandler -= TryAddTargetQuest;

        foreach ((Quest quest, Task task) in targetTasksByQuest)
        {
            if (quest != null) {
                quest.OnNewTaskGroup -= UpdateTargetTask;
                quest.OnCompleted -= RemoveTargetQuest;
            }
            
            if (task != null) task.OnStateChanged -= UpdateRunningTargetTaskCount;
        }
    }

    private void TryAddTargetQuest(Quest quest)
    {
        if (target != null && quest.ContainsTarget(target))
        {
            quest.OnNewTaskGroup += UpdateTargetTask;
            quest.OnCompleted += RemoveTargetQuest;

            UpdateTargetTask(quest, quest.CurrentTaskGroup);
        }
    }

    private void UpdateTargetTask(Quest quest, TaskGroup currentTaskGroup, TaskGroup prevTaskGroup = null)
    {
        targetTasksByQuest.Remove(quest);

        var task = currentTaskGroup.FindTaskByTarget(target);
        if (task != null)
        {
            targetTasksByQuest[quest] = task;
            task.OnStateChanged += UpdateRunningTargetTaskCount;

            UpdateRunningTargetTaskCount(task, task.State);
        }
    }

    private void RemoveTargetQuest(Quest quest) => targetTasksByQuest.Remove(quest);

    // expected bug
    private void UpdateRunningTargetTaskCount(Task task, TaskState currentState, TaskState prevState = TaskState.Inactive)
    {
        if (currentState == TaskState.Running)
        {
            rend.material = markerMaterialDatas.First(x => x.category == task.Category).markerMaterial;
            currentRunningTargetTaskCount++;
        }
        else
            currentRunningTargetTaskCount--;

        gameObject.SetActive(currentRunningTargetTaskCount != 0);
    }

    [System.Serializable]
    private struct MarkerMaterialData
    {
        public Category category;
        public Material markerMaterial;
    }
}
