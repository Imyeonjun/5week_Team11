using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatus : MonoBehaviour
{
    private TextMeshProUGUI levelText;
    public RectTransform levelBar;
    [SerializeField] private int level = 0;
    [SerializeField] private int exp = 0;
    int maxEXP = 100;
    private bool displayIsReady = false;

    private void Awake()
    {
        levelText = GetComponentInChildren<TextMeshProUGUI>();
        if (levelText == null) Debug.Log("Level TMP in Canvas is Null!");
    }

    public void Start()
    {
        levelText.gameObject.SetActive(false);
    }

    public void Update() 
    {
        if (displayIsReady == false && level >= 1) //레벨업 확인용 코드, 완성 시 Levelup()과 병합
        {
            levelText.gameObject.SetActive(true);
            displayIsReady = true;
        }

        EXPupdate();
    }

    public void EXPupdate() //GameManager에서 끌어올 것
    {
        Vector3 scale = levelBar.localScale;
        scale.x = (float)exp / maxEXP;
        levelBar.localScale = scale;

        if (exp >= maxEXP)
        {
            Levelup();
        }
    }
    public void Levelup()
    {
        level++;
        //MaxEXPChanger(maxEXP); 레벨업 될 때마다 최대 EXP 변경
        levelText.text = $"LV.{level}";
        exp -= maxEXP;
    }

    //public void MaxEXPChanger(int maxEXP)
    //{
    //}
}
