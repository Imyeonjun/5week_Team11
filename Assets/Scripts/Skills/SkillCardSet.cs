using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardSet : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Button cardButton; 
    private SkillManager skillCardManager;

    private int skillIndex;

    public void setCard(SkillElements skill, int index, SkillManager card)
    {
        nameText.text = skill.name;
        descriptionText.text = skill.description;
        skillIndex = index;
        skillCardManager = card;

        cardButton.onClick.RemoveAllListeners(); 
        cardButton.onClick.AddListener (() => skillCardManager.ApplySkill(skillIndex)); //��ư�� ����Ʈ �Ҵ�
    }
}
