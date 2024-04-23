using UnityEngine;

public abstract class Creature_DInfo : DynamicInfomation
{
    [field: Header("����ü ����")]

    [field: SerializeField]
    public CapsuleCollider2D Collider { get; private set; }
    [field: SerializeField]
    public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField]
    public Animator BoneAnimator { get; private set; }
}