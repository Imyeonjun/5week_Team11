using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //스킬 리스트
    //화살 개수 증가
    //공격력 증가
    //공격속도 증가
    //화살의 방향 변화

    public PlayerController player;
    private PlayerWeaponHandler playerWeaponHandler;

    public void Initialize(PlayerWeaponHandler handler) //인스턴스 확실히 지정해주기 위해서 사용
    {
       playerWeaponHandler = handler;
    }

    public void AttackPowerUP() //공격력 5 증가
    {
        playerWeaponHandler.Power += 5f;
    }

    public void AttackSpeedUp() //공격속도 10% 증가
    {
        playerWeaponHandler.Delay *= 0.9f;
    }

    public void MoveSpeedUp() //투사체 속도 20% 증가
    {
        playerWeaponHandler.Speed *= 1.2f;
    }

    public void AttackProjectileUP() //투사체 갯수 하나 증가
    {
        playerWeaponHandler.NumberofProjectilesPerShot += 1;
    }
}
