using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(StatHandler), typeof(AnimationHandler))]
public class Ranged_Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private StatHandler statHandler;
    private AnimationHandler animHandler;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackRange = 10f;
    public LayerMask playerLayer;

    private Transform player;

    private bool isMoving = false;
    private float moveTimer = 0f;
    private float attackTimer = 0f;
    private Vector2 moveDirection;
    private float idleTimer = 0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        statHandler = GetComponent<StatHandler>();
        animHandler = GetComponent<AnimationHandler>();

        rb.gravityScale = 0f; 
        rb.freezeRotation = true;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        SetRandomMove();
        SetRandomAttack();
    }

    private void Update()
    {
        HandleAttackTimer();
        if (isMoving)
        {
            HandleMoveTimer();
        }
        else
        {
            HandleIdleTimer();
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 newPosition = rb.position + moveDirection * statHandler.Speed * Time.fixedDeltaTime;

           
            RaycastHit2D hit = Physics2D.Raycast(rb.position, moveDirection, 0.5f, LayerMask.GetMask("Wall"));
            if (hit.collider == null)
            {
                rb.MovePosition(newPosition);
            }

            animHandler.Move(moveDirection);
        }
        else
        {
            animHandler.Move(Vector2.zero);
        }
    }

    private void SetRandomMove()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        moveTimer = Random.Range(1f, 3f);
        isMoving = true;
    }

    private void SetRandomAttack()
    {
        attackTimer = Random.Range(2f, 5f);
    }

    private void HandleMoveTimer()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            StartIdle(); // 이동 종료 후 정지 상태 시작
        }
    }
    private void HandleIdleTimer()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            SetRandomMove(); // 정지 후 다시 이동 시작
        }
    }
    private void StartIdle()
    {
        isMoving = false;
        idleTimer = Random.Range(3f, 5f);
        animHandler.Move(Vector2.zero); // 애니메이션 정지
    }
    
    private void HandleAttackTimer()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            FireProjectile();
            SetRandomAttack();
        }
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - firePoint.position).normalized;

        Rigidbody2D prb = projectile.GetComponent<Rigidbody2D>();
        if (prb != null)
        {
            prb.velocity = direction * 5f;
        }
    }
}
