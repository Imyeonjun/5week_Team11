using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationSkill : MonoBehaviour
{
    public PlayerController player;
    private PlayerWeaponHandler playerWeaponHandler;
    public void Initialize(PlayerWeaponHandler handler) //�ν��Ͻ� Ȯ���� �������ֱ� ���ؼ� ���
    {
        playerWeaponHandler = handler;
    }
}
