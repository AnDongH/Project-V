using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This is a scriptable object that represents the target of a quest.
/// </summary>
public abstract class TaskTarget : ScriptableObject
{
    // The reason it is of type object is that the inherited class will determine the specific type.
    public abstract object Value { get; }

    /// <summary>
    /// Check whether the Value you set matches the target to be checked.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public abstract bool IsEqual(object target);
}
