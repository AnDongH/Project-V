using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DontDestroySingleton<PlayerController>
{
    [field: SerializeField] public Player_DInfo P_DInfo { get; set; }
    [field: SerializeField] public Player_SInfo P_SInfo { get; set; }


}
