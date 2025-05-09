using UnityEngine;

// 모든 몬스터의 기본 움직임, 애니메이션, 스텟 처리 클래스.
public class Monster_Controller : MonoBehaviour
{
#region SerializeField
    [Header("몬스터 이동 설정")]
    [SerializeField] private SpriteRenderer characterRenderer; // 좌우 반전을 위한 렌더러
    [SerializeField] private Transform weaponPivot; // 무기를 회전시킬 기준 위치
    
    [SerializeField] private Monster_WeaponHandler WeaponPrefab;
    private Monster_WeaponHandler weaponHandler;

    [Header("몬스터 스텟 설정")]
    [Range(0,100)][SerializeField] private float health = 10.0f; // 몬스터 체력
    public float Health{get => health; set => health = Mathf.Clamp(value,0,100); }
    [Range(0f,20f)][SerializeField] private float speed = 1.0f; // 몬스터 이동 속도
    public float Speed{get => speed; set => speed = Mathf.Clamp(value,0,20); }

    [Header("몬스터 리소스")]
    [SerializeField] private float healthChangeDelay = 0.5f; // 피해 후 무적 지속 시간.

#endregion

#region 선언
    private Transform targetPlayer;
    protected Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트
    protected Monster_Animation monsterAnimation; // 애니메이션 처리 클래스

    protected Vector2 movementDirection = Vector2.zero; // 현재 이동 방향
    public Vector2 MovementDirection{get{return movementDirection;}}
    
    protected Vector2 lookDirection = Vector2.zero; // 현재 바라보는 방향
    public Vector2 LookDirection{get{return lookDirection;}}

    private Vector2 knockback = Vector2.zero; // 넉백 방향
    private float knockbackDuration = 0.0f; // 넉백 지속 시간

    private float timeSinceLastChange = float.MaxValue; // 마지막 체력 변경 시간

    public float CurrentHealth{get; private set;} // 현재 체력
    public float MaxHealth => health;// 최대 체력은 몬스터 스텟에서 설정한 체력으로 초기화
    
    protected bool isAttacking;//공격중 여부
    private float timeSinceLastAttack = float.MaxValue;//마지막 공격 이후 경과 시간.

#endregion 
    
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        monsterAnimation = GetComponent<Monster_Animation>();
        
        characterRenderer = GetComponentInChildren<SpriteRenderer>();

       if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<Monster_WeaponHandler>();
       
    }

    protected virtual void Start()
    {
        CurrentHealth = health;

        //테그에서 플레이어를 가져옴
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj != null)
        {
            targetPlayer = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found");
        }
    }
    
    protected virtual void Update()
    {
        Rotate(lookDirection);
        HandleAttackDelay();

        if(targetPlayer != null)
        {
            // 플레이어 방향으로 바라보게 설정
            lookDirection = (targetPlayer.position - transform.position).normalized;
            movementDirection = lookDirection; // 이동 방향 설정
        }

        if(timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime; // 무적 시간 증가
            if(timeSinceLastChange >= healthChangeDelay)
            {
               monsterAnimation.Damage(); // 무적 해제 애니메이션 실행
            }
        }
    }
    
    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime; // 넉백 시간 감소
        }
    }

#region 움직임 (base)
    private void Movment(Vector2 direction)
    {
        direction = direction * speed; // 이동 속도
        
        // 넉백 중이면 이동 속도 감소 + 넉백 방향 적용
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // 이동 속도 감소
            direction += knockback; // 넉백 방향 추가
        }
        
        // 실제 물리 이동
        _rigidbody.velocity = direction;
        monsterAnimation.Move();


    }

    private void Rotate(Vector2 direction)
    {
       
        if(direction == Vector2.zero)
        {
            return; // 방향이 없으면 회전하지 않음
        }

        bool isLeft = direction.x < 0;
        
        if(direction != Vector2.zero)
        {   
            // 스프라이트 좌우 반전
            characterRenderer.flipX = isLeft;
        }
        

        weaponHandler?.Rotate(isLeft);
       
    }
    
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // 상대 방향을 반대로 밀어냄
        knockback = -(other.position - transform.position).normalized * power;
    }  

   
#endregion

#region 체력 (Resource)
    
    public bool ChangeHealth(float change)
    {
        // 변화 없거나 무적 상태면 무시
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f; // 다시 무적 시작
        
        // 체력 적용
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

		// 데미지일 경우 (음수)
        if (change < 0)
        {
            monsterAnimation.Damage(); // 맞는 애니메이션 실행
            
        }

		// 체력이 0 이하가 되면 사망 처리
        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        Debug.Log("몬스터 사망");
        // monsterAnimation.Damage(); // 죽는 애니메이션 실행
        // Desrtoy(gameObject,1f); // 몬스터 오브젝트 파괴
    }



#endregion    


#region 무기 (weaponhandler)

    private void HandleAttackDelay()
    {
        if(weaponHandler == null)
        {
            return;
        }

        if(timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        
        // 공격 입력 중이고 쿨타임이 끝났으면 공격 실행
        if(isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack(); // 실제 공격 실행
        }
    }

    protected void Attack()
    {
        if(lookDirection != Vector2.zero)
        {          
            weaponHandler?.Attack(); // 무기 공격 실행
        }
    }

#endregion
}
