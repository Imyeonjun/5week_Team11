using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillPickUp : MonoBehaviour
{
    
    //LayerMask levelCollisionLayer;
    public PlayerController player;
    //public PlayerWeaponHandler playerWeaponHandler;
    public Skill skill;
    public skillList skillList;

    //public void Awake()
    //{
    //    if (playerWeaponHandler != null)
    //        playerWeaponHandler = Instantiate(WeaponPrefab, weaponPivot);
    //    else
    //        weaponHandler = GetComponentInChildren<PlayerWeaponHandler>();
    //}

    public skillList GetRandomSkill()
    {
        skillList[] values = (skillList[])System.Enum.GetValues(typeof(skillList));
        int index = Random.Range(0, values.Length);
        return values[index];
    }

    public void ApplyRandomSkill()
    {
        skillList = GetRandomSkill();
        //ApplySkill(skillList);
    }

    public void ApplySkill(skillList list)
    {
        PlayerWeaponHandler playerWeaponHandler = player.weaponHandler;
        switch (list)
        {
            case skillList.AttackPowerUP:
                playerWeaponHandler.Power = skill.AttackPowerUP();
                Debug.Log("공격력 증가!");
                break;
            case skillList.AttackSpeedUP:
                playerWeaponHandler.Delay = skill.AttackSpeedUp();
                Debug.Log("공격속도 증가!");
                break;
            case skillList.AttackProjectileUP:
                playerWeaponHandler.NumberofProjectilesPerShot = skill.AttackProjectileUP();
                Debug.Log("화살 수 증가!");
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) //나중에 스킬 UI때 편집
    {

        if(collision.gameObject.layer != 6)
        {
            return;
        }

        else
        {
            ApplySkill(skillList);
            
            Destroy(this.gameObject);
            
        }
    }
}
