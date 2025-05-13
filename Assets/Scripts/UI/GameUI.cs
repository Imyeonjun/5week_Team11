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

    private void Start()
    {
        UpdateHPSlider(1);
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

}
