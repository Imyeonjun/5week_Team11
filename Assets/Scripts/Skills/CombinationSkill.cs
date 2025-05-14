using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationSkill : MonoBehaviour //To Do: Skill -> combinationSkill 상속
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

    public void FireArrow() //To Do: 데미지 등 실제 기능 추가할 것
    {
        if (fireTrailPrefab && firePoint)
        {
            Instantiate(fireTrailPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("불화살 발사");
        }
    }

}
