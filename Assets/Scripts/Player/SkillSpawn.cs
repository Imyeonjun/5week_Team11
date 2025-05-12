using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawn : MonoBehaviour
{
    public GameObject SpawnPrefab;
    public Skill[] skill;

    public void Start()
    {
        GameObject instance = Instantiate(SpawnPrefab, transform.parent);

        skill.SkillList
            
            pickUP = instance.GetComponentInChildren<SkillPickUp>();

        
    }
}
