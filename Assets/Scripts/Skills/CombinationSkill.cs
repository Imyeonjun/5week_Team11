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

    public void PlusProjectilesPerShot() //To Do: ������ �� ���� ��� �߰��� ��
    {
<<<<<<< HEAD
        playerWeaponHandler.NumberofProjectilesPerShot += 1;
        Debug.Log("ȭ�� ���� + 1");
=======
        if (fireTrailPrefab && firePoint)
        {
            Instantiate(fireTrailPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("��ȭ�� �߻�");
        }
>>>>>>> parent of 1e8a125 (Merge branch 'Sungwon' into TestCombine_Sungwon)
    }

}
