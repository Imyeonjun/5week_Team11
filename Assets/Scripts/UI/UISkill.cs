using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkill : MonoBehaviour
{
    SkillPickUp skillPickUp;

    //public void Awake()
    //{
        
    //}
    public void OnClickSkillButton()
    {
        skillPickUp.ApplyRandomSkill();
    }

}
