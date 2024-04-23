using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Access {

    /// <summary>
    /// Global Player
    /// </summary>
    public static PlayerController Player { get; private set; } = PlayerController.Instance;
    
    /// <summary>
    /// TalkManager
    /// </summary>
    public static TalkManager Talk { get; private set; } = GameManager.Instance.GetComponent<TalkManager>();

    /// <summary>
    /// UIManager
    /// </summary>
    public static UI_Manager UI { get; private set; } = UI_Manager.Instance;

}
