using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Handller/SimpleCount", fileName = "Simple Count")]
public class SimpleCount : TaskHandler
{
    public override int Activate(Task task, int currentCondition, int conditionCount)
    {
        return currentCondition + conditionCount;
    }
}
