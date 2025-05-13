using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardSet : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;

    private int skillIndex;
    public void setCard(SkillElements skill, int index)
    {
        nameText.text = skill.name;
        descriptionText.text = skill.description;
        skillIndex = index;
    }
}

