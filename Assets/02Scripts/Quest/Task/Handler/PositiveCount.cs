using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Handller/PositiveCount", fileName = "Positive Count")]
public class PositiveCount : TaskHandler
{
    public override int Activate(Task task, int currentCondition, int conditionCount)
    {
        return conditionCount > 0 ? currentCondition + conditionCount : currentCondition;
    }
}
