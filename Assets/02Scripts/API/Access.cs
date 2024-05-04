using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// You can use this class to reference objects that are configured as singletons.
/// </summary>
public static class Access {

    /// <summary>
    /// Global Player
    /// </summary>
    public static PlayerController Player { get { return PlayerController.Instance; } }

    /// <summary>
    /// UIManager
    /// </summary>
    public static UIManager UIM { get { return UIManager.Instance; } }

    /// <summary>
    /// QuestManager
    /// </summary>
    public static QuestManager QuestM { get { return QuestManager.Instance; } }

    public static DialogueManager DIalogueM { get { return DialogueManager.Instance; } }
}
