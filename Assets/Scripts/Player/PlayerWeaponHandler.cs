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

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private int bulletIndex;

    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1f;

    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;

    public float Duration { get { return duration; } }

    [SerializeField] private float spread;

    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPershot;

    public int NumberofProjectilesPerShot { get { return numberofProjectilesPershot; } }

    [SerializeField] private float multipleProjectileAngle;

    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }

    [SerializeField] private Color projectileColor;

    public Color ProjectileColor { get { return projectileColor; } }

    private static readonly int isAttack = Animator.StringToHash("isAttack");

    public PlayerController playerController { get; private set; }

    private Animator animator;
    private SpriteRenderer weaponRenderer;
    private ProjectileManager projectileManager;
    private Skill skill; //스킬 추가하기

 
    public void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        
        animator.speed = 1f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    public void Start()
    {
        projectileManager = ProjectileManager.Instance;
    }

    public void Attack()
    {

        AttackAnimation();

        float projectileAngleSpace = multipleProjectileAngle;
        int numberOfProjectilePerShot = numberofProjectilesPershot;

        float minAlge = -(numberOfProjectilePerShot / 2f) * projectileAngleSpace;

        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAlge + projectileAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(playerController.LookDirection, angle);
        }

    }

    public void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle)
            );
    }
    public static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(isAttack);
        
    }

    public void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
