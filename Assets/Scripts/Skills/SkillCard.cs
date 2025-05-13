using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;

    public int skillIndex;
    public void setCard(SkillListElements skill, int index)
    {
        nameText.text = skill.name;
        descriptionText.text = skill.description;
        skillIndex = index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
