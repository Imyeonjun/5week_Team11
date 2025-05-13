using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject skillCardBg;
    public RectTransform startPoint;
    public SkillPickUp skillPickUp;
    public SkillList skillList;

    public void CardChoice()
    {
        skillCardBg.SetActive(true);
        MakePrefabs();
    }

    public void MakePrefabs()
    {
        float width = 300f;
        float totalwidth = width * 2;
        float interval = -totalwidth / 2;

        List<int> list = new(); //·£´ý¼ýÀÚ »Ì±â¿ë ¸®½ºÆ®

        for (int i = 0; i < 3; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, skillList.skillLists.Count);
            } while (usedIndexes.Contains(index));


            GameObject card = Instantiate(cardPrefab, startPoint);
            RectTransform rect = card.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(interval + i * width, 0);
        }
    }

    public void ChoiceCard()
    {
        skillPickUp.GetRandomSkill();
    }
}

