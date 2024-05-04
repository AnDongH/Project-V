using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    Inactive,
    Running,
    Complete,
    Cancel,
    WaitingForCompletion
}

[CreateAssetMenu(menuName = "Quest/Quest", fileName = "Quest_")]
public class Quest : ScriptableObject
{
    #region Events
    public delegate void TaskConditionChangedHandler(Quest quest, Task task, int currentCondition, int prevCondition);
    public delegate void CompletedHandler(Quest quest);
    public delegate void CanceledHandler(Quest quest);
    public delegate void NewTaskGroupHandler(Quest quest, TaskGroup currentTaskGroup, TaskGroup prevTaskGroup);
    #endregion

    [SerializeField] private Category category;
    [SerializeField] private Sprite icon;

    [Header("Text")]
    [SerializeField] private string id;
    [SerializeField] private string displayName;
    [SerializeField, TextArea] private string description;

    [Header("Task")]
    [SerializeField] private TaskGroup[] taskGroups;

    [Header("Reward")]
    [SerializeField] private Reward[] rewards;

    [Header("Option")]
    [SerializeField] private bool useAutoComplete;
    [SerializeField] private bool isCancelable;

    [Header("Condition")]
    [SerializeField] private QuestAcceptionCondition[] acceptionConditions;
    [SerializeField] private QuestAcceptionCondition[] cancelConditions;

    private int currentTaskGroupIndex;

    public Category Category => category;
    public Sprite Icon => icon;
    public string ID => id;
    public string DisplayName => displayName;
    public string Description => description;
    public QuestState State { get; private set; }
    public TaskGroup CurrentTaskGroup => taskGroups[currentTaskGroupIndex];
    public IReadOnlyList<TaskGroup> TaskGroups => taskGroups;
    public IReadOnlyList<Reward> Rewards => rewards;
    public bool IsRegistered => State != QuestState.Inactive;
    public bool IsComplatable => State == QuestState.WaitingForCompletion;
    public bool IsComplete => State == QuestState.Complete;
    public bool IsCancel => State == QuestState.Cancel;
    public virtual bool IsCancelable => isCancelable && cancelConditions.All(x => x.IsPass(this));
    public bool IsAcceptable => acceptionConditions.All(x => x.IsPass(this)) && !Access.QuestM.ContainsInCompleteQuests(this) && !Access.QuestM.ContainsInActiveQuests(this);

    public event TaskConditionChangedHandler OnTaskConditionChanged;
    public event CompletedHandler OnCompleted;
    public event CanceledHandler OnCanceled;
    public event NewTaskGroupHandler OnNewTaskGroup;

    /// <summary>
    /// The registration callback method that occurs when a quest is registered.
    /// </summary>
    public void OnRegister()
    {
        Debug.Assert(!IsRegistered, "This quest has already been registered.");

        foreach (var taskGroup in taskGroups)
        {
            taskGroup.Setup(this);
            foreach (var task in taskGroup.Tasks)
                task.OnConditionChanged += OnConditionChanged;
        }

        State = QuestState.Running;
        CurrentTaskGroup.Start();
    }

    /// <summary>
    /// To progress a quest, 
    /// check if the category of the task I set 
    /// matches the category of the given parameter, 
    /// and if the target of my task I set
    /// matches the parameter's target, 
    /// then update the values accordingly.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="target"></param>
    /// <param name="conditionCount"></param>
    public void ReceiveReport(string category, object target, int conditionCount)
    {
        // Error Report
        Debug.Assert(IsRegistered, "This quest has already been registered.");
        Debug.Assert(!IsCancel, "This quest has been canceled.");

        if (IsComplete) return;

        CurrentTaskGroup.ReceiveReport(category, target, conditionCount);

        if (CurrentTaskGroup.IsAllTaskComplete)
        {
            if (currentTaskGroupIndex + 1 == taskGroups.Length)
            {
                State = QuestState.WaitingForCompletion;
                if (useAutoComplete) Complete();
            }
            else
            {
                var prevTasKGroup = taskGroups[currentTaskGroupIndex++];
                prevTasKGroup.End();
                CurrentTaskGroup.Start();
                OnNewTaskGroup?.Invoke(this, CurrentTaskGroup, prevTasKGroup);
            }
        }
        else
            State = QuestState.Running;
    }

    public bool IsTarget(string category, object target) {
        // Error Report
        Debug.Assert(IsRegistered, "This quest has already been registered.");
        Debug.Assert(!IsCancel, "This quest has been canceled.");

        if (IsComplete) return false;

        return CurrentTaskGroup.IsTarget(category, target);
    }

    /// <summary>
    /// Complete the Quest
    /// </summary>
    public void Complete()
    {
        CheckIsRunning();

        foreach (var taskGroup in taskGroups)
            taskGroup.Complete();

        State = QuestState.Complete;

        foreach (var reward in rewards)
            reward.Give(this);

        OnCompleted?.Invoke(this);

        OnTaskConditionChanged = null;
        OnCompleted = null;
        OnCanceled = null;
        OnNewTaskGroup = null;
    }

    /// <summary>
    /// Cancel the Quest
    /// </summary>
    public virtual void Cancel()
    {
        CheckIsRunning();
        Debug.Assert(IsCancelable, "This quest can't be canceled");

        State = QuestState.Cancel;
        OnCanceled?.Invoke(this);
    }

    public bool ContainsTarget(object target) => taskGroups.Any(x => x.ContainsTarget(target));
    public bool ContainsTarget(TaskTarget target) => ContainsTarget(target.Value);

    /// <summary>
    /// Instantiate the quest object from the original scriptable object to use during runtime.
    /// </summary>
    /// <returns></returns>
    public Quest Clone()
    {
        var clone = Instantiate(this);
        clone.taskGroups = taskGroups.Select(x => new TaskGroup(x)).ToArray();

        return clone;
    }

    /// <summary>
    /// Condition changed callback
    /// </summary>
    /// <param name="task"></param>
    /// <param name="currentCondition"></param>
    /// <param name="prevCondition"></param>
    private void OnConditionChanged(Task task, int currentCondition, int prevCondition)
        => OnTaskConditionChanged?.Invoke(this, task, currentCondition, prevCondition);

    /// <summary>
    /// Quest Error Report
    /// </summary>
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void CheckIsRunning()
    {
        Debug.Assert(IsRegistered, "This quest has already been registered");
        Debug.Assert(!IsCancel, "This quest has been canceled.");
        Debug.Assert(!IsComplete, "This quest has already been completed");
    }
}
