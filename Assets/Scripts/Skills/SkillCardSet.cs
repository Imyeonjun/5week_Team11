using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardSet : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Button cardButton; 
    private SkillManager skillManager;

    private int skillIndex;

    public void setCard(SkillElements skill, int index, SkillManager card)
    {
        nameText.text = skill.name;
        descriptionText.text = skill.description;
        skillIndex = index;
        skillManager = card;

        cardButton.onClick.RemoveAllListeners(); 
        cardButton.onClick.AddListener (() => skillManager.ApplySkill(skillIndex)); //��ư�� ����Ʈ �ε��� �Ҵ�
    }
}
