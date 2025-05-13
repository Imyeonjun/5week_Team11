using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //��ų ����Ʈ
    //ȭ�� ���� ����
    //���ݷ� ����
    //���ݼӵ� ����
    //ȭ���� ���� ��ȭ

    public PlayerController player;
    private PlayerWeaponHandler playerWeaponHandler;

    public void Initialize(PlayerWeaponHandler handler) //�ν��Ͻ� Ȯ���� �������ֱ� ���ؼ� ���
    {
       playerWeaponHandler = handler;
    }

    public void AttackPowerUP() //���ݷ� 5 ����
    {
        playerWeaponHandler.Power += 5f;
    }
    public void AttackSpeedUp() //���ݼӵ� 10% ����
    {
        playerWeaponHandler.Delay -= playerWeaponHandler.Delay * - 0.1f;
    }

    public void MoveSpeedUp() //����ü �ӵ� 20% ����
    {
        playerWeaponHandler.Speed *= 1.2f;
    }

    //public int AttackProjectileUP()
    //{
    //    int baseAttackShot = playerWeaponHandler.NumberofProjectilesPerShot;
    //    int finalAttackShot = 3;
    //    return finalAttackShot;
    //}
}
