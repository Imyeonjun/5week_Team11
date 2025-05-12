using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillPickUp : MonoBehaviour
{
    
    LayerMask levelCollisionLayer;
    List<Skill> skills;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Skill skill = new Skill();        
        
        if(collision.gameObject.layer != 6)
        {
            return;
        }

        else
        {
            //skill.AddComponent<Skill>();
            Debug.Log("½ºÅ³ È¹µæ");
            Destroy(this.gameObject);
            
        }
    }
}
