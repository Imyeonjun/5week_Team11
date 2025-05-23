using UnityEngine;

// 근접 무기 전용 핸들러 (WeaponHandler 상속)
public class MonsterClose : MonsterWeaponHandler
{
    public PlayerController playerController;

    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one; // 공격 범위 (충돌 박스 크기)

		// 무기 크기에 따라 충돌 범위를 확장
    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();
        
        // BoxCast로 근접 공격 판정 (LookDirection 방향으로 충돌 검사)
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)_base.LookDirection * collideBoxSize.x, // 위치
            collideBoxSize,              // 박스 크기
            0,                           // 회전 없음
            Vector2.zero,                // 이동 거리 없음 (고정된 위치)
            0,                           // 거리 0 (한 번만 검사)
            target                       // 공격 가능한 대상 레이어 마스크
        );
        
        if (hit.collider != null)
        {
						// 대상에게 체력 감소 적용
            ResourceController resource = hit.collider.GetComponent<ResourceController>();
            if(resource != null)
            {
                resource.ChangeHealth(-Power); // 데미지 적용

                //PlayerController player = hit.collider.GetComponent<PlayerController>();
                //if(player != null)
                //{
                //    player.PlayerHit();
                //}
                
                // 넉백 효과가 설정되어 있을 경우 적용
                if(IsOnKnockback)
                {
                    PlayerController playerController = hit.collider.GetComponent<PlayerController>();
                    if(playerController != null)
                    {
                        playerController.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
            
        }
    }

		// 무기 방향 회전 (좌/우에 따라 오브젝트 y축 반전)
    public override void Rotate(bool isLeft)
    {
        if(isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
