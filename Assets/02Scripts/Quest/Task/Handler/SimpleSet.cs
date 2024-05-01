using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Handller/SimpleSet", fileName = "Simple Set")]
public class SimpleSet : TaskHandler
{
    public override int Activate(Task task, int currentCondition, int conditionCount)
    {
        return conditionCount;
    }
}
