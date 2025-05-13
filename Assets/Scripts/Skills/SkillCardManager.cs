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

        List<int> list = new(); //�������� �̱�� ����Ʈ

        for (int i = 0; i < 3; i++)  //���� ����Ʈ �߿��� ���� �ٸ� ��ų 3�� �̱�
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
            rect.anchoredPosition = new Vector2(interval + i * width, 0);

            SkillElements skill = skillList.skillLists[index];
            SkillCardSet cardSript = card.GetComponent<SkillCardSet>();
            cardSript.setCard(skill, index, this);
        }
    }

    public void ApplySkill(int skillIndex)
    {
        var selectSkill = skillList.skillLists[skillIndex];

        switch(selectSkill.name)
        {
            case "���ݷ� ����":
                skill.AttackPowerUP();
                Debug.Log($"���ݷ� 5 ����");
                break;

            case "���ݼӵ� ����":
                skill.AttackSpeedUp();
                Debug.Log($"���ݼӵ� 10% ����");
                break;

            case "����ü �ӵ� ����":
                skill.MoveSpeedUp();
                Debug.Log($"����ü �ӵ� 20% ����");
                break;

            case "����ü ����":
                skill.AttackProjectileUP();
                Debug.Log($"����ü ���� �ϳ� ����");
                break;
        }

        skillCardBg.SetActive(false);

        foreach (Transform child in startPoint)
        {
            Destroy(child.gameObject);
        }

        return;
    }
}
