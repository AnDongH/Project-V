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
    /// TalkManager
    /// </summary>
    public static TalkManager Talk { get { return GameManager.Instance.GetComponent<TalkManager>(); } }

    /// <summary>
    /// UIManager
    /// </summary>
    public static UIManager UI { get { return UIManager.Instance; } }

}
