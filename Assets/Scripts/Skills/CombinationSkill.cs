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

    public void FireArrow()
    {
        Debug.Log("조합 완료!");
    }

}
