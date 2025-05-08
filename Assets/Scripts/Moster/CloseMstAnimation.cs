using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMstAnimation : MonoBehaviour
{
    private static readonly int IsDead = Animator.StringToHash("isDead");
  
   protected Animator animator;

   protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Dead()
    {
        animator.SetBool(IsDead, true);
    }
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
