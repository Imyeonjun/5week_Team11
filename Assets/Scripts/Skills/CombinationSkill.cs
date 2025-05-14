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

    public void PlusProjectilesPerShot() //To Do: 데미지 등 실제 기능 추가할 것
    {
<<<<<<< HEAD
        playerWeaponHandler.NumberofProjectilesPerShot += 1;
        Debug.Log("화살 갯수 + 1");
=======
        if (fireTrailPrefab && firePoint)
        {
            Instantiate(fireTrailPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("불화살 발사");
        }
>>>>>>> parent of 1e8a125 (Merge branch 'Sungwon' into TestCombine_Sungwon)
    }

}
