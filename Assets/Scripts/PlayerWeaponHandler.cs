using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;

    public float Delay { get { return delay; } set { delay = value; } }

    [SerializeField] private float weaponSize = 1f;

    public float WeaponSize { get { return weaponSize; } set { weaponSize = value; } }

    [SerializeField] private float power = 1f;

    public float Power { get { return power; } set { power = value; } }

    [SerializeField] private float speed = 1f;

    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private float attackRange = 10f;

    public float AttackRange { get { return attackRange; } set { attackRange = value; } }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;

    public bool IsOnKnockback { get { return isOnKnockback; } set { isOnKnockback = value; } }

    [SerializeField] private float knockbackPower = 0.1f;

    public float KnockbackPower { get { return knockbackPower; } set { knockbackPower = value; } }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get { return knockbackTime; } set { KnockbackTime = value; } }

    private static readonly int isAttack = Animator.StringToHash("isAttack");

    public PlayerController Controller { get; private set; }

    private Animator animator;
    private SpriteRenderer weaponRenderer;

 
    protected virtual void Awake()
    {
        Controller = GetComponentInParent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public void Attack()
    {
        AttackAnimation();

    }

    public void AttackAnimation()
    {
        animator.SetTrigger(isAttack);
        Debug.Log("무기공격");
    }

    public void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
