using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private static readonly int isMove = Animator.StringToHash("isMove");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 direction) //PlayerController에서 이동 애니메이션
    {
        animator.SetBool(isMove, direction.magnitude > 0.5f);
    }

    public void Hit() //피격 애니메이션
    {
        animator.SetBool(isHit, true);
    }

    public void InvincibilityEnd() //피격 애니메이션 종료
    {
        animator.SetBool(isHit, false);
    }
}
