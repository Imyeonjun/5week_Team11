using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject skillCardBg;
    public RectTransform startPoint;
    public SkillList skillList;
    public Skill skill;
    public CombinationSkill combinationSkill;
    public PlayerController playerController;
    [SerializeField] private bool CombiSkillOn = false; //디버그용

    public void Start()
    {
        skillCardBg.SetActive(false);
        skill.Initialize(playerController.WeaponHandler);
    }

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

        for (int i = 0; i < 3; i++)  //리스트 내에서 서로 다른 스킬 3개 뽑기
        {
            int index;

            do index = Random.Range(0, skillList.skillLists.Count);
            while (list.Contains(index));

            list.Add(index);

            GameObject card = Instantiate(cardPrefab, startPoint);
            RectTransform rect = card.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(interval + i * width, 0);  //프리팹 생성 간격

            SkillElements skill = skillList.skillLists[index]; //스킬리스트 내용
            SkillCardSet cardSript = card.GetComponent<SkillCardSet>();
            cardSript.setCard(skill, index, this);
        }
    }

    public void ApplySkill(int skillIndex)
    {
        var selectSkill = skillList.skillLists[skillIndex];
        string skillName = selectSkill.name;
        selectSkill.stack++;

        switch (skillName)
        {
            case "공격력 증가": skill.AttackPowerUP(); break;

            case "공격속도 증가": skill.AttackSpeedUp(); break;

            case "투사체 속도 증가": skill.ProjectileSpeedUp(); break;
        }

        foreach (Transform child in startPoint) // 카드 프리팹 제거
        {
            Destroy(child.gameObject);
        }

        skillCardBg.SetActive(false);
        SkillCombineCheck();
        return;
    }
    private void SkillCombineCheck()
    {
        SkillElements powerUp = skillList.skillLists.Find(s => s.name == "공격력 증가");
        SkillElements attackSpeedUp = skillList.skillLists.Find(s => s.name == "공격속도 증가");
        SkillElements projectileSpeedUp = skillList.skillLists.Find(s => s.name == "투사체 속도 증가");

        bool isReadyForProjectilePerShot = powerUp.stack >= 1 && attackSpeedUp.stack >= 1;

        if (isReadyForProjectilePerShot) combinationSkill.PlusProjectilesPerShot(); //화살 갯수 증가
    }
}

