using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject skillCardBg;
    public RectTransform startPoint;
    public SkillList skillList;
    public Skill skill;
    public PlayerWeaponHandler playerWeaponHandler;

    public void ShowSkillCard()
    {
        skillCardBg.SetActive(true);
        MakePrefabs();
    }

    public void MakePrefabs()
    {
        float width = 300f;
        float totalwidth = width * 2;
        float interval = -totalwidth / 2;

        List<int> list = new(); //랜덤숫자 뽑기용 리스트

        for (int i = 0; i < 3; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, skillList.skillLists.Count);
            }
            while (list.Contains(index));

            list.Add(index);

            SkillElements skill = skillList.skillLists[index];

            GameObject card = Instantiate(cardPrefab, startPoint);
            RectTransform rect = card.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(interval + i * width, 0);

            SkillCardSet cardSript = card.GetComponent<SkillCardSet>();
            cardSript.setCard(skill, index, this);
        }
    }

    public void ApplySkill(int skillIndex)
    {
        var selectSkill = skillList.skillLists[skillIndex];

        switch(selectSkill.name)
        {
            case "공격력 증가":
                playerWeaponHandler.Power = skill.AttackPowerUP();
                Debug.Log("공격력 증가");
                Debug.Log(playerWeaponHandler.Power);
                break;

            case "공격속도 증가":
                playerWeaponHandler.Delay = skill.AttackSpeedUp();
                
                break;

            case "투사체 증가":
                playerWeaponHandler.NumberofProjectilesPerShot = skill.AttackProjectileUP();
                break;
        }
    }
}
