using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStaticInfomation", menuName = "StaticInfomations/Players/PlayerStaticInfomation")]
public class Player_SInfo : Creature_SInfo
{
    [field: Header("�÷��̾� ����")]

    [field: SerializeField]
    public string Name { get; private set; }
}