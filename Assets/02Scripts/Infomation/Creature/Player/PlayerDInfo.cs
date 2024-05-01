using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlayerDInfo : CreatureDInfo
{
    public PlayableDirector CurDirector { get; set; } = null;
}