using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TaskGroupState
{
    Inactive,
    Running,
    Complete
}

[System.Serializable]
public class TaskGroup
{
    [SerializeField]
    private Task[] tasks;

    public IReadOnlyList<Task> Tasks => tasks;
    public Quest Owner { get; private set; }
    public bool IsAllTaskComplete => tasks.All(x => x.IsComplete);
    public bool IsComplete => State == TaskGroupState.Complete;
    public TaskGroupState State { get; private set; }

    /// <summary>
    /// Recreate the tasks in the TaskGroup, 
    /// generating new instances rather than referencing the originals.
    /// </summary>
    /// <param name="copyTarget"></param>
    public TaskGroup(TaskGroup copyTarget)
    {
        tasks = copyTarget.Tasks.Select(x => Object.Instantiate(x)).ToArray();
    }

    /// <summary>
    /// Setup all tasks
    /// </summary>
    /// <param name="owner"></param>
    public void Setup(Quest owner)
    {
        Owner = owner;
        foreach (var task in tasks)
            task.Setup(owner);
    }

    /// <summary>
    /// Start all tasks
    /// </summary>
    public void Start()
    {
        State = TaskGroupState.Running;
        foreach (var task in tasks)
            task.Start();
    }

    /// <summary>
    /// End all tasks
    /// </summary>
    public void End()
    {
        State = TaskGroupState.Complete;
        foreach (var task in tasks)
            task.End();
    }

    /// <summary>
    /// Perform `ReceiveReport` for tasks with the appropriate category and target.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="target"></param>
    /// <param name="conditionCount"></param>
    public void ReceiveReport(string category, object target, int conditionCount)
    {
        foreach (var task in  tasks)
        {
            if (task.IsTarget(category, target))
                task.ReceiveReport(conditionCount);
        }
    }

    public bool IsTarget(string category, object target) {
        foreach (var task in tasks) {
            return task.IsTarget(category, target);
        }
        return false;
    }

    /// <summary>
    /// Check all tasks completed 
    /// </summary>
    public void Complete()
    {
        if (IsComplete)
            return;

        State = TaskGroupState.Complete;

        foreach (var task in tasks)
        {
            if (!task.IsComplete)
                task.Complete();
        }
    }

    public Task FindTaskByTarget(object target) => tasks.FirstOrDefault(x => x.ContainsTarget(target));
    public Task FindTaskByTarget(TaskTarget target) => FindTaskByTarget(target.Value);
    public bool ContainsTarget(object target) => tasks.Any(x => x.ContainsTarget(target));
    public bool ContainsTarget(TaskTarget target) => ContainsTarget(target.Value);
}
