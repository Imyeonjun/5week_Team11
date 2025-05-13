
using UnityEngine;
using System.Collections;

public class BossMonsterController : MonsterBase
{
    [Header("Boss Settings")]
    [SerializeField] private float bossScale = 3f;
    [SerializeField] private float closeAttackRange = 2f;
    [SerializeField] private float attackDelay = 2f;
    [SerializeField] private float knockbackResistance = 0.1f;

    [Header("Weapons")]
    [SerializeField] private MonsterClose closeWeapon;
    [SerializeField] private MonsterRangeWeapon rangeWeapon;

    [Header("References")]
    [SerializeField] private Animator animator;
    private Transform player;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        transform.localScale = Vector3.one * bossScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(BossAttackRoutine());
    }

    private IEnumerator BossAttackRoutine()
    {
        while (isAlive)
        {
            if (Vector2.Distance(transform.position, player.position) < closeAttackRange)
            {
                AttackClose();
            }
            else
            {
                AttackRange();
            }

            yield return new WaitForSeconds(attackDelay);
        }
    }

    private void AttackClose()
    {
        
        closeWeapon.Attack();
    }

    private void AttackRange()
    {
        // animator.SetTrigger("AttackRange");
        Vector2 direction = (player.position - transform.position).normalized;
        rangeWeapon.CreateProjectile(direction, 0f);
    }

    public override void ApplyKnockback(Transform attacker, float power, float duration)
    {
        // 넉백 저항 적용 (보스는 약하게만 밀림)
        Vector2 direction = (transform.position - attacker.position).normalized;
        Vector2 knockbackForce = direction * power * knockbackResistance;
        _rigidbody.AddForce(knockbackForce, ForceMode2D.Impulse);
    }

    public override void Death()
    {
        isAlive = false;
        animator.SetTrigger("Damage");
        base.Death();
    }
}