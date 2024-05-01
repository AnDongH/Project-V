using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InitialConditionValue : ScriptableObject
{
    /// <summary>
    /// The initial value for the condition to clear a task.
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public abstract int GetValue(Task task);
}
