using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;

    public PlayerController playerController;
    public ResourceController resourceController;
    public CharacterStat characterStat;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        resourceController = FindObjectOfType<ResourceController>();
        characterStat = playerController.GetComponent<CharacterStat>();

        UpdateHPSlider(1);
        UpdateHPText(resourceController.CurrentHealth, characterStat.Health);
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    public void UpdateCountMonster(int count) // 몬스터 카운트 추가
    {
        countText.text = count.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public void UpdateHPText(float currentHP, float maxHP)
    {
        hpText.text = $"{Mathf.CeilToInt(currentHP)} / {Mathf.CeilToInt(maxHP)}"; // 현재 체력 텍스트로 표시
    }

}
