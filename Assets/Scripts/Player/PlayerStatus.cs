using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private TextMeshProUGUI levelText;
    [SerializeField] private int level = 0;

    private void Awake()
    {
        levelText = GetComponentInChildren<TextMeshProUGUI>();
        if (levelText == null) Debug.Log("Level TMP in Canvas is Null!");
    }

    public void Start()
    {
        levelText.gameObject.SetActive(false);
    }

    public void LevelUp()
    {
        level++;
        levelText.text = $"LV.{level}";
    }

    public void Update()
    {
        if (level >= 1)
        {
            levelText.text = $"LV.{level}";
            levelText.gameObject.SetActive(true);
        }
    }
}
