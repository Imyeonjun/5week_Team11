using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardMaker: MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private RectTransform startPoint;
    [SerializeField] private float width = 300f;
    private float interval;
    private float totalwidth;

    void Start()
    {
        totalwidth = width * 2;
        interval = -totalwidth / 2;
    }

    public void MakePrefabs()
    {
        for (int i = 0; i < 3;  i++)
        {
            GameObject card = Instantiate(cardPrefab, startPoint);
            RectTransform rect = card.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(interval + i * width, 0);
        }
    }
}
