using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationSkill : MonoBehaviour //To Do: Skill -> combinationSkill ���
{
    public PlayerController player;
    private PlayerWeaponHandler playerWeaponHandler;
    public GameObject fireTrailPrefab;
    public Transform firePoint;
    private 

    public void Initialize(PlayerWeaponHandler handler) 
    {
        playerWeaponHandler = handler;
    }

    public void FireArrow() //To Do: ������ �� ���� ��� �߰��� ��
    {
        if (fireTrailPrefab && firePoint)
        {
            Instantiate(fireTrailPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("��ȭ�� �߻�");
        }
    }

}
