using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스킬리스트
public enum skillList
{
    AttackPowerUP,
    AttackSpeedUP,
    AttackProjectileUP
}


public class Skill : MonoBehaviour
{
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
