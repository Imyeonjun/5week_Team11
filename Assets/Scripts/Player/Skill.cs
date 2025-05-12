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
    
    public PlayerWeaponHandler playerWeaponHandler;

    

    public void ApplySkill(SkillList skill)
    {
        switch (skill)
        {
            case SkillList.AttackPowerUP:
                playerWeaponHandler.Power = finalAttackPower;
        }
    }
    public float AttackPowerUP()
    {
        float baseAttackPower = playerWeaponHandler.Power;
        float bonusAttackPower = 5;
        float finalAttackPower = baseAttackPower + bonusAttackPower;
        return finalAttackPower;
        Debug.Log("AttackPowerUP");
    }
    public float AttackSpeedUp()
    {
        float finalAttackSpeed = 0.3f;
        return finalAttackSpeed;
        Debug.Log("AttackSpeedUp");
    }

    public float AttackProjectileUP()
    {
        float baseAttackShot = playerWeaponHandler.NumberofProjectilesPerShot;
        float finalAttackShot = baseAttackShot++;
        return finalAttackShot;
        Debug.Log("AttackProjectileUP");
    }

   
}
