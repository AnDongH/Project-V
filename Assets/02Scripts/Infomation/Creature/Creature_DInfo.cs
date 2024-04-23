using UnityEngine;

public abstract class Creature_DInfo : DynamicInfomation
{
    [field: Header("생명체 정보")]

    [field: SerializeField]
    public CapsuleCollider2D Collider { get; private set; }
    [field: SerializeField]
    public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField]
    public Animator BoneAnimator { get; private set; }
}