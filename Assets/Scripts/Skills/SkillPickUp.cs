using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillPickUp : MonoBehaviour
{
    LayerMask levelCollisionLayer;
    public PlayerWeaponHandler playerWeaponHandler;
    public Skill skill;
    private List<SkillListElements> skillList = new List<SkillListElements>();

    //public void Awake()
    //{
    //    if (playerWeaponHandler != null)
    //        playerWeaponHandler = Instantiate(WeaponPrefab, weaponPivot);
    //    else
    //        weaponHandler = GetComponentInChildren<PlayerWeaponHandler>();
    //}

    public void ApplyRandomSkill()
    {
        GetRandomSkill();
        //ApplySkill(skillList);
    }

    public void GetRandomSkill()
    {
        int index = Random.Range(0, skillList.Count);
        ApplySkill(index);
        
    }


    public void ApplySkill(int num)
    {
        
    }
}

