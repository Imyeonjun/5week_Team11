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
    public PlayerController playerController;

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
            do
            {
                index = Random.Range(0, skillList.skillLists.Count);
            }
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
        int skillStack = selectSkill.stack; //SkillCardSet에서 선택한 skillIndex에 따라 리스트에 스킬 스택 저장
        skillStack++;

        switch (skillName)
        {
            case "공격력 증가":
                skill.AttackPowerUP();
                Debug.Log($"공격력 5 증가");
                Debug.Log($"공격력 스택: {selectSkill.stack}");
                break;

            case "공격속도 증가":
                skill.AttackSpeedUp();
                Debug.Log($"공격속도 10% 증가");
                Debug.Log($"공격속도 스택: {selectSkill.stack}");
                break;

            case "투사체 속도 증가":
                skill.MoveSpeedUp();
                Debug.Log($"투사체 속도 20% 증가");
                Debug.Log($"투사체 속도 스택: {selectSkill.stack}");
                break;

            case "투사체 증가":
                skill.AttackProjectileUP();
                Debug.Log($"투사체 갯수 하나 증가");
                Debug.Log($"투사체 갯수 스택: {selectSkill.stack}");
                break;
        }

        foreach (Transform child in startPoint) // 카드 프리팹 제거
        {
            Destroy(child.gameObject);
        }

        skillCardBg.SetActive(false);
        IsCombine(skillStack);
        return;
    }

    private void IsCombine(int skillStack)
    {
        bool isCombinePower = 
        if()
    }
}
