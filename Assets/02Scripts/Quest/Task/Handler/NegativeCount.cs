using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Handller/NegativeCount", fileName = "NegativeCount")]
public class NegativeCount : TaskHandler
{
    public override int Activate(Task task, int currentCondition, int conditionCount)
    {
        return conditionCount < 0 ? currentCondition - conditionCount : currentCondition;
    }
}