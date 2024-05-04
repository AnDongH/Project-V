using System.Linq;
using UnityEngine;

public enum TaskState
{
    Inactive,
    Running,
    Complete
}

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    #region Events
    // By creating an event that triggers when a variable changes, you don't need to track it with Update.
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
    public delegate void ConditionChangedHandler(Task task, int currentCondition, int prevCondition);
    #endregion

    // Which actions will cause the condition to be handled
    [SerializeField] private Category category;

    [Header("Text")]
    // Task id
    [SerializeField] private string id;
    // Description of a Task in a quest.
    [SerializeField] private string description;

    [Header("Action")]
    // The method by which the condition is handled.
    [SerializeField] private TaskHandler handler;

    [Header("Target")]
    // The target can be varied. For example, in a quest to collect coins,
    // there might not be a distinction between silver and gold coins.
    [SerializeField] private TaskTarget[] targets;

    [Header("Setting")]
    // for example, if your initial level is 30 and goal level is 40 then initialConditionValue is 30
    // Initial Value of AcceptionCondition
    [SerializeField] private InitialConditionValue initialConditionValue;
    // Goal of AcceptionCondition
    [SerializeField] private int neededConditionToComplete;
    [SerializeField] private bool canReceiveReportsDuringCompletion;

    private TaskState state;
    private int currentCondition;

    public event StateChangedHandler OnStateChanged;
    public event ConditionChangedHandler OnConditionChanged;

    /// <summary>
    /// This property constrains the value of `currentCondition` within a specific range 
    /// and triggers a corresponding event when the value changes.
    /// </summary>
    public int CurrentCondition
    {
        get => currentCondition;
        set
        {
            int prevCondition = currentCondition;
            currentCondition = Mathf.Clamp(value, 0, neededConditionToComplete);
            if (currentCondition != prevCondition)
            {
                State = currentCondition == neededConditionToComplete ? TaskState.Complete : TaskState.Running;
                OnConditionChanged?.Invoke(this, currentCondition, prevCondition);
            }
        }
    }
    public Category Category => category;
    public string ID => id;
    public string Description => description;
    public int NeededConditionToComplete => neededConditionToComplete;
    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            if (state != prevState) OnStateChanged?.Invoke(this, state, prevState);
        }
    }
    public bool IsComplete => State == TaskState.Complete;
    public Quest Owner { get; private set; }

    public void Setup(Quest owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// Task Start
    /// </summary>
    public void Start()
    {
        State = TaskState.Running;
        if (initialConditionValue) CurrentCondition = initialConditionValue.GetValue(this);
    }

    /// <summary>
    /// Task End
    /// </summary>
    public void End()
    {
        OnStateChanged = null;
        OnConditionChanged = null;
    }

    /// <summary>
    /// Update the condition by triggering the specified handler.
    /// </summary>
    /// <param name="conditionCount"></param>
    public void ReceiveReport(int conditionCount)
    {
        CurrentCondition = handler.Activate(this, CurrentCondition, conditionCount);
    }

    /// <summary>
    /// Task Complete
    /// </summary>
    public void Complete()
    {
        CurrentCondition = neededConditionToComplete;
    }

    /// <summary>
    /// Check if the specified category and target are correct.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool IsTarget(string category, object target)
        => Category == category &&
        targets.Any(x => x.IsEqual(target)) &&
        (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion));

    public bool ContainsTarget(object target) => targets.Any(x => x.IsEqual(target));
}

