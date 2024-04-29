using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskHandler : ScriptableObject
{
    /// <summary>
    /// An abstract method that determines how to handle a condition.
    /// </summary>
    /// <param name="task"></param>
    /// <param name="currentCondition"></param>
    /// <param name="conditionCount"></param>
    /// <returns></returns>
    public abstract int Activate(Task task, int currentCondition, int conditionCount);
}
