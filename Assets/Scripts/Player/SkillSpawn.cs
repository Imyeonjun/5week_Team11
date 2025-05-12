using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawn : MonoBehaviour
{
    //public GameObject SpawnPrefab;
    public SkillPickUp skillPickUp;
    private Skill skill;

    public void Awake() //나중에 스킬 UI때 편집
    {

        skill = GetComponentInChildren<Skill>();
        skillPickUp = GetComponentInChildren<SkillPickUp>();

        skillPickUp.ApplyRandomSkill();

    }
}
