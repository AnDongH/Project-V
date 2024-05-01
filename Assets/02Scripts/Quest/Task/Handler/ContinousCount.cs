using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Handller/ContinousCount", fileName = "ContinousCount")]
public class ContinousCount : TaskHandler
{
    public override int Activate(Task task, int currentCondition, int conditionCount)
    {
        return conditionCount > 0 ? currentCondition + conditionCount : 0;
    }
}
