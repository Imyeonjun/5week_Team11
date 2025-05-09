using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_CloseMonster : Monster_WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one; // 공격 범위 (충돌 박스 크기)

    private Monster_Controller controller; // 몬스터 컨트롤러

	// 무기 크기에 따라 충돌 범위를 확장
    protected override void Start()
    {
        collideBoxSize = collideBoxSize * WeaponSize;
        controller = GetComponentInParent<Monster_Controller>();
    }

    public override void Attack()
    {
        // BoxCast로 근접 공격 판정 (LookDirection 방향으로 충돌 검사)
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)controller.LookDirection * collideBoxSize.x, // 위치
            collideBoxSize,              // 박스 크기
            0,                           // 회전 없음
            Vector2.zero,                // 이동 거리 없음 (고정된 위치)
            0,                           // 거리 0 (한 번만 검사)
            target                       // 공격 가능한 대상 레이어 마스크
        );
        
        if (hit.collider != null)
        {
			// 대상에게 체력 감소 적용
            Monster_Controller resourceController = hit.collider.GetComponent<Monster_Controller>();
            if(resourceController != null)
            {
                resourceController.ChangeHealth(-Power); // 데미지 적용
                
                // 넉백 효과가 설정되어 있을 경우 적용
                if(IsOnKnockback)
                {
                    Monster_Controller controller = hit.collider.GetComponent<Monster_Controller>();
                    if(controller != null)
                    {
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
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
