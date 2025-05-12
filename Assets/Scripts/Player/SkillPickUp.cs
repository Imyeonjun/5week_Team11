using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillPickUp : MonoBehaviour
{
    
    LayerMask levelCollisionLayer;
    public Skill skill;

    public void Awake()
    {

    }

    public void GetRandomSkill()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
             
        
        if(collision.gameObject.layer != 6)
        {
            return;
        }

        else
        {
            
            Debug.Log("½ºÅ³ È¹µæ");
            Destroy(this.gameObject);
            
        }
    }
}
