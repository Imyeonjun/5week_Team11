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
                Debug.Log("���ݷ� ����!");
                break;
            case skillList.AttackSpeedUP:
                playerWeaponHandler.Delay = skill.AttackSpeedUp();
                Debug.Log("���ݼӵ� ����!");
                break;
            case skillList.AttackProjectileUP:
                playerWeaponHandler.NumberofProjectilesPerShot = skill.AttackProjectileUP();
                Debug.Log("ȭ�� �� ����!");
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) //���߿� ��ų UI�� ����
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
