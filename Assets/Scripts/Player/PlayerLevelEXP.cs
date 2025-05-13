using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerLevelEXP : MonoBehaviour //�ý��� ����� ���� ��ũ��Ʈ, ���� GameManager�� �ѱ� ��
{
    private SkillCardManager skillCardManager;
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
        skillCardManager = GetComponentInChildren<SkillCardManager>();
    }

    public void Update()
    {
        if (displayIsReady == false && level >= 1) //������ Ȯ�ο� �ڵ�, �ϼ� �� Levelup()�� ����
        {
            levelText.gameObject.SetActive(true);
            displayIsReady = true;
        }

        EXPupdate();
    }

    public void EXPupdate() //GameManager���� ����� ��
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
        //MaxEXPChanger(maxEXP); 
        levelText.text = $"LV.{level}";
        exp -= maxEXP;
        skillCardManager.ShowSkillCard();
    }

    //public void maxexpchanger(int maxEXP) //������ �� ������ �ִ� exp ����
    //{
    //}
}
