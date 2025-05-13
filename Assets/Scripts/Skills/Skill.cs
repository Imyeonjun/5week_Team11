using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //��ų ����Ʈ
    //ȭ�� ���� ����
    //���ݷ� ����
    //���ݼӵ� ����
    //ȭ���� ���� ��ȭ
    
    public PlayerWeaponHandler playerWeaponHandler;

    public float AttackPowerUP() //���ݷ� 5 ����
    {
        float baseAttackPower = playerWeaponHandler.Power;
        return baseAttackPower + 5;
    }
    public float AttackSpeedUp() //���ݼӵ� 30% ����
    {
        float baseAttackSpeed = playerWeaponHandler.Delay;
        return baseAttackSpeed * 1.3f;
    }

    public float MoveSpeedUp() //�̵��ӵ� 20% ����
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
