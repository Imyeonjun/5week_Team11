using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //스킬 리스트
    //화살 개수 증가
    //공격력 증가
    //공격속도 증가
    //화살의 방향 변화
    //public PlayerController player;
    public PlayerWeaponHandler playerWeaponHandler;
     
    public float AttackPowerUP()
    {
        float baseAttackPower = playerWeaponHandler.Power;
        float bonusAttackPower = 5;
        float finalAttackPower = baseAttackPower + bonusAttackPower;
        return finalAttackPower;
    }
    public float AttackSpeedUp()
    {
        float baseAttackSpeed = playerWeaponHandler.Delay;
        float finalAttackSpeed = 0.3f;
        return finalAttackSpeed;
    }

    public int AttackProjectileUP()
    {
        int baseAttackShot = playerWeaponHandler.NumberofProjectilesPerShot;
        int finalAttackShot = 3;
        return finalAttackShot;
    }

   
}
