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
    [SerializeField] private bool CombiSkillOn = false; //����׿�

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

        List<int> list = new(); //�������� �̱�� ����Ʈ

        for (int i = 0; i < 3; i++)  //����Ʈ ������ ���� �ٸ� ��ų 3�� �̱�
        {
            int index;

            do index = Random.Range(0, skillList.skillLists.Count);
            while (list.Contains(index));

            list.Add(index);

            GameObject card = Instantiate(cardPrefab, startPoint);
            RectTransform rect = card.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(interval + i * width, 0);  //������ ���� ����

            SkillElements skill = skillList.skillLists[index]; //��ų����Ʈ ����
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
            case "���ݷ� ����": skill.AttackPowerUP(); break;

            case "���ݼӵ� ����": skill.AttackSpeedUp(); break;

            case "����ü �ӵ� ����": skill.ProjectileSpeedUp(); break;
        }

        foreach (Transform child in startPoint) // ī�� ������ ����
        {
            Destroy(child.gameObject);
        }

        skillCardBg.SetActive(false);
        SkillCombineCheck();
        return;
    }
    private void SkillCombineCheck()
    {
        SkillElements powerUp = skillList.skillLists.Find(s => s.name == "���ݷ� ����");
        SkillElements attackSpeedUp = skillList.skillLists.Find(s => s.name == "���ݼӵ� ����");
        SkillElements projectileSpeedUp = skillList.skillLists.Find(s => s.name == "����ü �ӵ� ����");

        bool isReadyForProjectilePerShot = powerUp.stack >= 1 && attackSpeedUp.stack >= 1;

        if (isReadyForProjectilePerShot) combinationSkill.PlusProjectilesPerShot(); //ȭ�� ���� ����
    }
}

