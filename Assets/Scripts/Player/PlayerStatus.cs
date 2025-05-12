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
        levelText = GetComponent<TextMeshProUGUI>();
        if (levelText == null) Debug.Log("Level TMP is Null!");
    }

    public void Start()
    {
        levelText.gameObject.SetActive(false);
    }

    public void LevelUp()
    {
        if (level >= 1) levelText.gameObject.SetActive(true);
        level++;
        levelText.text = $"LV.{level}";
    }
}
