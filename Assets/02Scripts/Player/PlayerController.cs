using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerController : DontDestroySingleton<PlayerController>
{
    [field: SerializeField] public PlayerDInfo P_DInfo { get; set; }
    [field: SerializeField] public PlayerSInfo P_SInfo { get; set; }

    [field: SerializeField] private DynamicMoveProvider moveProvider { get; set; } = null;

    private void Start() {
        P_DInfo = new PlayerDInfo();
        P_SInfo = new PlayerSInfo();
    }

    // stop player
    public void StopPlayer() {
        moveProvider.moveSpeed = 0;
    }
}
