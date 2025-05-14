using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationSkill : MonoBehaviour //To Do: Skill -> combinationSkill »ó¼Ó
{
    public PlayerController player;
    private PlayerWeaponHandler playerWeaponHandler;
    public void Initialize(PlayerWeaponHandler handler) 
    {
        playerWeaponHandler = handler;
    }
}
