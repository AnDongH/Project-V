using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DontDestroySingleton<PlayerController>
{
    [field: SerializeField] public PlayerDInfo P_DInfo { get; set; }
    [field: SerializeField] public PlayerSInfo P_SInfo { get; set; }

}
