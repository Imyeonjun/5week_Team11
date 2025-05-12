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

    // Update is called once per frame
    public void Move(Vector2 direction)
    {
        animator.SetBool(isMove, direction.magnitude > 0.5f);
    }

    public void Hit()
    {
        animator.SetBool(isHit, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(isHit, false);
    }
}
