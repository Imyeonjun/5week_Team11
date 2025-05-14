using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationSkill : MonoBehaviour
{
    public PlayerController player;
    private PlayerWeaponHandler playerWeaponHandler;
    public void Initialize(PlayerWeaponHandler handler) //인스턴스 확실히 지정해주기 위해서 사용
    {
        playerWeaponHandler = handler;
    }
}
