using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestAcceptionCondition : ScriptableObject
{
    [SerializeField] private string description;

    /// <summary>
    /// Prerequisites for starting or canceling a quest.
    /// Quest parameter indicates which quest is granting the condition.
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public abstract bool IsPass(Quest quest);
}
