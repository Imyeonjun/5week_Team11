using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawn : MonoBehaviour
{
    //public GameObject SpawnPrefab;
    public SkillPickUp skillPickUp;
    private Skill skill;

    public void Awake()
    {

        skill = GetComponentInChildren<Skill>();
        skillPickUp = GetComponentInChildren<SkillPickUp>();

        skillPickUp.ApplyRandomSkill();
        

       


    }
}
