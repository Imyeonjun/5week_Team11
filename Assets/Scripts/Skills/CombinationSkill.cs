using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationSkill : MonoBehaviour //To Do: Skill -> combinationSkill 상속
{
    public PlayerController player;
    private PlayerWeaponHandler playerWeaponHandler;

    public void Initialize(PlayerWeaponHandler handler) 
    {
        playerWeaponHandler = handler;
    }

    public void PlusProjectilesPerShot() //To Do: 데미지 등 실제 기능 추가할 것
    {
        playerWeaponHandler.NumberofProjectilesPerShot += 1;
        Debug.Log("화살 갯수 + 1");
    }

}
