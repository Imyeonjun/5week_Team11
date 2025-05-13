using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Animation : MonoBehaviour
{
    //무기의 공격애니메이션을 위한 피벗.
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    //데미지를 위한 피벗
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsMove = Animator.StringToHash("IsMove");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move()
    {
        animator.SetBool(IsMove, true);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    public void Attack()
    {
        animator.SetBool(IsAttack, true);
    }

}
