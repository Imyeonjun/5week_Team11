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

    private float width = 300f;
    private float interval;
    private float totalwidth;

    void Start()
    {
        totalwidth = width * 2;
        interval = -totalwidth / 2;
    }

    public void CardChoice()
    {
        skillCardBg.SetActive(true);
        MakePrefabs();
    }

    public void MakePrefabs()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(cardPrefab, startPoint);
            RectTransform rect = card.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(interval + i * width, 0);
        }
    }

    public void ChoiceCard()
    {
        skillPickUp.ApplyRandomSkill();
    }
}

