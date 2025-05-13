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

    public float AttackPowerUP() //공격력 5 증가
    {
        float baseAttackPower = playerWeaponHandler.Power;
        return baseAttackPower + 5;
    }
    public float AttackSpeedUp() //공격속도 30% 증가
    {
        float baseAttackSpeed = playerWeaponHandler.Delay;
        return baseAttackSpeed * 1.3f;
    }

    public float MoveSpeedUp() //이동속도 20% 증가
    {
        float baseMoveSpeed = playerWeaponHandler.Speed;
        return baseMoveSpeed * 1.2f;
    }
    //public int AttackProjectileUP()
    //{
    //    int baseAttackShot = playerWeaponHandler.NumberofProjectilesPerShot;
    //    int finalAttackShot = 3;
    //    return finalAttackShot;
    //}
}
