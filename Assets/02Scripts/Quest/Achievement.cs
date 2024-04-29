using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is a quest that represents achievements such as titles, 
/// and it cannot be canceled.
/// </summary>
[CreateAssetMenu(menuName = "Quest/Achievement", fileName = "Achievement_")]
public class Achievement : Quest
{
    public override bool IsCancelable => false;

    public override void Cancel()
    {
        Debug.LogAssertion("Achievement can't be canceled");
    }
}
