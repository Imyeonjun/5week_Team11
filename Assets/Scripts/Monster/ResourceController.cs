using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f; // 피해 후 무적 지속 시간

    private MonsterBase _base;
    private MonsterAnimation _animation;
    private MonsterStat _stat;
    
    private float timeSinceLastChange = float.MaxValue; // 마지막 체력 변경 이후 경과 시간

    public float CurrentHealth { get; private set; } // 현재 체력 (외부 접근만 허용)
    public float MaxHealth => _stat.Health; // 최대 체력은 StatHandler로부터 가져옴

    private void Awake()
    {
        _base = GetComponent<MonsterBase>();
        _animation = GetComponent<MonsterAnimation>();
        _stat = GetComponent<MonsterStat>();

       
    }

    private void Start()
    {
        
    }

    private void Update()
    {
		    // 아직 무적 상태라면 시간 누적
        if (timeSinceLastChange < healthChangeDelay)
        {
		        // 무적 시간 종료 시 애니메이션에도 알림
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                _animation.InvincibilityEnd();
            }
        }
    }

		// 체력 변경 함수 (피해 or 회복)
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
            _animation.Damage(); // 맞는 애니메이션 실행
            
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
        _base.Death(); // 사망 애니메이션 실행 
    }

}