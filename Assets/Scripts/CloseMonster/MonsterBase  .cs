using UnityEngine;

// 모든 캐릭터의 기본 움직임, 회전, 넉백 처리를 담당하는 기반 클래스
public class MonsterBase : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트
    protected MonsterAnimation _animator; // 애니메이션을 위한 컴포넌트
    protected MonsterStat _stat; // 몬스터의 능력치를 위한 컴포넌트
    
    [SerializeField] private SpriteRenderer characterRenderer; // 좌우 반전을 위한 렌더러
    [SerializeField] private Transform weaponPivot; // 무기를 회전시킬 기준 위치

    [SerializeField] public MonsterWeaponHandler WeaponPrefab; // 장착할 무기 프리팹 (없으면 자식에서 찾아 사용)
    protected MonsterWeaponHandler _weaponHandler; // 장착된 무기

    protected bool isAttacking; // 공격 중 여부
    private float timeSinceLastAttack = float.MaxValue; // 마지막 공격 이후 경과 시간


    protected Vector2 movementDirection = Vector2.zero; // 현재 이동 방향
    public Vector2 MovementDirection{get{return movementDirection;}}
    
    protected Vector2 lookDirection = Vector2.zero; // 현재 바라보는 방향
    public Vector2 LookDirection{get{return lookDirection;}}

    private Vector2 knockback = Vector2.zero; // 넉백 방향
    private float knockbackDuration = 0.0f; // 넉백 지속 시간
    
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<MonsterAnimation>();
        _stat = GetComponent<MonsterStat>();

         if(WeaponPrefab != null)
	      _weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
	    else
	      _weaponHandler = GetComponentInChildren<MonsterWeaponHandler>(); // 이미 붙어 있는 무기 사용
    }
    

    protected virtual void Start()
    {
        
    }
    
    protected virtual void Update()
    {
        
        Rotate(lookDirection);
        HandleAttackDelay();
    }
    
    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime; // 넉백 시간 감소
        }
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * 5; // 이동 속도
        
        // 넉백 중이면 이동 속도 감소 + 넉백 방향 적용
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // 이동 속도 감소
            direction += knockback; // 넉백 방향 추가
        }
        
        // 실제 물리 이동
        _rigidbody.velocity = direction;
        _animator.Move(direction); // 애니메이션 이동 처리
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;
        
        // 스프라이트 좌우 반전
        characterRenderer.flipX = isLeft;
        
        if (weaponPivot != null)
        {
		        // 무기 회전 처리
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

    private void HandleAttackDelay()
    {
        if (_weaponHandler == null)
            return;

            // 공격 쿨다운 중이면 시간 누적
        if(timeSinceLastAttack <= _weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        
        // 공격 입력 중이고 쿨타임이 끝났으면 공격 실행
        if(isAttacking && timeSinceLastAttack > _weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack(); // 실제 공격 실행
        }
    }
        
    protected virtual void Attack()
    {
            // 바라보는 방향이 있을 때만 공격
        if(lookDirection != Vector2.zero)
            _weaponHandler?.Attack();
    }
    
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        // 상대 방향을 반대로 밀어냄
        knockback = -(other.position - transform.position).normalized * power;
    }    
}
