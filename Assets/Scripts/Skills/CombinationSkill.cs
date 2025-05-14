using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationSkill : MonoBehaviour //To Do: Skill -> combinationSkill ���
{
    public PlayerController player;
    private PlayerWeaponHandler playerWeaponHandler;
    public GameObject fireTrailPrefab;

    public void Initialize(PlayerWeaponHandler handler) 
    {
        playerWeaponHandler = handler;
    }

    public void FireArrow() //To Do: ������ �� ���� ��� �߰��� ��
    {
        if (fireTrailPrefab && firePoint)
        {
            ProjectileManager.Instance.ShootBullet(playerWeaponHandler, firePoint.position, firePoint.right, true);
            Debug.Log("��ȭ�� �߻�");
        }
    }

}
