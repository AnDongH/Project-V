using UnityEngine;

public abstract class Creature_SInfo : StaticInfomation
{
    public static float ToDamage(float attackForce)
    {
        return Random.Range(0.7f, 1.0f) * attackForce;
    }

    [field: Header("생명체 정보")]

    [field: SerializeField]
    public float DeathAnimationTime { get; private set; }

    [field: SerializeField]
    public float HitInvincibilityTime { get; private set; }
    [field: SerializeField]
    public float HitAnimationTime { get; private set; }

    [field: SerializeField]
    public float MoveSpeed { get; private set; }
}