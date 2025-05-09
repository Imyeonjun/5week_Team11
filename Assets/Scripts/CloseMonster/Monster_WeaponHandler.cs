using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//무기의 스텟 및 공격, 애니메이션도 같이 처리
public class Monster_WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }
    
    [SerializeField] private float weaponSize = 1f;
		public float WeaponSize { get => weaponSize; set => weaponSize = value; }
    
    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }
    
    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange =value; }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }
    
    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    
    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
    
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    
    protected Animator animator;
    protected SpriteRenderer weaponRenderer;
    protected Monster_Controller monsterController;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        monsterController = GetComponentInParent<Monster_Controller>();
        
        if(animator != null)
        {
            animator.speed = 1.0f / delay;
        }
         
    }

    protected virtual void Start()
    {
        if(monsterController == null)
        {
            Debug.LogError("Monster_Controller is not assigned in the inspector.");
        }
    }
    
    public virtual void StartAttackAnimation()
    {
        if(animator != null)
        {
            animator.SetTrigger(IsAttack);
        }
        else
        {
            PerformAttackHitCheck();
            OnAttackAnimationEnd();
        }
        
    }

    public virtual void PerformAttackHitCheck()
    {
        Debug.Log("몬스터웨폰핸들러: PerformAttackHitCheck");
    }

    public void OnAttackAnimationEnd()
    {
        if(monsterController != null)
        {
            monsterController.EndAttack();
        }

        Debug.Log("몬스터웨폰핸들러 : OnAttackAnimationEnd");
    }

    public void EndAttackAnimation()
    {
        if(animator != null)
        {
            animator.SetBool(IsAttack, false);
        }
    }


    public virtual void Rotate(bool isLeft)
    {
       if(isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
    
    
}
